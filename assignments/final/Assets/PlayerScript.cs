using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public CharacterController cc;
    public GameObject cameraObject;

    Vector3 startPosition = new Vector3(0,0,0);

    float moveSpeed = 10f;
    public bool invisible = false;
    public bool firstLevelFinished = false;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    void OnEnable(){
        GameManager.ChangeLevels += MoveToSecondLevel;
        GameManager.Retry += ResetLevel; 
    }

    void OnDisable(){
        GameManager.ChangeLevels -= MoveToSecondLevel;
        GameManager.Retry -= ResetLevel;
    }

    // Update is called once per frame
    void Update()
    {

        if (!GameManager.instance.playerCaught){
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

    void MoveToSecondLevel(){
        transform.position = new Vector3(1.1f, 0.1f, 72.1f);
    }
    public void OnTriggerEnter(Collider other){
        if (other.CompareTag("firstLevelFinish")){
            firstLevelFinished = true;
            GameManager.instance.currentLevel++;
            GameManager.instance.src.clip = GameManager.instance.buttonClickedSound;
            GameManager.instance.src.Play();
            GameManager.instance.firstLevelFinished = true;
        }else if(other.CompareTag("secondLevelFinish")){
            GameManager.instance.secondLevelFinsh = true;
        }
    }

    void ResetLevel(){
        Debug.Log(startPosition);
        cc.Move(startPosition);
    }
}
