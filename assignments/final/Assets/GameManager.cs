using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Action PlayerIsCaught;
    public static Action Retry;
    public static Action ChangeLevels;

    public Camera cameraObject;

    public AudioSource src;
    public AudioClip buttonClickedSound, playerCaughtSound, winSound, replaySound;
    bool playerCaughtSoundPlayed = false;
    bool winSoundPlayed = false;

    public PlayerScript player;
    public int currentLevel;

    float invisibilityReloadTimer = 10f;
    float invisibilityActiveTimer = 3f;
    bool invisibiltyAvailable = true;

    public bool playerInvisible = false;
    public bool playerCaught = false;
    public bool secondLevelFinsh = false;

    public TMP_Text invisibilityText;

    public GameObject retryPanel;
    public GameObject winPanel;
    public Image fadeImage; 

    public bool firstLevelFinished = false;
    

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
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
        fadeImage.enabled = false;

        currentLevel = 1;
        invisibilityText.text = "Invisibility: Available";
        
    }

    // Update is called once per frame
    void Update()
    {
        if (firstLevelFinished){
            StartCoroutine(GoToSecondLevel());
        }

        if (secondLevelFinsh){
            winPanel.SetActive(true);
            invisibilityText.text = "";
            if (!winSoundPlayed){
                src.clip = winSound;
                src.Play();
                winSoundPlayed = true;
            }
            
        }else{
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

        
    }
    public void RetryButtonClicked(){
        Retry?.Invoke();
        playerCaught = false;
        src.clip = buttonClickedSound;
        src.Play();
        retryPanel.SetActive(false);
        invisibiltyAvailable = true;
        invisibilityText.text = "Invisibility: Available";
        playerCaughtSoundPlayed = false;
        
    }

    IEnumerator GoToSecondLevel(){
        fadeImage.enabled = true;
        float speed = 0.6f;
        while (fadeImage.color.a < 1){
            float alpha = fadeImage.color.a + speed * Time.deltaTime;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            yield return null;
        }
        
        SceneManager.LoadScene("SecondLevel");
    }
    IEnumerator ReplayGame(){
        fadeImage.enabled = true;
        float speed = 0.4f;
        while (fadeImage.color.a < 1){
            float alpha = fadeImage.color.a + speed * Time.deltaTime;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            yield return null;
        }
        
        SceneManager.LoadScene("SampleScene");
    }
    

    public void PlayAgain(){
        src.clip = replaySound;
        src.Play();
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
        StartCoroutine(ReplayGame());
    }

}
