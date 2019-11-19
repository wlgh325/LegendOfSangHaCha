using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class BoxControl : MonoBehaviour{
    private float one_step = 0.1f;
    private float speed = 1;

    private float directionX, directionY;

    private bool isClickedLeftBtn, isClickedRightBtn, isClickedDownBtn;
    private bool isClickedRotateX, isClickedRotateY;
    private bool stopFlag = true;

    private float angle = 90;

    public float xSpeed = 5.0f;
    public float moveTime = 0.05f;

    float fall = 0;
    public float fallSpeed = 1f;

    void Start(){
       //StartCoroutine("RunFadeOut");
    }

    // Update is called once per frame
    void Update(){
        isClickedRotateX = CrossPlatformInputManager.GetButtonDown("rotateX");
        isClickedRotateY = CrossPlatformInputManager.GetButtonDown("rotateY");

        isClickedLeftBtn = CrossPlatformInputManager.GetButtonDown("GoLeft");
        isClickedRightBtn = CrossPlatformInputManager.GetButtonDown("GoRight");
        isClickedDownBtn = CrossPlatformInputManager.GetButtonDown("GoDown");

        // check Button clicked
        if (isClickedRotateY){
            transform.Rotate(new Vector3(0, angle, 0), Space.Self);
        }
        else if (isClickedRotateX){
            transform.Rotate(new Vector3(angle, 0, 0), Space.Self);
        }
        else if (isClickedLeftBtn){
            transform.position += new Vector3(-one_step, 0, 0);

            if(!CheckIsValidPosition()){
                transform.position += new Vector3(one_step, 0, 0);
            }
        }
        else if (isClickedRightBtn){
            transform.position += new Vector3(one_step, 0, 0);
            Debug.Log(transform.position);
            if(!CheckIsValidPosition()){
                transform.position += new Vector3(-one_step, 0, 0);
            }
        }
        else if (isClickedDownBtn){
            transform.position += new Vector3(0, 0, one_step);

            if(!CheckIsValidPosition()){
                transform.position += new Vector3(0, 0, -one_step);
            }
        }
        
        /*
        // fall box
        if(Time.time - fall >= fallSpeed){
            transform.position += new Vector3(0, 0, one_step);

            if(!CheckIsValidPosition()){
                transform.position += new Vector3(0, 0, -one_step);
            }
            fall = Time.time;
        }
        */
    }

    private void FixedUpdate(){
        
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
            Debug.Log("Round");
            Debug.Log(pos);
            if(FindObjectOfType<GameControl>().CheckIsInsideGrid(pos) == false){
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
}