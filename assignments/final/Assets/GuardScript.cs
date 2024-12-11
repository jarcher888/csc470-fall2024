using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System;


public class GuardScript : MonoBehaviour
{
    // public static Action PlayerIsCaught;
    public GameObject patrolPoint;
    public NavMeshAgent nma;
    // public TMP_Text lostText;
    public PlayerScript player;

    LayerMask layerMask;

    Vector3 startPosition;

    void OnEnable(){
        GameManager.PlayerIsCaught += StopMoving;
        GameManager.Retry += ResetLevel;
    }
    void OnDisable(){
        GameManager.PlayerIsCaught -= StopMoving;
        GameManager.Retry -= ResetLevel;
    }
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        nma.speed = 5;
        layerMask = LayerMask.GetMask("player");
        
        StartCoroutine(GuardPatrol());
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.playerCaught && !GameManager.instance.playerInvisible){
            Color rayColor = Color.yellow;
            RaycastHit hitInfo;
            Vector3 rayStart = transform.position + Vector3.up * 1;

            if (Physics.Raycast(rayStart, transform.forward, out hitInfo, 5, layerMask)){
                if (hitInfo.collider.CompareTag("player")){
                    GameManager.instance.playerCaught = true;
                }
            }
            Debug.DrawRay(rayStart, transform.forward * 5, rayColor);
        }
    }

    IEnumerator GuardPatrol(){
        while (!GameManager.instance.playerCaught){
            nma.SetDestination(patrolPoint.transform.position);
            float distToPoint = Vector3.Distance(transform.position, patrolPoint.transform.position);
            while (distToPoint > 1f){
                yield return null;
                distToPoint = Vector3.Distance(transform.position, patrolPoint.transform.position);
            }

            yield return new WaitForSeconds(.5f);

            nma.SetDestination(startPosition);
            float distToStart = Vector3.Distance(transform.position, startPosition);
            while (distToStart > 1.1f){
                yield return null;
                distToStart = Vector3.Distance(transform.position, startPosition);
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    void StopMoving(){
        nma.isStopped = true;
    }

    void ResetLevel(){
        transform.position = startPosition;
        nma.isStopped = false;
    }
}
