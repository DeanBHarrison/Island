using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;

    public string lastArea;


    public static PlayerController instance;

    public bool canMove = true;
    public bool isWaiting = false;

    private Vector3 bottomleftLimit;
    private Vector3 topRightLimit;


    private void Awake()
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

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (!Clock.instance.shouldTimePass)
        {

            canMove = false;
        }
        else
        {
            canMove = true;
        }


        if (canMove && !isWaiting)
        {
            theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        }
        else
        {
            theRB.velocity = Vector2.zero;
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomleftLimit.x, topRightLimit.x),
                                         Mathf.Clamp(transform.position.y, bottomleftLimit.y, topRightLimit.y), transform.position.z);
    }

    public void Setbounds(Vector3 botLeft, Vector3 topRight)
    {
        bottomleftLimit = botLeft + new Vector3(1f, 2f, 0f);
        topRightLimit = topRight - new Vector3(1f, 1f, 0f);
    }
}

