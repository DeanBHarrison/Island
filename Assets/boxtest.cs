using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxtest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
    }
    private void on(Collision collision)
    {
        Debug.Log("collision");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
