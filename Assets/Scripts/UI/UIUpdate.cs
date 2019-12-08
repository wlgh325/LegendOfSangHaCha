using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdate : MonoBehaviour{

    public GameObject levelupStatus;

    private readonly int boxSizeUp = 0;

    void Awake(){
        if(levelupStatus != null){
            levelupStatus.SetActive(false);
        }
    }

    public void showLevelUpStatus(){
        levelupStatus.SetActive(true);
        levelupStatus.transform.GetChild(boxSizeUp).gameObject.SetActive(true);
    }

    public void showTwoStatus()
    {
        levelupStatus.SetActive(true);
        levelupStatus.transform.GetChild(boxSizeUp).gameObject.SetActive(false);
    }
    public void unshowLevelUpStatus(){
        levelupStatus.SetActive(false);
    }
}
