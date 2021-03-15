using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightSettings;
using LightingSettings;

namespace EventHandling {

    public class Object {
        public List<LightCollider2D> lightignEventCache = new List<LightCollider2D>();
		public List<LightCollision2D> collisions = new List<LightCollision2D>();

		public void Update(Light2D lightingSource, EventPreset eventPreset) {
			if (lightingSource == null) {
				return;
			}

			collisions.Clear();

			// Get Event Receivers
			EventHandling.Collider.GetCollisions(collisions, lightingSource);

			// Remove Event Receiver Vertices with Shadows
			EventHandling.Collider.RemoveHiddenCollisions(collisions, lightingSource, eventPreset);
			EventHandling.Tilemap2D.RemoveHiddenCollisions(collisions, lightingSource, eventPreset);

			if (collisions.Count < 1) {
		
				for(int i = 0; i < lightignEventCache.Count; i++) {
					LightCollider2D collider = lightignEventCache[i];
					
					LightCollision2D collision = new LightCollision2D();
					collision.light = lightingSource;
					collision.collider = collider;
					collision.points = null;
					collision.state = LightEventState.OnCollisionExit;

					collider.CollisionEvent(collision);
				}

				lightignEventCache.Clear();

				return;
			}
				
			List<LightCollider2D> eventColliders = new List<LightCollider2D>();

			foreach(LightCollision2D collision in collisions) {
				eventColliders.Add(collision.collider);
			}

			for(int i = 0; i < lightignEventCache.Count; i++) {
				LightCollider2D collider = lightignEventCache[i];
				if (eventColliders.Contains(collider) == false) {
					
					LightCollision2D collision = new LightCollision2D();
					collision.light = lightingSource;
					collision.collider = collider;
					collision.points = null;
					collision.state = LightEventState.OnCollisionExit;

					collider.CollisionEvent(collision);
					
					lightignEventCache.Remove(collider);
				}
			}

			for(int i = 0; i < collisions.Count; i++) {
				LightCollision2D collision = collisions[i];
				
				if (lightignEventCache.Contains(collision.collider)) {
					collision.state = LightEventState.OnCollision;
				} else {
					collision.state = LightEventState.OnCollisionEnter;
					lightignEventCache.Add(collision.collider);
				}
			
				collision.collider.CollisionEvent(collision);
			}
		}
	}
}