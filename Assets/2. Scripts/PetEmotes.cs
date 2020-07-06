using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class PetEmotes : MonoBehaviour
{
    public Sprite[] PossibleEmotes;
    public Sprite[] CurrentEmotesToCycle;

    public static int emotesInArrayCounter;
    public static PetEmotes instance;

    private void Awake()
    {
        SetUpSingleton();
    }

    void Start()
    {
        InvokeRepeating("shouldICycleEmotes", .01f, 1);
    }


    public float waitTime = 3f;
    public bool resetCycle = true;

    public void addEmoteToCycle(int EmoteIndex)
    {
        CurrentEmotesToCycle[EmoteIndex] = PossibleEmotes[EmoteIndex];
        emotesInArrayCounter++;
    }

    public void removeEmoteFromCycle(int EmoteIndex)
    {
        CurrentEmotesToCycle[EmoteIndex] = null;
        emotesInArrayCounter--;
    }

    IEnumerator CycleCurrentEmotes()
    {
        resetCycle = false;
        for (int i = 0; i < CurrentEmotesToCycle.Length; i++)
        {

            if (CurrentEmotesToCycle[i] != null)
            {
             
                gameObject.GetComponent<SpriteRenderer>().sprite = CurrentEmotesToCycle[i];
                yield return new WaitForSeconds(waitTime);
            }
            if (i == CurrentEmotesToCycle.Length -1)
            {
               // yield return new WaitForSeconds(waitTime);
                resetCycle = true;
            }
        }
    }

    public void shouldICycleEmotes()
    {
        if (resetCycle && emotesInArrayCounter > 0)
        {
            StartCoroutine(CycleCurrentEmotes());
        }
        else if(emotesInArrayCounter == 0 && gameObject.GetComponent<SpriteRenderer>().sprite != null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
        }

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
            }
        }
    }
}
