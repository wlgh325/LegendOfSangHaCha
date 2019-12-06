using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class BoxSizeStatus : MonoBehaviour{

    public static BoxSizeStatus Instance{
        get{
            if(instance == null) instance = FindObjectOfType<BoxSizeStatus>();
            return instance;
        }
    }

    private static BoxSizeStatus instance;
    
	private bool isClickedBoxSizeStatus;
    private bool levelUp;
    // Start is called before the first frame update
    void Start(){
        levelUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        isClickedBoxSizeStatus = CrossPlatformInputManager.GetButtonDown("BoxSizeUp");
       
        if (isClickedBoxSizeStatus){
            BoxSizeUp();
        }
        if (isLevelUp()){
            levelUp = false;
        }
    }
	public void BoxSizeUp()
    {
        //UserStatus user = FindObjectOfType<UserStatus>();
		if(UserStatus.Instance.GetBoxSizeLevel() < 2){
			FindObjectOfType<UserStatus>().BoxSizeLevelUP();
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
