using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bull : MonoBehaviour
{

    public Rigidbody2D RB;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
    }
}
