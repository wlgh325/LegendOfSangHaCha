using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobySceneManage : MonoBehaviour
{

    public Toggle backGroundToggle;
    public Toggle soundEffectToggle;
    
    public AudioSource back_sound;
    public AudioSource effect_sound;

    public void ClickBackBtn(){
        SceneManager.UnloadScene("LobbyScene");
        SceneManager.LoadScene("FirstScene");
    }

    public void ClickAchievementBtn(){

    }

    public void ClickQuestBtn(){
        
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
