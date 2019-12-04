using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class TruckSizeStatus : MonoBehaviour
{
    public GameObject truckSizeStatusbtn;
    private bool isClickedTruckSizeStatus;
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
        isClickedTruckSizeStatus = CrossPlatformInputManager.GetButtonDown("TruckSizeUp");

        if (isClickedTruckSizeStatus)
        {
            //Debug.Log(FindObjectOfType<UserStatus>().GetBoxSizeLevel());
            truckSizeUp();
            //Debug.Log(FindObjectOfType<UserStatus>().GetBoxSizeLevel());
            deleteBtn();
            FindObjectOfType<scoreSizeStatus>().deleteBtn();
            FindObjectOfType<BoxSizeStatus>().deleteBtn();

        }
        if (isLevelUp())
        {
            flag = true;
            levelUp = false;
            Debug.Log("??");
        }
        truckSizeStatusbtn.SetActive(flag);

    }
    public void truckSizeUp() {
        if (UserStatus.boxSizeLevel < 2) {
            FindObjectOfType<UserStatus>().TruckSizeLevelUp();
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
    public void deleteBtn()
    {
        flag = false;
    }

}
