using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FirstSceneMenu : MonoBehaviour
{
    public Image fadeImage;
    float totalFadeTime = 1;

    public Color destColor;
    Color oriColor;

    // Start is called before the first frame update
    void Start(){
        oriColor = fadeImage.color;    
    }

    public void Fade(){
        StartCoroutine(Fade_());
    }

    IEnumerator Fade_(){
        float curTime = 0;

        fadeImage.gameObject.SetActive(true);
        while(curTime < totalFadeTime){
            curTime += Time.deltaTime;
            fadeImage.color = Color.Lerp(oriColor, destColor, curTime);
            yield return null;
        }
        ClickStartBtn();
    }

    private void ClickStartBtn(){
        SceneManager.LoadScene("LobyScene");
    }

    public void ClickCloseBtn(){
        Debug.Log("quit");
        Application.Quit();
    }
}
