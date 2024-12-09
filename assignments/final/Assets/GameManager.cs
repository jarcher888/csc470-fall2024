using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Action PlayerIsCaught;
    public static Action Retry;

    public PlayerScript player;

    float invisibilityReloadTimer = 10f;
    float invisibilityActiveTimer = 3f;
    bool invisibiltyAvailable = true;

    public bool playerInvisible = false;
    public bool playerCaught = false;

    public TMP_Text invisibilityText;

    public GameObject retryPanel;
    

    void OnEnable(){
        if (GameManager.instance == null){
            GameManager.instance = this;
        }else{
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        invisibilityText.text = "Invisibility: Available";
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCaught){
            PlayerIsCaught?.Invoke();
            invisibilityText.text = "";
            retryPanel.SetActive(true);
        }

        if (!playerCaught){
            if (Input.GetKeyDown(KeyCode.Space) && invisibiltyAvailable){
                playerInvisible = true;
                invisibilityActiveTimer = 3f;
            }

            if (playerInvisible){
                invisibiltyAvailable = false;
                invisibilityActiveTimer -= Time.deltaTime;
                invisibilityText.text = "Invisibility: " + Math.Round(invisibilityActiveTimer, 0) + " s";
                if (invisibilityActiveTimer < 0){
                    playerInvisible = false;
                    invisibilityReloadTimer = 10f;
                }
            }
            if (!playerInvisible && !invisibiltyAvailable){
                invisibilityReloadTimer -= Time.deltaTime;
                invisibilityText.text = "Cooldown: " + Math.Round(invisibilityReloadTimer, 0) + " s";
                if (invisibilityReloadTimer < 0){
                    invisibiltyAvailable = true;
                    invisibilityText.text = "Invisibility: Available";
                }
            }
        }
    }
    public void RetryButtonClicked(){
        Debug.Log("clicked");
        retryPanel.SetActive(false);
        Retry?.Invoke();
    }
}
