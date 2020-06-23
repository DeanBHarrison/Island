using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetImage : MonoBehaviour
{

    public Sprite currentImage;

    private void Start()
    {
       // currentImage = PlayerPet.SelectedPetSO.artwork;
        gameObject.GetComponent<Image>().sprite = currentImage;
    }

}
