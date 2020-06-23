using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PetSpawner : MonoBehaviour
{

    public static PetSpawner instance;

    // all this script does is spawn the pet.
    public  Pets[] PetSOArray;
    public  Pets SelectedPetSO;
    public int petIndex = 0;

    // Start is called before the first frame update
    void Awake()
    {
        SetUpSingleton();       
    }
    private void Start()
    {
        SelectedPetSO = PetSOArray[petIndex];
    }

    public void ChangePet()
    {

        petIndex++;
        if(petIndex == 4)
        {
            petIndex = 0;
        }

        SelectedPetSO = PetSOArray[petIndex];
        gameObject.GetComponent<SpriteRenderer>().sprite = SelectedPetSO.artwork;        
        PetStats.instance.setPetStatPotentials();       
        PetStats.instance.SetPetStatsToDefault();
    }



    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        instance = this;
    }
}
