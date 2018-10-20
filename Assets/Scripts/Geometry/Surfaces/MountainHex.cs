using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MountainHex : HexSurface {

    public float heightFactor = 1f;
    public float noiseSpaceStep = 1f;

    protected override Vector3[] GenerateVertices(List<Vector3Int> hexPositions)
    {
        var verts = base.GenerateVertices(hexPositions);

        float perlinOffsetX = Random.Range(-10000f, 10000f);
        float perlinOffsetY = Random.Range(-10000f, 10000f);

        for(int i = 0; i < verts.Length; i++)
        {
            Vector2 pixel = HexUtility.PixelFromCubeCoord(hexPositions[i], 1f);

            float height = heightFactor * Mathf.PerlinNoise(perlinOffsetX + pixel.x * noiseSpaceStep, perlinOffsetY + pixel.y * noiseSpaceStep);

            verts[i] = new Vector3(verts[i].x, height, verts[i].z);
        }

        return verts;
    }

    protected override Vector3[] GenerateNormals(List<Vector3Int> hexPositions, Vector3[] verts)
    {

        var normals = base.GenerateNormals(hexPositions, verts);

        //Foreach vert
        for(int i = 0; i < verts.Length; i++)
        {
            Vector3 newNormal = Vector3.zero;

            //Current position in hex space
            var hexPos = hexPositions[i];
            float vertHeight = verts[i].y;
            float dist = HexUtility.DistanceBetweenNeighbours();

            float theta = 0;

            for (int dir = 0; dir < 3; dir++)
            {
                //Get opposite neighbours
                var nei1Index = hexPositions.IndexOf(HexUtility.Neighbour(hexPos, dir));
                var nei2Index = hexPositions.IndexOf(HexUtility.Neighbour(hexPos, dir + 3));

                //What do we do at the edges?
                if(nei1Index < 0 || nei2Index < 0)
                {
                    //For now, we lleave the vector as Up
                    continue;
                }

                //Get neigbours' height
                float nei1Height = verts[nei1Index].y;
                float nei2Height = verts[nei2Index].y;

                //Interpolate
                var interpolation = Calculus.LagrangeInterpolate(new Dictionary<float, float>()
                {
                    { (-dist), nei1Height },
                    { 0, vertHeight },
                    { dist, nei2Height },
                });

                //Get gradient
                float m = Calculus.TakeDerivative(interpolation, 0);

                //Find perpendicular line
                float perp_m = -(1 / m);

                //Convert the normal to 3d world space by rotating it about the y axis
                Vector4 normalComponent = new Vector2(perp_m, 1f).normalized;

                Matrix4x4 rotY = new Matrix4x4(
                    new Vector4(Mathf.Cos(theta), 0, -Mathf.Sin(theta), 0),
                    new Vector4(               0, 1,                 0, 0),
                    new Vector4(Mathf.Cos(theta), 0,  Mathf.Sin(theta), 0),
                    new Vector4(               0, 0,                 0, 1)
                );

                Vector4 n = rotY * normalComponent;

                newNormal.x = n.x;
                newNormal.y = n.y;
                newNormal.z = n.z;

                //Find theta - the angle between the world space and our interpolation space
                //We know that the orientation of the hexagon, so let's use that to our advantage
                theta += (Mathf.PI / 3);
            }

            normals[i] = newNormal;
        }

        return normals;
    }
}
