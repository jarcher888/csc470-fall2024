using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlaneScript : MonoBehaviour
{

    int maxCollectible = 10;
    int score = 0;
    float timer = 90f;
    float forwardSpeed = 25f;
    float xRotationSpeed = 80f;
    float yRotationSpeed = 80f;
    float zRotation = 500f;

    float prevHAxis;

    bool hasWon = false;

    

    public GameObject cameraObject;

    public TMP_Text scoreText;

    public TMP_Text timerText;

    public Terrain terrain;

    public TMP_Text speedText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasWon){
            speedText.text = "Speed: " + MathF.Round(forwardSpeed, 2);

            if (forwardSpeed > 0){
                forwardSpeed -= .003f;
            }else{
                lose();
            }


            timer -= Time.deltaTime;
            timerText.text = Math.Round(timer, 0) + " s";

            if (timer < 0){
                if (score != maxCollectible){
                    lose();
                }
            }

            if (score == maxCollectible){
                scoreText.text = "Yippee! You win :)";
                timer = 0;
                hasWon = true;
            }

            float terrainHeight = terrain.SampleHeight(transform.position);
            if (transform.position.y < terrainHeight) {
                lose();
            }
        }


        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 amountToRotate = new Vector3(0,0,0);
        amountToRotate.x = -vAxis * xRotationSpeed;
        amountToRotate.y = hAxis * yRotationSpeed;

        //Desperately tried to improve flying mechanics but there was no success 
        //and my attempts just made things worse so I decided to leave it as is
        // if (hAxis != 0){
        //     amountToRotate.z = -hAxis * zRotation;
        //     prevHAxis = -hAxis;
        // }else if (hAxis == 0 && transform.rotation.z > -0.1 && transform.rotation.z < 0.1){
        //     amountToRotate.z = -prevHAxis * 4 * zRotation;

        // }
        // amountToRotate.z = -hAxis * zRotation;
        amountToRotate *= Time.deltaTime;
        transform.Rotate(amountToRotate, Space.Self);


        
        transform.position += transform.forward * forwardSpeed * Time.deltaTime;

        Vector3 cameraPosition = transform.position;
        cameraPosition += -transform.forward * 8f;
        cameraPosition += Vector3.up * 3f;
        cameraObject.transform.position = cameraPosition;
        cameraObject.transform.LookAt(transform.position);
    
    }

    public void OnTriggerEnter(Collider other){

        if (other.CompareTag("collectible")){

            forwardSpeed += 5f;

            score++;

            scoreText.text = "Score: " + score;

            Destroy(other.gameObject);
        }

    }

    public void lose(){
        scoreText.text = "You lose :(";
        Destroy(gameObject);
        timer = 0;
    }
}
