﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightingTilemapCollider;
using LightingSettings;

public class DayLightingTile {
  public List<Polygon2D> polygons;

  public DayLighting.TilemapShadowMesh shadowMesh;

  public float height = 1;
}

[ExecuteInEditMode]
public class DayLightTilemapCollider2D : MonoBehaviour {

    public MapType tilemapType = MapType.UnityRectangle;

    public int shadowLayer = 0;

	public ShadowTileType shadowTileType = ShadowTileType.AllTiles;

	public float height = 1;

    public int maskLayer = 0;

	public DayLightingTilemapColliderTransform transform2D = new DayLightingTilemapColliderTransform();
	
    public static List<DayLightTilemapCollider2D> list = new List<DayLightTilemapCollider2D>();

    public Rectangle rectangle = new Rectangle();
	public Isometric isometric = new Isometric();
	public Hexagon hexagon = new Hexagon();

    public SuperTilemapEditorSupport.TilemapCollider2D superTilemapEditor = new SuperTilemapEditorSupport.TilemapCollider2D();

    public List<DayLightingTile> dayTiles = new List<DayLightingTile>();

	static public List<DayLightTilemapCollider2D> GetList() {
		return(list);
	}

    public void OnEnable() {
		list.Add(this);

		rectangle.SetGameObject(gameObject);
		isometric.SetGameObject(gameObject);
		hexagon.SetGameObject(gameObject);

		superTilemapEditor.eventsInit = false;
		superTilemapEditor.SetGameObject(gameObject);

		LightingManager2D.Get();

		Initialize();
    }

	public void OnDisable() {
		list.Remove(this);
	}

	void Update() {
		transform2D.Update(this);

		if (transform2D.moved) {
			transform2D.moved = false;

			foreach(DayLightingTile dayTile in dayTiles) {
				dayTile.height = height;
				dayTile.shadowMesh.Generate(dayTile);
			}
		}
	}

	public LightingTilemapCollider.Base GetCurrentTilemap() {
		switch(tilemapType) {
			case MapType.SuperTilemapEditor:
				return(superTilemapEditor);
			case MapType.UnityRectangle:
				return(rectangle);
			case MapType.UnityIsometric:
				return(isometric);
			case MapType.UnityHexagon:
				return(hexagon);
		}
		return(null);
	}

	public void Initialize() {
		TilemapEvents.Initialize();

		GetCurrentTilemap().Initialize();

		dayTiles.Clear();

		switch(tilemapType) {
			case MapType.SuperTilemapEditor:

				switch(superTilemapEditor.shadowTypeSTE) {
					case SuperTilemapEditorSupport.TilemapCollider.ShadowType.Grid:
					case SuperTilemapEditorSupport.TilemapCollider.ShadowType.TileCollider:
						foreach(LightingTile tile in GetTileList()) {
						DayLightingTile dayTile = new DayLightingTile();
						dayTile.shadowMesh = new DayLighting.TilemapShadowMesh();
						dayTile.height = height;

						dayTile.polygons = tile.GetWorldPolygons(GetCurrentTilemap());

						dayTiles.Add(dayTile);

						dayTile.shadowMesh.Generate(dayTile);
					}
					break;

						#if (SUPER_TILEMAP_EDITOR)
					case SuperTilemapEditorSupport.TilemapCollider.ShadowType.Collider:
					
							foreach(Polygon2D polygon in superTilemapEditor.GetWorldColliders()) {
								DayLightingTile dayTile = new DayLightingTile();
								dayTile.shadowMesh = new DayLighting.TilemapShadowMesh();
								dayTile.height = height;

								dayTile.polygons = new List<Polygon2D>();
								
								Polygon2D poly = polygon.Copy();
								poly.ToOffsetItself(transform.position);

								dayTile.polygons.Add(poly);
							
								dayTiles.Add(dayTile);

								dayTile.shadowMesh.Generate(dayTile);
							}

						
					break;
					#endif
				}

				
			break;

			case MapType.UnityRectangle:

				switch(rectangle.shadowType) {
					case LightingTilemapCollider.ShadowType.Grid:
					case LightingTilemapCollider.ShadowType.SpritePhysicsShape:
						foreach(LightingTile tile in GetTileList()) {
							DayLightingTile dayTile = new DayLightingTile();
							dayTile.shadowMesh = new DayLighting.TilemapShadowMesh();
							dayTile.height = height;

							dayTile.polygons = tile.GetWorldPolygons(GetCurrentTilemap());

							dayTiles.Add(dayTile);

							dayTile.shadowMesh.Generate(dayTile);
						}

					break;

					case LightingTilemapCollider.ShadowType.CompositeCollider:
						foreach(Polygon2D polygon in rectangle.compositeColliders) {
							DayLightingTile dayTile = new DayLightingTile();
							dayTile.shadowMesh = new DayLighting.TilemapShadowMesh();
							dayTile.height = height;

							dayTile.polygons = new List<Polygon2D>();
							
							Polygon2D poly = polygon.Copy();
							poly.ToOffsetItself(transform.position);

							dayTile.polygons.Add(poly);
						
							dayTiles.Add(dayTile);

							dayTile.shadowMesh.Generate(dayTile);
						}
					break;
				}
				
			break;
		}

		
	}

    public List<LightingTile> GetTileList() {
		return(GetCurrentTilemap().mapTiles);
	}

    public TilemapProperties GetTilemapProperties() {
		return(GetCurrentTilemap().Properties);
	}

    void OnDrawGizmosSelected() {
		if (Lighting2D.ProjectSettings.sceneView.drawGizmos != EditorDrawGizmos.Selected) {
			return;
		}
		
		DrawGizmos();
	}

	private void OnDrawGizmos() {
		if (Lighting2D.ProjectSettings.sceneView.drawGizmos != EditorDrawGizmos.Always) {
			return;
		}

		DrawGizmos();
	}

	private void DrawGizmos() {
		if (isActiveAndEnabled == false) {
			return;
		}

		Gizmos.color = new Color(1f, 0.5f, 0.25f);

		LightingTilemapCollider.Base tilemap = GetCurrentTilemap();

		foreach(DayLightingTile dayTile in dayTiles) {
			GizmosHelper.DrawPolygons(dayTile.polygons, transform.position);
		}

		Gizmos.color = new Color(0, 1f, 1f);

		switch(Lighting2D.ProjectSettings.sceneView.drawGizmosBounds) {
			case EditorGizmosBounds.Rectangle:
				GizmosHelper.DrawRect(transform.position, tilemap.GetRect());
			break;
		}
	}
}
