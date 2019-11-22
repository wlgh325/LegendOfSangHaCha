using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class BoxSizeStatus : MonoBehaviour
{
    public GameObject BoxSizeStatusbtn;
	private bool isClickedBoxSizeStatus;
    private bool flag;
    private bool levelUp;
    // Start is called before the first frame update
    void Start()
    {
        flag = false;
        levelUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        isClickedBoxSizeStatus = CrossPlatformInputManager.GetButtonDown("BoxSizeUp");
       
        if (isClickedBoxSizeStatus)
        {
            Debug.Log(FindObjectOfType<UserStatus>().GetBoxSizeLevel());
            BoxSizeUp();
            Debug.Log(FindObjectOfType<UserStatus>().GetBoxSizeLevel());
            flag = false;
           
        }
        if (isLevelUp())
        {
            flag = true;
            levelUp = false;
            Debug.Log("??");
        }
         BoxSizeStatusbtn.SetActive(flag);
        
    }
	public void BoxSizeUp()
    {
        UserStatus user = FindObjectOfType<UserStatus>();
		if(user.GetBoxSizeLevel() < 2)
        {
			user.BoxSizeLevelUP();
        }
    }
	public bool isLevelUp()
    {
        return levelUp;
    }
	public void triggerLeverUp()
    {
        levelUp = true;
    }
	
}
