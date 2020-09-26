using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{

    public float lifetime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
        MusicPlayer.instance.PlaySFX(4);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
