using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public int MusicToPlay;
    public Transform target;
    public bool disablePlayerFollow = false;

    public Tilemap theMap;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    private float halfHeight;
    private float halfWidth;

    // Start is called before the first frame update
    void Start()
    {
        setUpCameraLimits();
        MusicPlayer.instance.PlayMusic(MusicToPlay);
        
    }
    //sets the variables so that camera can follow player and stay in bounds below
  

    // Update is called once per frame
    void LateUpdate()
    {
        CameraFollowPlayer();
        SetCameraBounds();
    }

    public void setUpCameraLimits()
    {
        if (!disablePlayerFollow)
        {
            target = PlayerController.instance.transform;
            halfHeight = Camera.main.orthographicSize;
            halfWidth = halfHeight * Camera.main.aspect;
            bottomLeftLimit = theMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
            topRightLimit = theMap.localBounds.max - new Vector3(halfWidth, halfHeight, 0f);
            PlayerController.instance.Setbounds(theMap.localBounds.min, theMap.localBounds.max);
        }
    }

    //sets the camera to follow the player
    public void CameraFollowPlayer()
    {
        if (!disablePlayerFollow)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }

    //stops the camera being able to go outside of the level bounds
    public void SetCameraBounds()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
                                         Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);

    }
}
