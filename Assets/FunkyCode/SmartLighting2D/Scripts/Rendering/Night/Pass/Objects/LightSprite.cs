using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rendering.Night {
	
    public class LightSprite {

		static public void Draw(LightSprite2D id, Camera camera) {
			Material material;

			if (id.GetSprite() == null) {
				return;
			}

			if (id.InCamera(camera) == false) {
				return;
			}

			Vector2 offset = -camera.transform.position;

			Vector2 position = id.transform.position;

			Vector2 scale = id.transform.lossyScale;
			scale.x *= id.lightSpriteTransform.scale.x;
			scale.y *= id.lightSpriteTransform.scale.y;

			float rot = id.lightSpriteTransform.rotation;
			if (id.lightSpriteTransform.applyRotation) {
				rot += id.transform.rotation.eulerAngles.z;
			}

			switch(id.type) {
				case LightSprite2D.Type.Light: 

					Color color = id.color;

					material = Lighting2D.materials.GetAdditive();
					material.SetColor ("_TintColor", color);

					material.mainTexture = id.GetSprite().texture;
					Universal.Sprite.FullRect.Simple.Draw(id.spriteMeshObject, material, id.spriteRenderer, offset + position + id.lightSpriteTransform.position, scale, rot);
					material.mainTexture = null;

					break;

				case LightSprite2D.Type.Mask:

					material = Lighting2D.materials.GetMask();
					
					material.mainTexture = id.GetSprite().texture;
					material.color = id.color;
					Universal.Sprite.FullRect.Simple.Draw(id.spriteMeshObject, material, id.spriteRenderer, offset + position + id.lightSpriteTransform.position, scale, rot);
					material.mainTexture = null;
					material.color = Color.white;
				
					break;
			}
		}
	}
}