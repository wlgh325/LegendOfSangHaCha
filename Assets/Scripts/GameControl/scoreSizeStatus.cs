using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class scoreSizeStatus : MonoBehaviour {
    public static scoreSizeStatus Instance = null;

    void Awake(){
        if(Instance == null){
            Instance = this;
        }
        else if(Instance != this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private bool isClickedScoreSizeStatus;
    private bool levelUp;
    // Start is called before the first frame update
    void Start()
    {
        levelUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        isClickedScoreSizeStatus = CrossPlatformInputManager.GetButtonDown("ScoreSizeUp");

        if (isClickedScoreSizeStatus){
            scoreSizeUp();
        }
        if (isLevelUp()){
            levelUp = false;
        }
        
    }
    public void scoreSizeUp(){

        if (UserStatus.Instance.GetScoreSizeLevel() < 2){
            FindObjectOfType<UserStatus>().ScoreSizeLevelUp();
            GetComponent<UIUpdate>().unshowLevelUpStatus();
        }
    }
    public bool isLevelUp()
    {
        return levelUp;
    }
    public void triggerLevelUp()
    {
        levelUp = true;
    }
}
