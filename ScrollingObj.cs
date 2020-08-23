using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObj : MonoBehaviour
{
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isGameover)
            transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
