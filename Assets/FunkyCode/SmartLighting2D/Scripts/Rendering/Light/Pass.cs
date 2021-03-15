using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightSettings;

namespace Rendering.Light {

    public class Pass {

        public Light2D light;
        public LayerSetting layer;
        public int layerID;

        public float lightSizeSquared;

        public List<LightCollider2D> colliderList;
        public List<LightCollider2D> layerShadowList;
        public List<LightCollider2D> layerMaskList;

        #if UNITY_2017_4_OR_NEWER
            public List<LightTilemapCollider2D> tilemapList;
        #endif

        public bool drawMask = false;
        public bool drawShadows = false;

        public Material materialWhite;
        public Material materialNormalMap_PixelToLight;
        public Material materialNormalMap_ObjectToLight;

        public Sorting.SortPass sortPass = new Sorting.SortPass();

        public bool Setup(Light2D light, LayerSetting setLayer) {
            // Layer ID
            layerID = setLayer.GetLayerID();
            if (layerID < 0) {
                return(false);
            }

            layer = setLayer;

            // Calculation Setup
            this.light = light;
            lightSizeSquared = Mathf.Sqrt(light.size * light.size + light.size * light.size);
        
            colliderList = LightCollider2D.List;

            layerShadowList = LightCollider2D.GetShadowList(layerID);
            layerMaskList = LightCollider2D.GetMaskList(layerID);

            #if UNITY_2017_4_OR_NEWER
                tilemapList = LightTilemapCollider2D.GetList();
            #endif

            // Draw Mask & Shadows?
            drawMask = (layer.type != LightLayerType.ShadowOnly);
            drawShadows = (layer.type != LightLayerType.MaskOnly);

            // Materials
            materialWhite = Lighting2D.materials.GetMask();
    
            materialNormalMap_PixelToLight = Lighting2D.materials.GetNormalMapSpritePixelToLight();
            materialNormalMap_ObjectToLight = Lighting2D.materials.GetNormalMapSpriteObjectToLight();

            materialNormalMap_PixelToLight.SetFloat("_LightSize", light.size);
            materialNormalMap_PixelToLight.SetFloat("_LightIntensity", light.bumpMap.intensity);
            materialNormalMap_PixelToLight.SetFloat("_LightZ", light.bumpMap.depth);

            materialNormalMap_ObjectToLight.SetFloat("_LightSize", light.size);
            materialNormalMap_ObjectToLight.SetFloat("_LightIntensity", light.bumpMap.intensity);
            materialNormalMap_ObjectToLight.SetFloat("_LightZ", light.bumpMap.depth);

            sortPass.pass = this;
            
            // Sort
            sortPass.Clear();

            return(true);
        }
    }
}