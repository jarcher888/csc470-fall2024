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
    public TMP_Text lostText;
    public PlayerScript player;
    bool caught = false;

    LayerMask layerMask;

    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("player");
        lostText.text = "";

        // Debug.Log(patrolPoint.transform.position);
        startPosition = transform.position;
        StartCoroutine(GuardPatrol());
    }

    // Update is called once per frame
    void Update()
    {
        if (!caught && !player.invisible){
            Color rayColor = Color.yellow;
            RaycastHit hitInfo;
            Vector3 rayStart = transform.position + Vector3.up * 1;

            if (Physics.Raycast(rayStart, transform.forward, out hitInfo, 4, layerMask)){
                if (hitInfo.collider.CompareTag("player")){
                    Debug.Log("hit");
                    lostText.text = "You've been caught!";
                    caught = true;
                }
            }
            Debug.DrawRay(rayStart, transform.forward * 4, rayColor);
        }
    }

    IEnumerator GuardPatrol(){
        while (!caught){
            nma.SetDestination(patrolPoint.transform.position);
            float distToPoint = Vector3.Distance(transform.position, patrolPoint.transform.position);
            while (distToPoint > 1f){
                yield return null;
                distToPoint = Vector3.Distance(transform.position, patrolPoint.transform.position);
            }

            yield return new WaitForSeconds(1);

            nma.SetDestination(startPosition);
            float distToStart = Vector3.Distance(transform.position, startPosition);
            while (distToStart > 1.1f){
                // Debug.Log(distToStart);
                yield return null;
                distToStart = Vector3.Distance(transform.position, startPosition);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
