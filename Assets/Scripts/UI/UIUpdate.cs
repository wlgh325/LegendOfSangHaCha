using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdate : MonoBehaviour{

    public GameObject levelupStatus;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void showLevelUpStatus(){
        levelupStatus.SetActive(true);
    }

    public void unshowLevelUpStatus(){
        levelupStatus.SetActive(false);
    }
}
