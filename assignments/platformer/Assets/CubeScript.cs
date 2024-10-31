using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{

    float moveSpeed = 1;
    float freq = 3;
    float amp = 2;
    float offset;

    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

        offset = Random.Range(0, Mathf.PI * 2);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = startPosition + Vector3.forward * Mathf.Sin((offset + Time.time) * freq) * amp;
        transform.position = pos;
    }
}
