﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LightingShape {

    public class SpritePhysicsShape : Base {
        
        private Sprite sprite;

        public SpriteExtension.PhysicsShape physicsShape = null;

        private SpriteRenderer spriteRenderer;

        override public void ResetLocal() {
            base.ResetLocal();
            
            physicsShape = null;

			sprite = null;
        }

        public Sprite GetOriginalSprite() {
            if (sprite == null) {
                GetSpriteRenderer();

                if (spriteRenderer != null) {
                    sprite = spriteRenderer.sprite;
                }
            }
			return(sprite);
		}
        
		public SpriteRenderer GetSpriteRenderer() {
			if (transform == null) {
				return(spriteRenderer);
			}

			if (spriteRenderer == null) {
				spriteRenderer = transform.GetComponent<SpriteRenderer>();
			}

			return(spriteRenderer);
		}

        public SpriteExtension.PhysicsShape GetPhysicsShape() {
			if (physicsShape == null) {
                Sprite sprite = GetOriginalSprite();

                if (sprite != null) {
                    physicsShape = SpriteExtension.PhysicsShapeManager.RequesCustomShape(sprite);
                }
			}
			return(physicsShape);
		}

		public override List<MeshObject> GetMeshes() {
			if (meshes == null) {
				List<Polygon2D> polygons = GetPolygonsLocal();

				if (polygons.Count > 0) {
					meshes = new List<MeshObject>();
					foreach(Polygon2D poly in polygons) {
						if (poly.pointsList.Count < 3) {
							continue;
						}

						Mesh mesh = PolygonTriangulator2D.Triangulate (poly, Vector2.zero, Vector2.zero, PolygonTriangulator2D.Triangulation.Advanced);
						if (mesh) {

							MeshObject meshObject = MeshObject.Get(mesh);

							if (meshObject != null) {
								meshes.Add(meshObject);
							}
						}
						
					}
				}
			}
			return(meshes);
		}

		public override List<Polygon2D> GetPolygonsWorld() {
			if (polygons_world != null) {
				return(polygons_world);
			}

			Vector2 scale = new Vector2();

			List<Polygon2D> localPolygons = GetPolygonsLocal();
		
			if (polygons_world_cache != null) {

				polygons_world = polygons_world_cache;

				Polygon2D poly;
				Polygon2D wPoly;

				Vector2D point;
				Vector2D Wpoint;

				SpriteRenderer spriteRenderer = GetSpriteRenderer();

				for(int i = 0; i < localPolygons.Count; i++) {
					poly = localPolygons[i];
					wPoly = polygons_world[i];
					
					for(int p = 0; p < poly.pointsList.Count; p++) {
						point = poly.pointsList[p];
						Wpoint = wPoly.pointsList[p];
						
						Wpoint.x = point.x;
						Wpoint.y = point.y;
					}

					if (spriteRenderer != null) {
				
						if (spriteRenderer.flipX != false || spriteRenderer.flipY != false) {

							scale.x = 1;
							scale.y = 1;

							if (spriteRenderer.flipX == true) {
								scale.x = -1;
							}

							if (spriteRenderer.flipY == true) {
								scale.y = -1;
							}
						
							wPoly.ToScaleItself(scale);
						}
					}

					wPoly.ToWorldSpaceItselfUNIVERSAL(transform);
				}
			} else {
			
				Polygon2D polygon;

				polygons_world = new List<Polygon2D>();

				SpriteRenderer spriteRenderer = GetSpriteRenderer();

				foreach(Polygon2D poly in localPolygons) {
					polygon = poly.Copy();

					if (spriteRenderer != null) {

						if (spriteRenderer.flipX != false || spriteRenderer.flipY != false) {

							scale.x = 1;
							scale.y = 1;

							if (spriteRenderer.flipX == true) {
								scale.x = -1;
							}

							if (spriteRenderer.flipY == true) {
								scale.y = -1;
							}
						
							polygon.ToScaleItself(scale);
						}
					}
					
					polygon.ToWorldSpaceItselfUNIVERSAL(transform);

					polygons_world.Add(polygon);
				}

				polygons_world_cache = polygons_world;
			}

			return(polygons_world);
		}

		public override List<Polygon2D> GetPolygonsLocal() {
			if (polygons_local != null) {
				return(polygons_local);
			}

			polygons_local = new List<Polygon2D>();

			#if UNITY_2017_4_OR_NEWER
			
				if (physicsShape == null) {

					if (GetOriginalSprite() == null) {
						return(polygons_local);
					}

					physicsShape = GetPhysicsShape();
				}

				polygons_local = physicsShape.Get();

			#endif

			return(polygons_local);
		}
    }
}