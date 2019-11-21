using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User 
{
    public GameObject UserCh;

    private int level;
    private int boxSizeLevel;
    private int truckSizeLevel;
    private int scoreSizeLevel;
 

    void Start()
    {
        level = 1;
        boxSizeLevelLevel = 0;
        truckSizeLevel = 0;
        scoreSizeLevel = 0;
    }   

    public void Update()
    {
       
    }
    public int getBoxSizeLevel()
    {
        return boxSizeLevel;
    }
}