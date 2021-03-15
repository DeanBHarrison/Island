using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rendering.Day {

    public class Sorted {
        static public void Draw(Pass pass) {
            for(int id = 0; id < pass.sortList.count; id++) {
                Sorting.SortObject sortObject = pass.sortList.list[id];

                switch(sortObject.type) {
                    case Sorting.SortObject.Type.Collider:
                        DayLightCollider2D collider = (DayLightCollider2D)sortObject.lightObject;

                        if (collider != null) {


                            if (collider.mainShape.shadowType == DayLightCollider2D.ShadowType.Collider || collider.mainShape.shadowType == DayLightCollider2D.ShadowType.SpritePhysicsShape) {
                                Lighting2D.materials.GetShadowBlur().SetPass (0);
                                GL.Begin(GL.TRIANGLES);
                                    Shadow.Draw(collider, pass.offset);  
                                GL.End(); 
                            }

                            SpriteRendererShadow.Draw(collider, pass.offset);
                
                            SpriteRenderer2D.Draw(collider, pass.offset);

                        }

                    break;
                }
            }
        }
    }
}
