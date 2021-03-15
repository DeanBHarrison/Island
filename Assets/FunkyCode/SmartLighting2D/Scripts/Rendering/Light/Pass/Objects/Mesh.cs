using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rendering.Light {

    public class Mesh {
        
        public static void Mask(Light2D light, LightCollider2D id, Material material, LayerSetting layerSetting) {
			if (id.InLight(light) == false) {
				return;
			}

			foreach(LightingColliderShape shape in id.shapes) {
				MeshRenderer meshRenderer = id.mainShape.meshShape.GetMeshRenderer();
				
				if (meshRenderer == null) {
					return;
				}

				List<MeshObject> meshObjects = id.mainShape.GetMeshes();

				if (meshObjects == null) {
					return;
				}

				if (meshRenderer.sharedMaterial != null) {
					material.mainTexture = meshRenderer.sharedMaterial.mainTexture;
				} else {
					material.mainTexture = null;
				}

				Vector2 position = shape.transform2D.position - light.transform2D.position;

				Vector2 pivotPosition = shape.GetPivotPoint() - light.transform2D.position;
				material.color = LayerSettingColor.Get(pivotPosition, layerSetting, id.maskEffect);

				material.SetPass(0);
			
				GLExtended.DrawMesh(meshObjects, position, id.mainShape.transform2D.scale, shape.transform2D.rotation);
				
				material.mainTexture = null;
			}
		}
    }
}