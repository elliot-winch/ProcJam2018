using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Prism : Renderable {

    public enum FaceType
    {
        Flat,
        Round
    }

    [Header("Geometry")]
    [Range(3, 60)]
    public int vertCount = 6;

    [Header("Shape")]
    public FaceType faceType = FaceType.Flat;
    public float thickness = 0.5f;
    public float size = 1f;

    [Header("Options")]
    public bool hideTop;
    public List<int> hiddenSideFaces;

    protected override Mesh GenerateMesh()
    {
        int normalsPerVert = 0;

        //When we have flat faces, we need three normals per point
        if(faceType == FaceType.Flat)
        {
            normalsPerVert = 3;
        }
        //When we have round faces, we only need two normals per point
        else if (faceType == FaceType.Round)
        {
            normalsPerVert = 2;
        }

        //The number of geometric vertices for a prism is 2 * vertCount. We multiple by vertsPerPoint for normals
        Vector3[] vertices = new Vector3[2 * vertCount * normalsPerVert];

        var corners = GeometryUtility.PointsAboutCircle(vertCount);

        for(int i = 0; i < vertCount; i++)
        {
            for(int j = 0; j < normalsPerVert; j++)
            {
                int k1 = (i * normalsPerVert) + j;
                int k2 = k1 + (vertCount * normalsPerVert);

                vertices[k1] = new Vector3(corners[i].x * size, 0f, corners[i].y * size);

                vertices[k2] = new Vector3(corners[i].x * size, -thickness, corners[i].y * size);
            }
        }

        List<int> triangles = new List<int>();

        if(hideTop == false)
        {
            triangles.AddRange(fillSurface(Enumerable.Range(0, (vertices.Length / 2) - 1).Where(x => (x % normalsPerVert == 0)).ToArray(), true));
        }

        triangles.AddRange(fillSurface(Enumerable.Range((vertices.Length / 2), (vertices.Length / 2) - 1).Where(x => (x % normalsPerVert == 0)).ToArray(), false));

        Vector3[] normals = new Vector3[vertices.Length];

        for(int i = 0; i < vertCount; i++)
        {
            int cornerVertIndex = i * normalsPerVert;
            int bottomFaceEquivalent = (vertCount * normalsPerVert);

            normals[cornerVertIndex] = Vector3.up;
            normals[cornerVertIndex + bottomFaceEquivalent] = Vector3.down;

            int topLeft = cornerVertIndex + 1;
            int bottomLeft = topLeft + bottomFaceEquivalent;

            //Couldn't see a meaningful function for this
            int normalSkip = (normalsPerVert == 3) ? 4 : 2;

            int topRight = (topLeft + normalSkip) % (vertices.Length / 2);
            int bottomRight = topRight + bottomFaceEquivalent;

            int[] sideFace = new int[]
            {
                topLeft,
                bottomLeft,
                bottomRight,
                topRight
            };

            //if we have elected to hide this face, don't create triangles
            //else, do
            if (hiddenSideFaces.Contains(i) == false)
            {
                triangles.AddRange(fillSurface(sideFace, true));
            }

            if (faceType == FaceType.Flat)
            {
                float angle = ((2f * Mathf.PI) * (i / (float)vertCount) + (Mathf.PI / vertCount));

                var circlePoint = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                Vector3 normalVec = new Vector3(circlePoint.x, 0f, circlePoint.y).normalized;

                foreach (int s in sideFace)
                {
                    normals[s] = normalVec;
                }

            }
            else if (faceType == FaceType.Round)
            {
                //If we want rounded edges, use the vector for this corner
                //The line that travels through the center of the prism face to the corner
                Vector3 normalVec = new Vector3(corners[i].x, 0f, corners[i].y).normalized;

                normals[topLeft] = normalVec;
                normals[bottomLeft] = normalVec;
            }
        }

        Mesh mesh = new Mesh();

        mesh.name = "Prism (" + vertCount + ")";
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.triangles = triangles.ToArray();

        return mesh;
    }

    private int[] fillSurface(int[] verts, bool clockwise)
    {
        int numVerts = verts.Length;
        int[] triangles = new int[0];

        //Case where we have a square in the previous call
        if (numVerts <= 2)
        {
            return new int[0];
        }

        if(numVerts == 3)
        {

            if (clockwise)
            {
                return new int[]
                {
                    verts[0], verts[2], verts[1]
                };
            }
            else
            {
                return verts;
            }
        }

        int[] unfinished = new int[((numVerts + 1) / 2)];

        for(int i = 0; i < numVerts; i+=2)
        {
            unfinished[i / 2] = verts[i];

            if (i + 1 < numVerts)
            {
                int[] triangle;

                if (clockwise)
                {
                    triangle = new int[]
                    {
                    verts[i], (verts[(i+ 2) % numVerts]), verts[i + 1]
                    };
                } else
                {
                    triangle = new int[]
                    {
                    verts[i], verts[i + 1], (verts[(i + 2) % numVerts])
                    };
                }

                triangles = triangles.Concat(triangle).ToArray();
            }
        }


        return triangles.Concat(fillSurface(unfinished, clockwise)).ToArray();
    }

}
