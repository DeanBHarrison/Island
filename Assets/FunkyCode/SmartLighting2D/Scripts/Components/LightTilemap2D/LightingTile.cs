using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using LightingTilemapCollider;

#if UNITY_2017_4_OR_NEWER

	[System.Serializable]
	public class LightingTile {
		public Vector3Int gridPosition;
		// public TilemapCollider2D or TilemapRoom

		public Vector2? worldPosition = null;
		public float worldRotation = 0;
		public Vector2 worldScale = Vector2.one;
		public float worldRadius = 1.4f;
			
		public Tile.ColliderType colliderType;

		private Sprite originalSprite;

		public Rect uv; // STE
		public Vector2 scale = Vector2.one;
		
		public SpriteMeshObject spriteMeshObject = new SpriteMeshObject();

		private MeshObject shapeMesh = null;

		private SpriteExtension.PhysicsShape spritePhysicsShape = null;
		private List<Polygon2D> spritePhysicsShapePolygons = null;

		private List<Polygon2D> localPolygons = null;
		private List<Polygon2D> worldPolygons = null;
		private List<Polygon2D> worldPolygonsCache = null;

		public void SetOriginalSprite(Sprite sprite) {
			originalSprite = sprite;
		}

		public Sprite GetOriginalSprite() {
			return(originalSprite);
		}

		public bool NotInRange(Vector2 pos, float sourceSize) {
			return(Vector2.Distance(pos, Vector2.zero) > sourceSize + worldRadius);
		}

		public void ResetWorld() {
			worldPolygons = null;
			worldPosition = null;
			worldRotation = 0;
		}

		public void UpdateTransform(LightingTilemapCollider.Base tilemap) {
			if (worldPosition != null) {
				return;
			}

			worldPosition = tilemap.TileWorldPosition(this);
			worldRotation = tilemap.TileWorldRotation(this);
			worldScale = tilemap.TileWorldScale();
		}

		// Remove
		public Vector2 GetWorldPosition(LightingTilemapCollider.Base tilemap) {
			if (worldPosition == null) {
				worldPosition = tilemap.TileWorldPosition(this);
				worldRotation = tilemap.TileWorldRotation(this);
				worldScale = tilemap.TileWorldScale();
			}
			
			return(worldPosition.Value);
		}

		public void SetLocalPolygons(List<Polygon2D> localPolygons) {
			this.localPolygons = localPolygons;
		}

		public List<Polygon2D> GetWorldPolygons(LightingTilemapCollider.Base tilemap) {
			if (worldPolygons == null) {
				List<Polygon2D> localPolygons = GetLocalPolygons(tilemap);
				if (worldPolygonsCache == null) {

					worldPolygons = new List<Polygon2D>();
					worldPolygonsCache = worldPolygons;
					
					UpdateTransform(tilemap);
	
					foreach(Polygon2D polygon in localPolygons) {
						Polygon2D worldPolygon = polygon.Copy();

						if (scale != Vector2.one) {
							worldPolygon.ToScaleItself(scale);
						}

						worldPolygon.ToScaleItself(worldScale);
						worldPolygon.ToRotationItself(tilemap.transform.eulerAngles.z * Mathf.Deg2Rad);
						worldPolygon.ToOffsetItself(worldPosition.Value);

						worldPolygons.Add(worldPolygon);
					}
					
				} else {
					worldPolygons = worldPolygonsCache;

					UpdateTransform(tilemap);

					for(int i = 0; i < localPolygons.Count; i++) {
						Polygon2D polygon = localPolygons[i];
						Polygon2D worldPolygon = worldPolygons[i];

						for(int j = 0; j < polygon.pointsList.Count; j++) {
							worldPolygon.pointsList[j].x = polygon.pointsList[j].x;
							worldPolygon.pointsList[j].y = polygon.pointsList[j].y;
						}

						if (scale != Vector2.one) {
							worldPolygon.ToScaleItself(scale);
						}

						worldPolygon.ToScaleItself(worldScale);
						worldPolygon.ToRotationItself(tilemap.transform.eulerAngles.z * Mathf.Deg2Rad);
						worldPolygon.ToOffsetItself(worldPosition.Value);

					
					}

						
				}
			}

			return(worldPolygons);
		}

		public List<Polygon2D> GetLocalPolygons(LightingTilemapCollider.Base tilemap) {
			if (localPolygons == null) {

				if (tilemap.IsPhysicsShape()) {

					List<Polygon2D> customShapePolygons = GetPhysicsShapePolygons();
				
					if (customShapePolygons.Count > 0) {
						localPolygons = customShapePolygons;
					} else {
						localPolygons = new List<Polygon2D>();
					}

				} else {
					localPolygons = new List<Polygon2D>();

					switch(tilemap.TilemapType()) {
						case MapType.UnityRectangle:
						case MapType.SuperTilemapEditor:

							localPolygons.Add(Polygon2D.CreateRect(Vector2.one));

						break;

						case MapType.UnityIsometric:

							localPolygons.Add(Polygon2D.CreateIsometric(new Vector2(1, 0.5f)));

						break;

						case MapType.UnityHexagon:

							localPolygons.Add(Polygon2D.CreateHexagon(new Vector2(1, 0.5f)));

						break;

					}
				}
			}
			return(localPolygons);
		}

		public List<Polygon2D> GetPhysicsShapePolygons() {
			if (spritePhysicsShapePolygons == null) {

				spritePhysicsShapePolygons = new List<Polygon2D>();

				if (originalSprite == null) {
					return(spritePhysicsShapePolygons);
				}

				#if UNITY_2017_4_OR_NEWER
					if (spritePhysicsShape == null) {
						spritePhysicsShape = SpriteExtension.PhysicsShapeManager.RequesCustomShape(originalSprite);
					}

					if (spritePhysicsShape != null) {
						spritePhysicsShapePolygons = spritePhysicsShape.Get();
					}
				#endif
			}

			return(spritePhysicsShapePolygons);
		}

		public MeshObject GetDynamicMesh() {
			if (shapeMesh == null) {
				if (spritePhysicsShapePolygons != null && spritePhysicsShapePolygons.Count > 0) {
					shapeMesh = spritePhysicsShape.GetMesh();
				}
			}
			return(shapeMesh);
		}





		public static MeshObject GetStaticMesh(LightingTilemapCollider.Base tilemap) {
			switch(tilemap.TilemapType()) {
				case MapType.UnityRectangle:
					return(Rectangle.GetStaticMesh());

				case MapType.UnityIsometric:
					return(Isometric.GetStaticMesh());

				case MapType.UnityHexagon:
					return(Hexagon.GetStaticMesh());
			}

			return(null);
		}




		public class Rectangle {
			public static MeshObject meshObject = null;

			public static MeshObject GetStaticMesh() {
				if (meshObject == null) {
					// Can be optimized?
					Mesh mesh = new Mesh();

					float x = 0.5f;
					float y = 0.5f;

					mesh.vertices = new Vector3[]{new Vector2(-x, -y), new Vector2(x, -y), new Vector2(x, y), new Vector2(-x, y)};
					mesh.triangles = new int[]{0, 1, 2, 2, 3, 0};
					mesh.uv = new Vector2[]{new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1)};
			
			
					meshObject = MeshObject.Get(mesh);	
				}
				return(meshObject);
			}
		}

		public static class Isometric {
			private static MeshObject meshObject = null;

			public static MeshObject GetStaticMesh() {
				if (meshObject == null) {
					Mesh mesh = new Mesh();

					float x = 0.5f;
					float y = 0.5f;

					mesh.vertices = new Vector3[]{new Vector2(0, y), new Vector2(x, y / 2), new Vector2(0, 0), new Vector2(-x, y / 2)};
					mesh.triangles = new int[]{0, 1, 2, 2, 3, 0};
					mesh.uv = new Vector2[]{new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1)};
			
					meshObject = MeshObject.Get(mesh);		
				}
				return(meshObject);
			}
		}

		public static class Hexagon {
			private static MeshObject meshObject = null;

			public static MeshObject GetStaticMesh() {
				if (meshObject == null) {
					Mesh mesh = new Mesh();

					float x = 0.5f ;
					float y = 0.5f;

					float yOffset = - 0.25f;
					mesh.vertices = new Vector3[]{new Vector2(0, y * 1.5f + yOffset), new Vector2(x, y + yOffset), new Vector2(0, -y * 0.5f + yOffset), new Vector2(-x, y + yOffset), new Vector2(-x, 0 + yOffset), new Vector2(x, 0 + yOffset)};
					mesh.triangles = new int[]{0, 1, 5, 4, 3, 0, 0, 5, 2, 0, 2, 4};
					mesh.uv = new Vector2[]{new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1), new Vector2(0, 1),  new Vector2(0, 1) };
			
			
					meshObject = MeshObject.Get(mesh);	
				}
				return(meshObject);
			}
		}

		public static class STE {
			private static MeshObject meshObject = null;

			public static MeshObject GetStaticMesh() {
				if (meshObject == null) {
					Mesh mesh = new Mesh();

					float x = 0.5f;
					float y = 0.5f;

					mesh.vertices = new Vector3[]{new Vector2(-x, -y), new Vector2(x, -y), new Vector2(x, y), new Vector2(-x, y)};
					mesh.triangles = new int[]{0, 1, 2, 2, 3, 0};
					mesh.uv = new Vector2[]{new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1)};

					meshObject = MeshObject.Get(mesh);	
				}

				return(meshObject);
			}
		}
	}

#endif