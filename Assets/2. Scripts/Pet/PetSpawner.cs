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
        spawnFirstPet();
    }

    public void spawnFirstPet()
    {
        //gets the first petIndex to spawn depending on how the questions were answered.
        petIndex = StateManager.PetSpawnIndex;
        SelectedPetSO = PetSOArray[petIndex];
        SpawnPet();
    }

    public void ChangePet()
    {

        //cycles through pets for debug loaded to a button
        petIndex++;
        if(petIndex == 4)
        {
            petIndex = 0;
        }
        SpawnPet();
        GameMenu.instance.setPetStatsOnMenu();
    }

    public void SpawnPet()
    {
        //spawns a pet based on it's "pet index" sets the artwork, stat potentials then sets its stats and hp to default.
        SelectedPetSO = PetSOArray[petIndex];
        gameObject.GetComponent<SpriteRenderer>().sprite = SelectedPetSO.artwork;
        PetStats.instance.setPetStatPotentials();
        PetStats.instance.FillRedAndBlue();
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

    public PetSelect pets;
}


