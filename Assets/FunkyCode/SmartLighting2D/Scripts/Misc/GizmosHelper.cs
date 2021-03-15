using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightingSettings;

public static class GizmosHelper {

	static public void DrawRect(Vector3 position, Rect rect) {
		Vector3 p0 = LightingPosition.GetPosition3DWorld(new Vector2(rect.x, rect.y), position);
		Vector3 p1 = LightingPosition.GetPosition3DWorld(new Vector2(rect.x + rect.width, rect.y), position);
		Vector3 p2 = LightingPosition.GetPosition3DWorld(new Vector2(rect.x + rect.width, rect.y + rect.height), position);
		Vector3 p3 = LightingPosition.GetPosition3DWorld(new Vector2(rect.x, rect.y + rect.height), position);

        Gizmos.DrawLine(p0, p1);
		Gizmos.DrawLine(p1, p2);
		Gizmos.DrawLine(p2, p3);
		Gizmos.DrawLine(p3, p0);
    }

    static public void DrawCircle(Vector3 position, float rotation, float angle, float size) {
        Vector3 center = position;
		int step = 10;

		int start = -(int)(angle / 2);
		int end = (int)(angle / 2);

		for(int i = start; i < end; i += step) {
			float rot = i + 90 + rotation;

			float rotA = rot * Mathf.Deg2Rad;
			float rotB = (rot + step) * Mathf.Deg2Rad;

			Vector3 pointA = LightingPosition.GetPosition3D(new Vector2(Mathf.Cos(rotA) * size, Mathf.Sin(rotA) * size), center);
			Vector3 pointB = LightingPosition.GetPosition3D(new Vector2(Mathf.Cos(rotB) * size, Mathf.Sin(rotB) * size), center);

			Gizmos.DrawLine(pointA, pointB);

			if (angle < 360 && angle > 0) {
				if (i == start) {
					Gizmos.DrawLine(pointA, center);
				}

				if (i + step > end) {
					Gizmos.DrawLine(pointB, center);
				}
			}
		}
    }

    static public void DrawPolygons(List<Polygon2D> polygons, Vector3 position) {
		if (polygons == null) {
			return;
		}
		
        foreach(Polygon2D polygon in polygons) {
            DrawPolygon(polygon, position);
        }
    }

	static public void DrawPolygon(Polygon2D polygon, Vector3 position) {
		Vector3 a = Vector3.zero;
		Vector3 b = Vector3.zero;

		for(int i = 0; i < polygon.pointsList.Count; i++) {

			Vector2D p0 = polygon.pointsList[i];
			Vector2D p1 = polygon.pointsList[(i + 1) % polygon.pointsList.Count];

			a = LightingPosition.GetPosition3DWorld(p0.ToVector2(), position);
			b = LightingPosition.GetPosition3DWorld(p1.ToVector2(), position);

			Gizmos.DrawLine(a, b);
		}
    }
}