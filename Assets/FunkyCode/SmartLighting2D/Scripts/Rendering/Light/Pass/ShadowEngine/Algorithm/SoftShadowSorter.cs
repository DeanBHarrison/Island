using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoftShadowSorter {

	public static Polygon2D polygon;

	public static Light2D light;

	public static Vector2 center;

	public static float[] direction = new float[1000];

	public static Vector2D minPoint;
	public static Vector2D maxPoint;

	public static void Set(Polygon2D poly, Light2D light2D) {
		polygon = poly;
		
		light = light2D;

		Vector2 lightPosition = -light.transform2D.position;

		center.x = 0;
		center.y = 0;

		foreach(Vector2D p in polygon.pointsList) {
			center.x += (float)p.x + lightPosition.x;
			center.y += (float)p.y + lightPosition.y;
		}

		center.x /= polygon.pointsList.Count;
		center.y /= polygon.pointsList.Count;

		float centerDirection = Mathf.Atan2(center.x, center.y) * Mathf.Rad2Deg;

		centerDirection = (centerDirection + 720) % 360 + 180;

		foreach(Vector2D p in polygon.pointsList) {
			int id = polygon.pointsList.IndexOf(p);

			float dir = Mathf.Atan2((float)p.x + lightPosition.x, (float)p.y + lightPosition.y) * Mathf.Rad2Deg;
			dir = (dir + 720 - centerDirection) % 360;
			
			direction[id] = dir;
		}


		float min = 10000;
		float max = -10000;
	
		foreach(Vector2D p in polygon.pointsList) {
			int id = polygon.pointsList.IndexOf(p);

			if (direction[id] < min) {
				min = direction[id];
				minPoint = p;
			}

			if (direction[id] > max) {
				max = direction[id];
				maxPoint = p;
			}
		}
	}
}