using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyTechController : MonoBehaviour
{


    public static EnemyTechController instance;


    public List<string> Combo1;
    public List<string> Combo2;
    public List<string> Combo3;
    public List<string> Combo4;
    public List<string> Combo5;
    public List<string> Combo6;
    public List<string> Combo7;
    public List<string> Combo8;
    public List<string> Combo9;
    public List<string> Combo10;
    public List<string> CombotoSet;

    public Techs[] TechsReference;
    public GameObject[] HandGO;
    public Techs[] HandTechs;

    public Techs TechLastUsed;

    public void SetCombo(int comboindex)
    {
            switch (comboindex)
            {
                case 1:
                CombotoSet = Combo1;
                    break;

                case 2:
                CombotoSet = Combo2;
                    break;

                case 3:
                CombotoSet = Combo3;
                    break;

                case 4:
                CombotoSet = Combo4;
                    break;

                case 5:
                CombotoSet = Combo5;
                    break;

                case 6:
                CombotoSet = Combo6;
                    break;

            case 7:
                CombotoSet = Combo7;
                break;

            case 8:
                CombotoSet = Combo8;
                break;

            case 9:
                CombotoSet = Combo9;
                break;

            case 10:
                CombotoSet = Combo10;
                break;

            default:
                Debug.Log("combo set as default");
                CombotoSet = Combo1;
                break;
        }


        for (int i = 0; i < CombotoSet.Count; i++)
        {
            HandGO[i].SetActive(true);
            Debug.Log("setting combo icons etc");
            //sets the handtechs from the combo given
            HandTechs[i] = GetTechDetails(CombotoSet[i]);
            HandGO[i].GetComponent<Image>().sprite = HandTechs[i].techSprite;

        }

    }

    public void UseTech()
    {

            Debug.Log("ENEMY tech used");
            TechLastUsed = HandTechs[0];
            HandGO[0].GetComponent<Image>().sprite = null;
            battleSystem.instance.EnemyTechPlayed(HandTechs[0]);
            HandTechs[0] = null;
            SortHandTechs();


    }

    private void Awake()
    {
        SetUpSingleton();
    }

    private void Start()
    {
       
    }
    private void SetUpSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
                Debug.Log("something messed up with tech controller");
            }
        }
    }

    public void SortHandTechs()
    {
        // Variable to keep track of what's the next empty slot
        int emptySlot = 0;

        // Iterate through each handtech slot that we have
        for (int i = 0; i < 3; i++)
        {
            // If we see an available item in the inventory, move it to an empty slot
            if (HandTechs[i] != null)
            {
                HandTechs[emptySlot] = HandTechs[i];
                HandGO[emptySlot].GetComponent<Image>().sprite = HandGO[i].GetComponent<Image>().sprite;
                emptySlot++;
            }
        }

        // Populate inventory with "empty" items
        // We start at the next empty slot, and just iterate through the remaining slots left
        for (int i = emptySlot; i < 3; i++)
        {
            // Reset the values
            HandTechs[i] = null;
            HandGO[i].SetActive(false);
            HandGO[i].GetComponent<Image>().sprite = null;

        }
    }

    public Techs GetTechDetails(string TechToGet)
    {
        for (int i = 0; i < TechsReference.Length; i++)
        {
            if (TechsReference[i].name == TechToGet)
            {
                return TechsReference[i];
            }
        }
        return null;
    }
}
