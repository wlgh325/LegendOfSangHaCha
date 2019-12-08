using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbySceneManager : MonoBehaviour
{
    
    public GameObject alertWindow;
    public Button alertCloseButton;
    public Text alertText;
    public Toggle backGroundToggle;
    public Toggle soundEffectToggle;
    
    public AudioSource back_sound;
    public AudioSource effect_sound;

    void Awake() {
        Camera camera = FindObjectOfType<Camera>();
        back_sound = camera.GetComponent<AudioSource>();
    }

    public void ClickBackBtn(){
        SceneManager.LoadScene("FirstScene");
    }

    public void ClickAchievementBtn(){
        alertWindow.SetActive(true);
        alertText.text = "업적 기능은 개발중입니다.";
    }

    public void ClickPurchaseMoneyBtn() {
        alertWindow.SetActive(true);
        alertText.text = "게임 머니 구입 기능은 개발중입니다.";
    }
    public void ClickCloseBtn() {
        alertCloseButton.transform.parent.gameObject.SetActive(false);
    }
    public void ClickQuestBtn(){
        alertWindow.SetActive(true);
        alertText.text = "퀘스트 기능은 개발중입니다.";
    }
    
    public void ClickBackSoundToggle(){
        if(backGroundToggle.isOn == true)
            back_sound.mute = false;
        else
            back_sound.mute = true;
    }

    public void ClickEffectToggle(){
        if(soundEffectToggle.isOn == true)
            effect_sound.mute = false;
        else
            effect_sound.mute = true;
    }
}
