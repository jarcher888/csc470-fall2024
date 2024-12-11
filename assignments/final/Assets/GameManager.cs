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
    public static Action ChangeLevels;

    public Camera cameraObject;

    public AudioSource src;
    public AudioClip buttonClickedSound, playerCaughtSound;
    bool playerCaughtSoundPlayed = false;

    public PlayerScript player;
    public int currentLevel;

    float invisibilityReloadTimer = 10f;
    float invisibilityActiveTimer = 3f;
    bool invisibiltyAvailable = true;

    public bool playerInvisible = false;
    public bool playerCaught = false;

    public TMP_Text invisibilityText;

    public GameObject retryPanel;

    // public bool firstLevelFinished = false;
    

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
        // PlayerPrefs.SetString("finishedLevel", "false");
        currentLevel = 1;
        invisibilityText.text = "Invisibility: Available";

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(currentLevel);
        // if (PlayerPrefs.GetString("finishedLevel") == "true"){
        //     currentLevel = 2;
        // }

        if (playerCaught){
            if (!playerCaughtSoundPlayed){
                src.clip = playerCaughtSound;
                src.Play();
                playerCaughtSoundPlayed = true;
            }
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
        Retry?.Invoke();
        playerCaught = false;
        src.clip = buttonClickedSound;
        src.Play();
        retryPanel.SetActive(false);
        invisibiltyAvailable = true;
        invisibilityText.text = "Invisibility: Available";
        
    }

}
