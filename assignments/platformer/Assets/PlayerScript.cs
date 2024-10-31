using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{

    public CharacterController cc;
    public GameObject cameraObject;
    public TMP_Text timerText;
    public TMP_Text winText;
    public GameObject hammer;
    public GameObject hammer2;
    public GameObject hammer3;
    public GameObject hammer4;

    float moveSpeed = 10f;

    float gravity = -9.8f;
    float friction = -2.8f;
    float yVelocity = 0f;
    float zVelocity = 0f;
    float dashAmount = 8f;

    float jumpVelocity = 7f;

    bool camOneHit = false;
    bool camTwoHit = false;
    bool won = false;
    bool ended = false;
    float timer = 30f;
    
    // Start is called before the first frame update
    void Start()
    {
        timerText.text = timer + " s";
        winText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (!ended){

            timer -= Time.deltaTime;
            timerText.text = Math.Round(timer, 0) + " s";


            float hAxis = Input.GetAxis("Horizontal");
            float vAxis = Input.GetAxis("Vertical");

            if (yVelocity > 0 && Input.GetKeyDown(KeyCode.LeftShift))
            {
                zVelocity = dashAmount;
            }
            zVelocity += friction * Time.deltaTime;
            zVelocity = Mathf.Clamp(zVelocity, 0, 10000);


            if (!cc.isGrounded)
            {

                yVelocity += gravity * Time.deltaTime;
            }
            else
            {
                yVelocity = -2;
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    yVelocity = jumpVelocity;
                }
                zVelocity = 0;
            }

            Vector3 cameraFlat = cameraObject.transform.forward;
            cameraFlat.y = 0;
            cameraFlat.Normalize();

            Vector3 leftRightMove = cameraObject.transform.right * hAxis * moveSpeed;

            Vector3 amountToMove = cameraFlat * vAxis * moveSpeed + leftRightMove;
            amountToMove.y += yVelocity;
            amountToMove += transform.forward * zVelocity;
            amountToMove *= Time.deltaTime;


            cc.Move(amountToMove);

            amountToMove.y = 0;
            transform.forward = amountToMove.normalized;

            if (camTwoHit)
            {
                
                float rotationAmount = MathF.Sin(3 + Time.time * 1.5f) * 3f;
                Vector3 rotationVector = new Vector3(rotationAmount, 0, 0);

                hammer.transform.Rotate(rotationVector);

                float rotationAmount2 = MathF.Sin(2.4f + Time.time * 1.5f) * 5f;
                Vector3 rotationVector2 = new Vector3(rotationAmount2, 0, 0);

                hammer2.transform.Rotate(rotationVector2);

                float rotationAmount3 = MathF.Sin(10 + Time.time * 1.5f) * 3f;
                Vector3 rotationVector3 = new Vector3(rotationAmount3, 0, 0);

                hammer3.transform.Rotate(rotationVector3);

                float rotationAmount4 = MathF.Sin(7 + Time.time * 1.5f) * 5f;
                Vector3 rotationVector4 = new Vector3(rotationAmount4, 0, 0);

                hammer4.transform.Rotate(rotationVector4);
            }
            if (camTwoHit && cc.isGrounded && transform.position.y < 1){
                transform.position = new Vector3(34, 8, 2);
            }
            if (timer < 0){
                ended = true;
            }
        }else if(won){
            winText.text = "Congratulations! You beat my very good and difficult game!";
        }else if (ended && timer < 0 ){
            winText.text = "You lose";
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("cam1") && !camOneHit)
        {
            cameraObject.transform.position = new Vector3(-25, 20, 2);
            cameraObject.transform.LookAt(new Vector3(0, 0, 3));
            camOneHit = true;
        }
        else if (other.CompareTag("cam2") && !camTwoHit)
        {
            camTwoHit = true;
            cameraObject.transform.position = new Vector3(42, 14, 2);
            cameraObject.transform.LookAt(new Vector3(0, 0, 2));
        }else if (other.CompareTag("gamewin") && camTwoHit){
            won = true;
            ended = true;
        }
    }

    public void restart(){
        ended = false;
        timer = 30f;
        won = false;
        camOneHit = false;
        camTwoHit = false;
        transform.position = new Vector3(7f, 0, -6f);
        cameraObject.transform.position = new Vector3(-.63f,9.4f,-31.1f);
        cameraObject.transform.LookAt(new Vector3(2f, 0, 6f));
        winText.text = "";
    }
}
