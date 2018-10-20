using System;

using System.Linq;
using System.Collections.Generic;
using UnityEngine;


public class HexSurface : Renderable {

    //The number of "hex rings" from the center to the edge
    public int hexGridDist = 2;

    protected override Mesh GenerateMesh()
    {
        Mesh m = new Mesh();

        var hexPositions = HexUtility.Spiral(hexGridDist);

        m.vertices = GenerateVertices(hexPositions);
        m.triangles = GenerateTriangles(hexGridDist);

        m.normals = GenerateNormals(m.vertices);

        foreach (var n in m.normals)
        {
            Debug.Log(n);
        }

        return m;
    }

    protected virtual Vector3[] GenerateVertices(List<Vector3Int> hexPositions)
    {
        Vector3[] verts = new Vector3[hexPositions.Count];

        for(int i = 0; i < hexPositions.Count; i++)
        {
            var pos = HexUtility.PixelFromCubeCoord(hexPositions[i], 1f);

            verts[i] = new Vector3(pos.x, 0f, pos.y);
        }

        return verts;
    }

    protected virtual Vector3[] GenerateNormals(Vector3[] verts)
    {
        Vector3[] normals = new Vector3[verts.Length];

        for(int i = 0; i < normals.Length; i++)
        {
            normals[i] = Vector3.up;
            Debug.Log(normals[i]);
        }

        return normals;
    }

    #region Triangles
    protected virtual int[] GenerateTriangles(int dist)
    {

        List<int> triangles = new List<int>();
        
        for(int d = 0; d < dist; d++)
        {
            for(int axis = 0; axis < 6; axis++)
            {
                var innerSide = hexSide(d, axis);
                var outerSide = hexSide(d + 1, axis);

                for(int k = 0; k < innerSide.Length; k++)
                {
                    if(k < innerSide.Length - 1)
                    {
                        triangles.AddRange(new int[]
                        {
                            innerSide[k],
                            outerSide[k + 1],
                            innerSide[k + 1]
                        });
                    }

                    triangles.AddRange(new int[]
                    {
                        innerSide[k],
                        outerSide[k],
                        outerSide[k + 1]
                    });
                }
            }
        }

        return triangles.ToArray();
    }

    private int hexPointIndex(int dist, int axis)
    {
        if(dist == 0)
        {
            return 0;
        }

        int index = 1;

        for(int i = 0; i < dist; i++)
        {
            index += 6 * i;
        }

        index += dist * (axis % 6);

        return index;
    }

    private int[] hexSide(int dist, int axis)
    {
        if (dist == 0)
        {
            return new int[] { 0 };
        }

        int start = hexPointIndex(dist, axis);
        int end = hexPointIndex(dist, axis + 1);

        int[] side = new int[dist + 1];

        for(int i = 0; i < dist; i++)
        {
            side[i] = start + i;
        }

        //Why not loop from start to end? Because end is less than start, 
        //when axis == 5
        side[dist] = end;

        return side;
    }
    #endregion
}
