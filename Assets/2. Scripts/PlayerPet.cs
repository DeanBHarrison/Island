using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPet : MonoBehaviour
{

    public StateManager StateManager;
    public int petIndex;
    public GameObject[] StarterPets;
    public GameObject spawnLocation;
    public bool shouldISpawnPet = false;

    // Start is called before the first frame update
    void Awake()
    {
        SetUpSingleton();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SpawnPet();
        petIndex = StateManager.PetSpawnIndex;
    }

    public void SpawnPet()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3 && shouldISpawnPet == false)
        {       
            Instantiate(StarterPets[petIndex], new Vector3(2.47f, -5.14f, 0f), transform.rotation);
            shouldISpawnPet = true;
            Destroy(gameObject);
        }
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
    }
}
