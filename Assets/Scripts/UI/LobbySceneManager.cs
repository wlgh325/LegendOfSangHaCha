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
