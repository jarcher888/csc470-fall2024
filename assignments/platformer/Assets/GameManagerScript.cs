using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    float spacing = 6f;
    public GameObject cubePrefab;

    GameObject[] obstacles;
    int numObstacles = 4;
    // Start is called before the first frame update
    void Start()
    {
        obstacles = new GameObject[numObstacles];

        Vector3 startingPosition = transform.position;
        for (int i = 0; i < numObstacles; i++){
            Vector3 pos = startingPosition + transform.forward;
            pos.x = i * spacing;
            pos.y += 4.5f;
            float amp = 1.5f;
            float freq = 2f;
            pos += transform.right * amp * Mathf.Sin(i * freq);
            GameObject cube = Instantiate(cubePrefab, pos, Quaternion.identity);
            obstacles[i] = cube;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
