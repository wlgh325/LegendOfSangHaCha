﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour{
    public float limitTime;
    private bool isGameover;
    public Text timeText;
    public static int gridWidth = 16;
    public static int gridHeight = 14;
    public static int gridDepth = 32;
    
    public static Transform[,,] grid = new Transform[gridWidth,gridHeight, gridDepth];
    public static List<BoxControl> boxList = new List<BoxControl>();

    void Start(){
        isGameover = false;
        limitTime = 180.0f;
    }

    // Update is called once per frame
    private void Update(){
        if (!isGameover)
        {
            int minute = (int)limitTime / 60;
            int second = (int)(limitTime - minute * 60);

            if (second >= 10)
            {
                if (limitTime < 11)
                {
                    // 버닝
                    Time.timeScale = 8.0f; // 게임 속도
                    limitTime -= 0.125f * Time.deltaTime;
                    timeText.color = Color.red;
                    timeText.fontSize = 30;
                }
                else {
                    limitTime -= Time.deltaTime;
                }
                timeText.text = "0" + minute + ":" + second;
            }
            else
            {
                 if (limitTime <= 10)
                {
                    // 버닝
                    Time.timeScale = 8.0f;
                    limitTime -= 0.125f * Time.deltaTime;
                    timeText.color = Color.red;
                    timeText.fontSize = 30;
                }
                else {
                    limitTime -= Time.deltaTime;
                }
                timeText.text = "0" + minute + ":0" + second;
            }
            
            if (limitTime < 0)
            {
                isGameover = true;
            }
        }
        else {  
            // 게임 종료하고 점수 집계
            GameOver();
        }
        /*
        if(Input.GetKey(KeyCode.E)){
            expBar.value += exp;
        }
        */
    }
    
    // update grid state
    public void UpdateGrid(BoxControl boxes){
        
        for(int k = 0; k < gridDepth; ++k){
            for(int j = 0; j < gridHeight; ++j){
                for(int i = 0; i<gridWidth; ++i){
                    if(grid[i, j, k] != null){
                        if(grid[i, j, k].parent == boxes.transform){
                            grid[i, j, k] = null;
                        }
                    }
                }
            }

        }
        
        foreach(Transform box in boxes.transform){
            Vector3 pos = Round (box.position);
            grid[(int)pos.x, (int)pos.y, (int)pos.z] = box;
        }
    }
    
    public Transform GetTransformAtGridPosition(Vector3 pos){
        if(pos.z > gridDepth - 1){
            return null;
        }
        else{
            return grid[(int)pos.x, (int)pos.y, (int)pos.z];
        }
    }

    public bool CheckIsInsideGrid(Vector3 pos){
        //return true;
        return ((int)pos.x >= 0 && pos.x < gridWidth && (int)pos.z < gridDepth && pos.z >= 0
        && (int)pos.y >=0 && (int)pos.y < gridHeight);
    }

    // 반올림 하는 function
    public Vector3 Round(Vector3 pos){
        return new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
    }

    public bool CheckIsAboveGrid (BoxControl boxes){
        for(int x = 0; x<gridWidth; ++x){
            foreach(Transform box in boxes.transform){
                Vector3 pos = Round(box.position);
                if(pos.z < 0){
                    return true;
                }

            }
        }
        return false;
    }

    public void GameOver(){
        Application.LoadLevel("GameOver");
    }
}
