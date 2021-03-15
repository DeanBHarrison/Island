using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LightingShape {
		
	public class Base {
		public Vector2? worldPointPoint = null;

		public List<Polygon2D> polygons_world = null;
		public List<Polygon2D> polygons_world_cache = null;

		public List<Polygon2D> polygons_local = null;

		public List<MeshObject> meshes = null;
	
		public Rect worldRect;

		public Transform transform;

		public void SetTransform(Transform transform) {
			this.transform = transform;
		}

		virtual public List<Polygon2D> GetPolygonsLocal() {
			return(polygons_local);
		}

		virtual public List<Polygon2D> GetPolygonsWorld() {
			return(polygons_world);
		}

		virtual public void ResetLocal() {
			meshes = null;

			polygons_local = null;

			polygons_world = null;
			polygons_world_cache = null;

			ResetWorld();
		}

		virtual public void ResetWorld() {
			polygons_world = null;

			worldRect = new Rect();

			worldPointPoint = null;
		}

		public Rect GetWorldRect() {
			if (worldRect.width < 0.01f) {
				worldRect = Polygon2DHelper.GetRect(GetPolygonsWorld());
			}

			return(worldRect);
		}




		
		public Vector2 GetPivotPoint_ShapeCenter() {
			if (worldPointPoint == null) {

				List<Polygon2D> polys = GetPolygonsWorld();

				worldPointPoint = GetWorldRect().center;
			}

			return(worldPointPoint.Value);
		}

		public Vector2 GetPivotPoint_TransformCenter() {
			if (worldPointPoint == null) {
				worldPointPoint = transform.position;
			}

			return(worldPointPoint.Value);
		}

		public Vector2 GetPivotPoint_LowestY() {
			if (worldPointPoint == null) {

				List<Polygon2D> polys = GetPolygonsWorld();

				Vector2 lowestPoint = Vector2.zero;
				lowestPoint.y = 999999;

				foreach(Polygon2D poly in polys) {
					foreach(Vector2D p in poly.pointsList) {
						if (p.y < lowestPoint.y) {
							lowestPoint = p.ToVector2();
						}
					}
				}
				worldPointPoint = lowestPoint;
			}

			return(worldPointPoint.Value);
		}

		public virtual List<MeshObject> GetMeshes() {
			return(null);
		}

	}
}