using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightingShape;

[System.Serializable]
public class LightingColliderShape {
	public LightCollider2D.ShadowType shadowType = LightCollider2D.ShadowType.SpritePhysicsShape;
	public LightCollider2D.MaskType maskType = LightCollider2D.MaskType.Sprite;
	public LightCollider2D.MaskPivot maskPivot = LightCollider2D.MaskPivot.TransformCenter;

	[Min(0)]
	public float shadowDistance = 0;

	[Range(0, 1)]
	public float shadowTranslucency = 0;

	public LightingColliderTransform transform2D = new LightingColliderTransform();
	public Transform transform;

	public Collider2DShape collider2DShape = new Collider2DShape();
	public CompositeCollider2DShape compositeShape = new CompositeCollider2DShape();

	public SpriteShape spriteShape = new SpriteShape();
	public SpritePhysicsShape spritePhysicsShape = new SpritePhysicsShape();
	
	public MeshRendererShape meshShape = new MeshRendererShape();
	public SkinnedMeshRendererShape skinnedMeshShape = new SkinnedMeshRendererShape();

	public Collider3DShape collider3DShape = new Collider3DShape();

	public LightingShape.Base GetMaskShape() {
		switch(maskType) {
			case LightCollider2D.MaskType.Sprite:
				return(spriteShape);

			case LightCollider2D.MaskType.BumpedSprite:
				return(spriteShape);

			case LightCollider2D.MaskType.SpritePhysicsShape:
				return(spritePhysicsShape);

			case LightCollider2D.MaskType.CompositeCollider2D:
				return(compositeShape);

			case LightCollider2D.MaskType.Collider2D:
				return(collider2DShape);
				
			case LightCollider2D.MaskType.Collider3D:
				return(collider3DShape);

			case LightCollider2D.MaskType.MeshRenderer:
				return(meshShape);

			case LightCollider2D.MaskType.SkinnedMeshRenderer:
				return(skinnedMeshShape);
		}

		return(null);
	}

	public LightingShape.Base GetShadowShape() {
		switch(shadowType) {
			case LightCollider2D.ShadowType.SpritePhysicsShape:
				return(spritePhysicsShape);

			case LightCollider2D.ShadowType.Collider2D:
				return(collider2DShape);

			case LightCollider2D.ShadowType.Collider3D:
				return(collider3DShape);

			case LightCollider2D.ShadowType.CompositeCollider2D:
				return(compositeShape);

			case LightCollider2D.ShadowType.MeshRenderer:
				return(meshShape);

			case LightCollider2D.ShadowType.SkinnedMeshRenderer:
				return(skinnedMeshShape);
		}

		return(null);
	}
	
	public void SetTransform(Transform setTransform) {
		transform = setTransform;

		transform2D.SetShape(this);

		spriteShape.SetTransform(transform);
		spritePhysicsShape.SetTransform(transform);

		collider2DShape.SetTransform(transform);
		compositeShape.SetTransform(transform);

		meshShape.SetTransform(transform);
		skinnedMeshShape.SetTransform(transform);

		collider3DShape.SetTransform(transform);
	}

	public void ResetLocal() {
		LightingShape.Base shadowShape = GetShadowShape();
		if (shadowShape != null) {
			shadowShape.ResetLocal();
			shadowShape.ResetWorld();
		}

		LightingShape.Base maskShape = GetMaskShape();
		if (maskShape != null) {
			maskShape.ResetLocal();
			maskShape.ResetWorld();
		}
	}

	public void ResetWorld() {
		LightingShape.Base shadowShape = GetShadowShape();
		if (shadowShape != null) {
			shadowShape.ResetWorld();
		}

		LightingShape.Base maskShape = GetMaskShape();
		if (maskShape != null) {
			maskShape.ResetWorld();
		}
	}

	public bool RectOverlap(Rect rect) {
		LightingShape.Base shadowShape = GetShadowShape();
		LightingShape.Base maskShape = GetMaskShape();

		if (maskShape != null && shadowShape != null) {
			if (maskShape == shadowShape) {
				return(shadowShape.GetWorldRect().Overlaps(rect));
			}	
		}

		if (shadowShape != null) {
			bool result = shadowShape.GetWorldRect().Overlaps(rect);

			if (result) {
				return(true);
			}
		}

		if (maskShape != null) {
			bool result = maskShape.GetWorldRect().Overlaps(rect);

			if (result) {
				return(true);
			}
		}

		return(false);
	}

	public Rect GetWorldRect() {
		LightingShape.Base shadowShape = GetShadowShape();
		if (shadowShape != null) {
			return(shadowShape.GetWorldRect());
		}

		LightingShape.Base maskShape = GetMaskShape();
		if (maskShape != null) {
			return(maskShape.GetWorldRect());
		}

		return(new Rect());
	}

	public List<MeshObject> GetMeshes() {
		LightingShape.Base maskShape = GetMaskShape();
		if (maskShape != null) {
			return(maskShape.GetMeshes());
		}

		return(null);
	}

	public List<Polygon2D> GetPolygonsLocal() {
		LightingShape.Base shadowShape = GetShadowShape();
		if (shadowShape != null) {
			return(shadowShape.GetPolygonsLocal());
		}

		return(null);
	}

	public List<Polygon2D> GetPolygonsWorld() {
		LightingShape.Base shadowShape = GetShadowShape();
		if (shadowShape != null) {
			return(shadowShape.GetPolygonsWorld());
		}

		return(null);
	}

	public Vector2 GetPivotPoint() {
		LightingShape.Base shadowShape = GetShadowShape();
		if (shadowShape != null) {
			switch(maskPivot) {
				case LightCollider2D.MaskPivot.TransformCenter:
					return(shadowShape.GetPivotPoint_TransformCenter());

				case LightCollider2D.MaskPivot.ShapeCenter:
					return(shadowShape.GetPivotPoint_ShapeCenter());

				case LightCollider2D.MaskPivot.LowestY:
					return(shadowShape.GetPivotPoint_LowestY());
			}
		}

		return(Vector2.zero);
	}

	public bool IsEdgeCollider() {
		switch(shadowType) {
			case LightCollider2D.ShadowType.Collider2D:
				return(collider2DShape.edgeCollider2D);
		}
		
		return(false);
	}
}