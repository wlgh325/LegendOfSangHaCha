﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class BoxControl : MonoBehaviour{

    public bool allowXRotation = true, limitXRotation = false;

    public bool allowYRotation = true, limitYRotation = false;
    private float speed = 1;

    private float directionX, directionY;

    private bool isClickedLeftBtn, isClickedRightBtn, isClickedDownBtn, isClickedUpBtn, isClickedDepthBtn;
    private bool isClickedRotateX, isClickedRotateY;
    private bool isClickedSendBtn; // 보내기 버튼
    private bool stopFlag = true;

    private float angle = 90;

    public float xSpeed = 5.0f;
    public float moveTime = 0.05f;

    float fall = 0;
    public float fallSpeed = 1f;
    public int size;
    public static int boxNum;

    void Start(){
       //StartCoroutine("RunFadeOut");
       //stack BoxNum 
               
                //boxNum += size;
    }

    // Update is called once per frame
    void Update(){
        isClickedRotateX = CrossPlatformInputManager.GetButtonDown("rotateX");
        isClickedRotateY = CrossPlatformInputManager.GetButtonDown("rotateY");

        isClickedLeftBtn = CrossPlatformInputManager.GetButtonDown("GoLeft");
        isClickedRightBtn = CrossPlatformInputManager.GetButtonDown("GoRight");
        isClickedDownBtn = CrossPlatformInputManager.GetButtonDown("GoDown");
        isClickedUpBtn = CrossPlatformInputManager.GetButtonDown("GoUp");
        isClickedDepthBtn = CrossPlatformInputManager.GetButtonDown("GoDepth");

        isClickedSendBtn = CrossPlatformInputManager.GetButtonDown("Send");
        
        // check Button clicked
        if (isClickedRotateY){
            if(allowYRotation){
                if(limitYRotation){
                    if(transform.rotation.eulerAngles.y >= 90){
                        transform.Rotate(new Vector3(0, -angle, 0), Space.Self);
                    }
                    else{
                        transform.Rotate(new Vector3(0, angle, 0), Space.Self);
                    }
                }
                else{
                    transform.Rotate(new Vector3(0, angle, 0), Space.Self);
                }

                if(!CheckIsValidPosition()){
                    if(limitYRotation){
                        if(transform.rotation.eulerAngles.y >= 90){
                            transform.Rotate(new Vector3(0, -angle, 0), Space.Self);
                        }
                        else{
                            transform.Rotate(new Vector3(0, angle, 0), Space.Self);
                        }
                    }
                    else{    
                        transform.Rotate(new Vector3(0, -angle, 0), Space.Self);
                    }
                    
                }
            }
        }
        else if (isClickedRotateX){
            if(allowXRotation){
                if(limitXRotation){
                    if(transform.rotation.eulerAngles.x >= 90){
                        transform.Rotate(new Vector3(-angle, 0, 0), Space.Self);
                    }
                    else{
                        transform.Rotate(new Vector3(angle, 0, 0), Space.Self);
                    }
                }
                else{
                    transform.Rotate(new Vector3(angle, 0, 0), Space.Self);
                }

                if(!CheckIsValidPosition()){
                    if(limitXRotation){
                        if(transform.rotation.eulerAngles.x >= 90){
                            transform.Rotate(new Vector3(-angle, 0, 0), Space.Self);
                        }
                        else{
                            transform.Rotate(new Vector3(angle, 0, 0), Space.Self);
                        }
                    }
                    else{    
                        transform.Rotate(new Vector3(-angle, 0, 0), Space.Self);
                    }
                    
                }
            }
        }
        else if (isClickedLeftBtn){
            transform.position += new Vector3(-1, 0, 0);
            
            if(!CheckIsValidPosition()){
                transform.position += new Vector3(1, 0, 0);
            }
            else{
                FindObjectOfType<GameControl>().UpdateGrid(this);
            }
        }
        else if (isClickedRightBtn){
            transform.position += new Vector3(1, 0, 0);
            
            if(!CheckIsValidPosition()){
                transform.position += new Vector3(-1, 0, 0);
            }
            else{
                FindObjectOfType<GameControl>().UpdateGrid(this);
            }
        }
        else if (isClickedDownBtn){
            transform.position += new Vector3(0, -1, 0);
            
            if(!CheckIsValidPosition()){
                transform.position += new Vector3(0, 1, 0);
            }
            else{
                FindObjectOfType<GameControl>().UpdateGrid(this);
            }
        }
        else if (isClickedUpBtn){
            transform.position += new Vector3(0, 1, 0);
            
            if(!CheckIsValidPosition()){
                transform.position += new Vector3(0, -1, 0);
            }
            else{
                FindObjectOfType<GameControl>().UpdateGrid(this);
            }
        }
        else if (isClickedDepthBtn){
            transform.position += new Vector3(0, 0, 1);
            
            if(!CheckIsValidPosition()){
                transform.position += new Vector3(0, 0, -1);
            }
            else{
                FindObjectOfType<GameControl>().UpdateGrid(this);
            }
        }
        else if (isClickedSendBtn){
            // Name "Send"인 버튼 배치 need
            SendTruck();
            UpdateNewTruck(); // 트럭을 새로 갱신하고 모든 변수들 초기화
        }
        // fall box
        if(Time.time - fall >= fallSpeed){
            transform.position += new Vector3(0, 0, 1);

            if(!CheckIsValidPosition()){
                transform.position += new Vector3(0, 0, -1);
                enabled = false;
                if(FindObjectOfType<GameControl>().CheckIsAboveGrid(this)){
                    FindObjectOfType<GameControl>().GameOver();
                }
                boxNum +=size;
                GameControl.boxList.Add(this);
                FindObjectOfType<MakeRandomBox>().makeRandomBox();
            }
            else{
                FindObjectOfType<GameControl>().UpdateGrid(this);
            }
            fall = Time.time;
        }
        
    }

    private void FixedUpdate(){
        
    }

    private void SendTruck() {
        UserStatus user = FindObjectOfType<UserStatus>();
        if(boxNum >0){
            user.UpdateExpAndCharge();
        }else{
            //팝업으로 보낼수 없다 표시? 
        }
        
    }

    public void RemoveBoxes() {
        Debug.Log(GameControl.boxList.Count);
        for (int i = 0; i < GameControl.boxList.Count; i++) {
            Destroy(GameControl.boxList[i].gameObject);
        }
        GameControl.boxList.Clear();
    }

    private void UpdateNewTruck() {
        boxNum = 0;
        RemoveBoxes();
    }
    IEnumerator RunFadeOut()
    {
        bool flag = true;
        int timeCount = 0;
        while (true)
        { 
            if (flag)
            {
                if (stopFlag)
                {
                    Vector3 tmp = new Vector3(xSpeed * Time.deltaTime, 0.0f, 0.0f);
                    transform.position += tmp;
                    timeCount++;
                    yield return new WaitForSeconds(0.0f);
                    if (timeCount * Time.deltaTime > moveTime)
                    {
                        flag = false;
                        timeCount = 0;
                    }
                }
                else
                {
                    Vector3 tmp = new Vector3(xSpeed * Time.deltaTime, 0.0f, 0.0f);
                    transform.position -= tmp;
                    yield return new WaitForSeconds(0.0f);
                    break;
                }
            
            }
            else
            {
            
                yield return new WaitForSeconds(1.5f);
                flag = true;
            }
       
        }
    }

    bool CheckIsValidPosition (){
        foreach(Transform box in transform){
            Vector3 pos = FindObjectOfType<GameControl>().Round(box.position);
            
            if(FindObjectOfType<GameControl>().CheckIsInsideGrid(pos) == false){
                return false;
            }

            if(GameControl.grid[(int)pos.x, (int)pos.y, (int)pos.z] != null && GameControl.grid[(int)pos.x, (int)pos.y, (int)pos.z].parent != transform){
                
                return false;
            }
        }
        return true;
    }

    void OnTriggerEnter(Collider col)
    {
        
        stopFlag = false;
    }
    void OnTriggerStay(Collider col)
    {
        stopFlag = false;
    }
    public int getBoxNum(){
        return boxNum;
    }
}