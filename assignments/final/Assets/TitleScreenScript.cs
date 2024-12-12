using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenScript : MonoBehaviour
{
    public Image fadeImage;
    public AudioSource src;
    public AudioClip startGameSound;
    
    // Start is called before the first frame update
    void Start()
    {
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            StartCoroutine(FadeToNextScene());
            src.clip = startGameSound;
            src.Play();
        }
    }

    IEnumerator FadeToNextScene(){
        float speed = .4f;
        while (fadeImage.color.a < 1){
            float alpha = fadeImage.color.a + speed * Time.deltaTime;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            yield return null;
        }
        
        SceneManager.LoadScene("SampleScene");
    }
}
