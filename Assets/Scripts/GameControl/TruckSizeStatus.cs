using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class TruckSizeStatus : MonoBehaviour{

    public static TruckSizeStatus Instance = null;

    void Awake(){
        if(Instance == null){
            Instance = this;
        }
        else if(Instance != this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    
    private bool isClickedTruckSizeStatus;
    private bool levelUp;
    // Start is called before the first frame update
    void Start()
    {
        levelUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        isClickedTruckSizeStatus = CrossPlatformInputManager.GetButtonDown("TruckSizeUp");

        if (isClickedTruckSizeStatus){
            truckSizeUp();
        }
        if (isLevelUp()){
            levelUp = false;
        }
    }
    public void truckSizeUp() {
        if (UserStatus.Instance.GetTruckSizeLevel() < 2) {
            FindObjectOfType<UserStatus>().TruckSizeLevelUp();
            GetComponent<UIUpdate>().unshowLevelUpStatus();
            GameManager.Instance.SpawnTruck();
            GameControl.Instance.updateGridLevel();
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
