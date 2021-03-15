﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LightingShape {
		
	public class Collider2DShape : Base {
		public bool edgeCollider2D = false;
				
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

		public override List<Polygon2D> GetPolygonsLocal() {
			if (polygons_local != null) {
				return(polygons_local);
			}

			if (transform == null) {
				return(polygons_local);
			}
			
			polygons_local = Polygon2DListCollider2D.CreateFromGameObject(transform.gameObject);

			if (polygons_local.Count > 0) {

				edgeCollider2D = (transform.GetComponent<EdgeCollider2D>() != null);

			//} else {
				//Debug.LogWarning("SmartLighting2D: LightingCollider2D object is missing Collider2D Component", transform);
			}
		
			return(polygons_local);
		}

		public override List<Polygon2D> GetPolygonsWorld() {
	
			if (polygons_world != null) {
				return(polygons_world);
			}

			if (polygons_world_cache != null) {

				polygons_world = polygons_world_cache;

				Polygon2D poly;
				Polygon2D wPoly;
				
				Vector2D point;
				Vector2D wPoint;

				List<Polygon2D> list = GetPolygonsLocal();

				for(int i = 0; i < list.Count; i++) {
					poly = list[i];
					wPoly = polygons_world[i];

					for(int p = 0; p < poly.pointsList.Count; p++) {
						point = poly.pointsList[p];
						wPoint = wPoly.pointsList[p];
						
						wPoint.x = point.x;
						wPoint.y = point.y;
					}

					wPoly.ToWorldSpaceItself(transform);
				}

			} else {
				polygons_world = new List<Polygon2D>();

				if ( GetPolygonsLocal() != null) {
					foreach(Polygon2D poly in GetPolygonsLocal()) {
						polygons_world.Add(poly.ToWorldSpace(transform));
					}
				}
		
				polygons_world_cache = polygons_world;
			}
		
			return(polygons_world);
		}

	}
}