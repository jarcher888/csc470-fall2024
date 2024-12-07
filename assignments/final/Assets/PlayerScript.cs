using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public CharacterController cc;
    public GameObject cameraObject;

    float moveSpeed = 10f;
    public bool invisible = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)){
            invisible = true;
        }
        

        float vAxis = Input.GetAxis("Vertical");
        float hAxis = Input.GetAxis("Horizontal");

        Vector3 cameraFlat = cameraObject.transform.forward;
        cameraFlat.y = 0;
        cameraFlat.Normalize();

        Vector3 leftRightMove = hAxis * moveSpeed * cameraObject.transform.right;

        Vector3 amountToMove = cameraFlat * moveSpeed * vAxis + leftRightMove;
        amountToMove *= Time.deltaTime;

        cc.Move(amountToMove);
        if (amountToMove.magnitude > 0){
            transform.forward = amountToMove.normalized;
        }
    }
}
