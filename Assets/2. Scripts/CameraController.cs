using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int MusicToPlay;

    // Start is called before the first frame update
    void Start()
    {
        MusicPlayer.instance.PlayMusic(MusicToPlay);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
