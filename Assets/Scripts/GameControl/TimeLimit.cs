using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeLimit : MonoBehaviour {

    public Text timeText;
    public float limitTime;
    private bool isGameover;

    void Start() {
        Debug.Log("time start");
        isGameover = false;
        limitTime = 180.0f;
    }

    private void Update(){
        if(BoxControl.start){
            if (!isGameover) {
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
                    FindObjectOfType<GameControl>().GameOver();
          
                }
                /*
                if(Input.GetKey(KeyCode.E)){
                    expBar.value += exp;
                }
                */

        }

    }


}
