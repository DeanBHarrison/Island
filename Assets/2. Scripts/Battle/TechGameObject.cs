using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechGameObject : MonoBehaviour
{
    public bool EnemyTech = false;

    public int TechGameObjectIndex;
    public int EnemyTechGameObjectIndex;

    

    public void OnHoverEnter()
    {
        Debug.Log("XXX" + TechGameObjectIndex);
        TechController.instance.descriptionBox.SetActive(true);
        TechController.instance.DescriptionBoxText.text = TechController.instance.HandTechs[TechGameObjectIndex].Description;
        TechController.instance.Energycost.text = TechController.instance.HandTechs[TechGameObjectIndex].EnergyCost.ToString();
        TechController.instance.TechImage.sprite = TechController.instance.HandTechs[TechGameObjectIndex].techSprite;
    }

    public void OnHoverExit()
    {
        TechController.instance.descriptionBox.SetActive(false);
    }
}
