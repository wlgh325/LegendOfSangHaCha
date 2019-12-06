using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdate : MonoBehaviour{

    public GameObject levelupStatus;

    void Awake(){
        if(levelupStatus != null){
            levelupStatus.SetActive(false);
        }
    }

    public void showLevelUpStatus(){
        levelupStatus.SetActive(true);
    }

    public void unshowLevelUpStatus(){
        levelupStatus.SetActive(false);
    }
}
