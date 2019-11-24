using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float screenWidth;
    private int cameraIndex;
    public Camera[] cameras;
    public GameObject truckLeft;
    public GameObject truckRight;

    // cameras[0] : left
    // cameras[1] : center
    // cameras[2] : Right
    void Start(){
        screenWidth = Screen.width;
        cameraIndex = 1;
        cameras[0].gameObject.SetActive(false);
        cameras[2].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update(){
        if(Input.touchCount > 0){
            GameObject tempObj = null;
            if(Input.GetTouch (0).position.x < screenWidth / 2){
                // center -> left
                if(cameraIndex == 1){
                    cameras[cameraIndex].gameObject.SetActive(false);
                    cameras[0].gameObject.SetActive(true);
                    tempObj = GameObject.Find("Cube1");
                    tempObj.SetActive(false);
                }
                // right -> center
                else if(cameraIndex == 2){
                    cameras[cameraIndex].gameObject.SetActive(false);
                    cameras[1].gameObject.SetActive(true);
                    tempObj = GameObject.Find("Cube3");
                    tempObj.SetActive(true);
                }
            }
            // right camera
            if(Input.GetTouch (0).position.x > screenWidth / 2){
                // left -> center
                if(cameraIndex == 0){
                    cameras[cameraIndex].gameObject.SetActive(false);
                    cameras[1].gameObject.SetActive(true);
                    tempObj = GameObject.Find("Cube1");
                    tempObj.SetActive(true);
                }
                // center -> right
                else if(cameraIndex == 1){
                    cameras[cameraIndex].gameObject.SetActive(false);
                    cameras[2].gameObject.SetActive(true);
                    tempObj = GameObject.Find("Cube3");
                    tempObj.SetActive(false);
                }
            }
        }
    }

    public void leftClicked(){
        // center -> left
        if(cameraIndex == 1){
            cameras[cameraIndex].gameObject.SetActive(false);
            cameras[0].gameObject.SetActive(true);
            truckLeft.SetActive(false);
            cameraIndex = 0;
        }
        // right -> center
        else if(cameraIndex == 2){
            cameras[cameraIndex].gameObject.SetActive(false);
            cameras[1].gameObject.SetActive(true);
            truckRight.SetActive(true);
            cameraIndex = 1;
        }
    }

    public void rightClicked(){        
        // left -> center
        if(cameraIndex == 0){
            cameras[cameraIndex].gameObject.SetActive(false);
            cameras[1].gameObject.SetActive(true);
            truckLeft.SetActive(true);
            cameraIndex = 1;
        }
        // center -> right
        else if(cameraIndex == 1){
            cameras[cameraIndex].gameObject.SetActive(false);
            cameras[2].gameObject.SetActive(true);
            truckRight.SetActive(false);
            cameraIndex = 2;
        }
    }
    
}
