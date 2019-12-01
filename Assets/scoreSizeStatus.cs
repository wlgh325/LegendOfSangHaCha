﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class scoreSizeStatus : MonoBehaviour
{
    public GameObject scoreSizeStatusbtn;
    private bool isClickedScoreSizeStatus;
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
        isClickedScoreSizeStatus = CrossPlatformInputManager.GetButtonDown("ScoreSizeUp");

        if (isClickedScoreSizeStatus)
        {
            Debug.Log(FindObjectOfType<UserStatus>().GetBoxSizeLevel());
            scoreSizeUp();
            Debug.Log(FindObjectOfType<UserStatus>().GetBoxSizeLevel());
            flag = false;

        }
        if (isLevelUp())
        {
            flag = true;
            levelUp = false;
            Debug.Log("??");
        }
        scoreSizeStatusbtn.SetActive(flag);
    }
    public void scoreSizeUp()
    {
        UserStatus user = FindObjectOfType<UserStatus>();
        if (user.GetScoreSizeLevel() < 2)
        {
            user.ScoreSizeLevelUp();
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
