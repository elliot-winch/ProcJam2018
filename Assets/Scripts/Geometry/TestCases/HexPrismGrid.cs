using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Test laying out a hex prism grid using functions from HexUtility
/// </summary>
public class HexPrismGrid : MonoBehaviour
{
    public float size;
    public Prism tile;

    private void Awake()
    {
        var hexGrid = HexUtility.Spiral(6);

        Render(hexGrid);
    }

    public void Render(List<Vector3Int> hexes)
    {
        foreach (var h in hexes)
        {
            var pos = HexUtility.PixelFromCubeCoord(h, size);

            var prism = Instantiate(tile, new Vector3(pos.x, 0f, pos.y), Quaternion.identity);

            prism.size = size;
            prism.Render();
        }
    }
}