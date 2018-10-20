using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeometryUtility {

    public static Vector2[] PointsAboutCircle(int numIncrements)
    {
        var corners = new Vector2[numIncrements];

        for (int a = 0; a < numIncrements; a++)
        {
            float angle = ((2f * Mathf.PI) * (a / (float)numIncrements));

            corners[a] = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }

        return corners;
    }
}
