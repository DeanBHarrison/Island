using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    public AudioSource[] Music;
    public AudioSource[] SFX;
    

    public static MusicPlayer instance;

    // Start is called before the first frame update
    void Awake()
    {
        SetUpSingleton();
    }

    private void Start()
    {
        
    }

    private void Update()
    {

    }

    public void PlaySFX(int soundToPlay)
    {
            SFX[soundToPlay].Play();
    }

    public void PlayMusic(int soundToPlay)
    {
        if (!Music[soundToPlay].isPlaying)
        {
            StopMusic();
            Music[soundToPlay].Play();
        }
    }

    public void StopMusic()
    {
        for(int i=0; i<Music.Length; i++)
        {
            Music[i].Stop();
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

        DontDestroyOnLoad(gameObject);
    }
}
