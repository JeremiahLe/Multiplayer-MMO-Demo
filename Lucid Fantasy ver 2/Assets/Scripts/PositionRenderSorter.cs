using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRenderSorter : MonoBehaviour
{
    [SerializeField]
    private int sortingOrderBase = 5000;
    private Renderer myRenderer;
    [SerializeField]
    private float offset = 0;
    [SerializeField]
    private bool runOnlyOnce = false;

    private float timer;
    private float timerMax = .1f;
    
    // Start is called before the first frame update

    private void Awake()
    {
        myRenderer = gameObject.GetComponent<Renderer>();   
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = timerMax;
            myRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y - offset);
            if (runOnlyOnce)
            {
                Destroy(this);
            }
        }
    }
}
