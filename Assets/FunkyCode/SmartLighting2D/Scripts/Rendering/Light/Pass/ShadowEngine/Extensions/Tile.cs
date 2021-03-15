using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rendering.Light.Shadow {

    public class Tile {
        
        static public void Draw(Light2D light, LightingTile tile, LightTilemapCollider2D tilemap) {
            LightingTilemapCollider.Base tilemapCollider = tilemap.GetCurrentTilemap();

            List<Polygon2D> polygons = tile.GetWorldPolygons(tilemapCollider);

            ShadowEngine.Draw(polygons, 0, 0);
        }  
    }
}