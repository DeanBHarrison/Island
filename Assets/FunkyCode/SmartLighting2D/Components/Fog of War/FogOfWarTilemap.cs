using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightingTilemapCollider;
using LightingSettings;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class FogOfWarTilemap : MonoBehaviour {
    public MapType mapType = MapType.UnityRectangle;  

    public SuperTilemapEditorSupport.TilemapRoom2D superTilemapEditor = new SuperTilemapEditorSupport.TilemapRoom2D();
    public Rectangle rectangle = new Rectangle();

    //public LighitngTilemapRoomTransform lightingTransform = new LighitngTilemapRoomTransform();

    public static List<FogOfWarTilemap> List = new List<FogOfWarTilemap>();

    public TilemapRenderer tilemapRenderer = null;

    public Material material;

      public TilemapRenderer GetTilemapRenderer() {
        if (tilemapRenderer == null) {
            tilemapRenderer = GetComponent<TilemapRenderer>();
        }

        material =  tilemapRenderer.sharedMaterial;
        return(tilemapRenderer);
    }

    public void OnEnable() {
        List.Add(this);

        LightingManager2D.Get();

        rectangle.SetGameObject(gameObject);
        superTilemapEditor.SetGameObject(gameObject);

        Initialize();
    }

    public void OnDisable() {
        List.Remove(this);
    }

    public LightingTilemapCollider.Base GetCurrentTilemap() {
        switch(mapType) {
            case MapType.SuperTilemapEditor:
                return(superTilemapEditor);
            case MapType.UnityRectangle:
                return(rectangle);

        }
        return(null);
    }

    public void Initialize() {
        TilemapEvents.Initialize();
        
        GetCurrentTilemap().Initialize();


    }

    public void Update() {
        //lightingTransform.Update(this);

       // if (lightingTransform.UpdateNeeded) {

       //     GetCurrentTilemap().ResetWorld();

       //     Light2D.ForceUpdateAll();
       // }

        TilemapRenderer tilemapRenderer = GetTilemapRenderer();

        if (tilemapRenderer == null) {
            return;
        }

        LightingManager2D manager = LightingManager2D.Get();
    
        if (manager.fogOfWarCameras.Length > 0) {
            if (Lighting2D.FogOfWar.useOnlyInPlay) {
                if (Application.isPlaying) {
                    tilemapRenderer.enabled = false;
                } else {
                    tilemapRenderer.enabled = true;
                }
            } else {
                tilemapRenderer.enabled = false;
            }
        } else {
            tilemapRenderer.enabled = true;
        }
        
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

        // Gizmos.color = new Color(1f, 0.5f, 0.25f);

        Gizmos.color = new Color(1f, 0f, 1f);

        switch(Lighting2D.ProjectSettings.sceneView.drawGizmosBounds) {
            case EditorGizmosBounds.Rectangle:
                GizmosHelper.DrawRect(transform.position, GetCurrentTilemap().GetRect());
            break;
        }
    }

}
