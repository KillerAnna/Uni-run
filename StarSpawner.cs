using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject starPrefab;
    public float yMin = 1.25f;
    public float yMax = 4f;
    public float x = 20.0f;

    float span;
    float delta;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameover)
            return;

        span = Random.Range(1.8f, 4f);
        delta += Time.deltaTime;
        if(delta >= span)
        {
            delta = 0f;
            Vector2 yVec = new Vector2(x, Random.Range(yMin, yMax));

            Instantiate(starPrefab, yVec, Quaternion.identity);
        }
    }
}
