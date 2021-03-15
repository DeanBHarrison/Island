using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birdy : MonoBehaviour
{

    public Animator animator;
    public bool flyaway = false;
    public float flyspeed;
    public Rigidbody2D RB;



    void Update()
    {
        animator.SetBool("fly", flyaway);

        if (flyaway)
        {
            RB.velocity = new Vector2(flyspeed, flyspeed);
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("walked near bird");
            flyaway = true;
            Destroy(gameObject, 10f);
        }
    }
}
