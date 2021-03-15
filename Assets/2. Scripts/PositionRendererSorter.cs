using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRendererSorter : MonoBehaviour
{

    [SerializeField]
    private int sortingOrderBase = 100;
    private Renderer myRenderer;
    [SerializeField]
    private float offset = 0f;
    [SerializeField]
    private bool runOnce = false;


    private void Awake()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
    }

    private void Start()
    {       
        InvokeRepeating("SortLayerDepth", 0.01f, 0.05f);
        sortingOrderBase = 100;
    }


    private void SortLayerDepth()
    {
        myRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y - offset);
        if (runOnce)
        {
            Destroy(this);
        }
    }
}
