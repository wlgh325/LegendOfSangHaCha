using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
    private float screenWidth;
    private int cameraIndex;
    public Camera[] cameras;
    private float doubleTapTouchTime;
    float touchDuration;
    Touch touch;

    GameObject leftWall;
    GameObject rightWall;
    // cameras[0] : left
    // cameras[1] : center
    // cameras[2] : Right
    void Start(){
        screenWidth = Screen.width;
        cameraIndex = 1;
        cameras[0].gameObject.SetActive(false);
        cameras[2].gameObject.SetActive(false);
    }

    // https://forum.unity.com/threads/single-tap-double-tap-script.83794/
    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            touchDuration += Time.deltaTime;
            touch = Input.GetTouch(0);

            if (touch.position.x < screenWidth / 2)
            {
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    if (touch.phase == TouchPhase.Ended && touchDuration < 0.2f)
                        StartCoroutine("singleOrDoubleLeft");
                }
            }

            // right camera
            if (touch.position.x > screenWidth / 2)
            {
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    if (touch.phase == TouchPhase.Ended && touchDuration < 0.2f)
                        StartCoroutine("singleOrDoubleRight");
                }
            }
        }
        else
            touchDuration = 0.0f;


    }
    IEnumerator singleOrDoubleLeft()
    {
        yield return new WaitForSeconds(0.3f);

        if (touch.tapCount == 2)
        {
            //this coroutine has been called twice. We should stop the next one here otherwise we get two double tap
            // center -> left
            if (cameraIndex == 1)
            {
                cameras[cameraIndex].gameObject.SetActive(false);
                cameras[0].gameObject.SetActive(true);

                // UnShow leftWall
                if(leftWall == null)
                
                    leftWall = GameObject.FindWithTag("LeftWall");

                leftWall.SetActive(false);
                
                cameraIndex = 0;
                StopCoroutine("singleOrDoubleLeft");
            }
            // right -> center
            else if (cameraIndex == 2)
            {
                cameras[cameraIndex].gameObject.SetActive(false);
                cameras[1].gameObject.SetActive(true);
                
                // Show rightWall
                if(rightWall == null)
                    rightWall = GameObject.FindWithTag("Rightall");
                rightWall.SetActive(true);
                
                cameraIndex = 1;
                StopCoroutine("singleOrDoubleLeft");
            }
        }
    }

    IEnumerator singleOrDoubleRight()
    {
        yield return new WaitForSeconds(0.3f);

        if (touch.tapCount == 2)
        {
            //this coroutine has been called twice. We should stop the next one here otherwise we get two double tap

            // left -> center
            if (cameraIndex == 0)
            {
                cameras[cameraIndex].gameObject.SetActive(false);
                cameras[1].gameObject.SetActive(true);
                
                // Show leftWall
                if(leftWall == null)
                    leftWall = GameObject.FindWithTag("LeftWall");
                leftWall.SetActive(true);

                cameraIndex = 1;
                StopCoroutine("singleOrDoubleRight");
            }
            // center -> right
            else if (cameraIndex == 1)
            {
                cameras[cameraIndex].gameObject.SetActive(false);
                cameras[2].gameObject.SetActive(true);
                
                // UnShow rightWall
                if(rightWall == null)
                    rightWall = GameObject.FindWithTag("RightWall");
                rightWall.SetActive(false);

                cameraIndex = 2;
                StopCoroutine("singleOrDoubleRight");
            }
        }
    }

/*
    public void left(){
            if (cameraIndex == 1)
            {
                cameras[cameraIndex].gameObject.SetActive(false);
                cameras[0].gameObject.SetActive(true);

                // UnShow leftWall
                if(leftWall == null)
                
                    leftWall = GameObject.FindWithTag("LeftWall");

                leftWall.SetActive(false);
                
                cameraIndex = 0;
                StopCoroutine("singleOrDoubleLeft");
            }
            // right -> center
            else if (cameraIndex == 2)
            {
                cameras[cameraIndex].gameObject.SetActive(false);
                cameras[1].gameObject.SetActive(true);
                
                // Show rightWall
                if(rightWall == null)
                    rightWall = GameObject.FindWithTag("Rightall");
                rightWall.SetActive(true);
                
                cameraIndex = 1;
                StopCoroutine("singleOrDoubleLeft");
            }
    }

    public void right(){
        // left -> center
            if (cameraIndex == 0)
            {
                cameras[cameraIndex].gameObject.SetActive(false);
                cameras[1].gameObject.SetActive(true);
                
                // Show leftWall
                if(leftWall == null)
                    leftWall = GameObject.FindWithTag("LeftWall");
                leftWall.SetActive(true);

                cameraIndex = 1;
                StopCoroutine("singleOrDoubleRight");
            }
            // center -> right
            else if (cameraIndex == 1)
            {
                cameras[cameraIndex].gameObject.SetActive(false);
                cameras[2].gameObject.SetActive(true);
                
                // UnShow rightWall
                if(rightWall == null)
                    rightWall = GameObject.FindWithTag("RightWall");
                rightWall.SetActive(false);

                cameraIndex = 2;
                StopCoroutine("singleOrDoubleRight");
            }
    }
    */
}