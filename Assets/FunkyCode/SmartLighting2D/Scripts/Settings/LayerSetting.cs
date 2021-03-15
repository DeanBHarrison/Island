
using UnityEngine;
using LightSettings;

[System.Serializable]
public class LayerSetting {
	public int layerID = 0;
	public LightLayerType type = LightLayerType.ShadowAndMask;

	public LightLayerSorting sorting = LightLayerSorting.None;
	public LightLayerSortingIgnore sortingIgnore = LightLayerSortingIgnore.None;

	public LightLayerShadowEffect shadowEffect = LightLayerShadowEffect.Default;
	public int shadowEffectLayer = 0;

	public LightLayerMaskEffect maskEffect = LightLayerMaskEffect.AlwaysLit;
	public float maskEffectDistance = 1;

	public int GetLayerID() {
		int layer = (int)layerID;

		if (layer < 0) {
			return(-1);
		}

		return(layer);
	}
}

public class LayerSettingColor {
    public static Color Get(Vector2 position, LayerSetting layerSetting, MaskEffect maskEffect) {
		if (maskEffect == MaskEffect.Unlit) {
			return(Color.black);
		}
		
		if (layerSetting.maskEffect == LightLayerMaskEffect.AboveLit) {
            return(LayerSettingsColorEffects.GetColor(position, layerSetting));
        } else if (layerSetting.maskEffect == LightLayerMaskEffect.NeverLit) {
			return(Color.black);
		} else {
            return(Color.white);
        }
	}
}

public class LayerSettingsColorEffects {
	public static Color GetColor(Vector2 position, LayerSetting layerSetting) {
		float distance = layerSetting.maskEffectDistance;

		float pos, c;
		
		if (distance > 0) {
			pos = (position.y - distance / 2) / distance;

			c = (pos  + 1);
		} else {
			pos = position.y;

			if (pos < 0) {
				c = 0;
			} else {
				c = 1;
			}
		}
		


		if (c < 0) {
			c = 0;
		}

		if (c > 1) {
			c = 1;
		}

		return(new Color(c, c, c, 1));
	}
}