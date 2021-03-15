using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
namespace LightingShape {
		
	public class Collider3DShape : Base {
		public bool edgeCollider2D = false;
				
		public override List<MeshObject> GetMeshes() {
			if (meshes == null) {
				List<Polygon2D> polygons = GetPolygonsLocal();

				if (polygons == null) {
					return(null);
				}

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

		public override List<Polygon2D> GetPolygonsLocal()  {
			if (polygons_local != null) {
				return(polygons_local);
			}

			if (transform == null) {
				return(polygons_local);
			}

			polygons_local = Polygon2DListCollider3D.CreateFromGameObject(transform.gameObject);

			return(null);
		}

		public override List<Polygon2D> GetPolygonsWorld() {
			if (polygons_world != null) {
				return(polygons_world);
			}

			polygons_world = new List<Polygon2D>();

			if ( GetPolygonsLocal() != null) {
				foreach(Polygon2D poly in GetPolygonsLocal()) {
					polygons_world.Add(poly.ToWorldSpace(transform));
				}
			}
			
			return(polygons_world);
		}
	}
}