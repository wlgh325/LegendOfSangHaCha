using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class BoxControl : MonoBehaviour
{

    private float box_size = 0.5f;
    private float speed = 1;

    private float directionX, directionY;
    private bool rotateX, rotateY;
    private bool stopFlag = true;

   

    private float angle = 90;

    public float xSpeed = 5.0f;
    public float moveTime = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
       //rb = GetComponent<Rigidbody2D>();
        StartCoroutine("RunFadeOut");
    }

    // Update is called once per frame
    void Update()
    {

        directionX = CrossPlatformInputManager.GetAxis("Horizontal");
        directionY = CrossPlatformInputManager.GetAxis("Vertical");


        rotateX = CrossPlatformInputManager.GetButtonDown("rotateX");
        rotateY = CrossPlatformInputManager.GetButtonDown("rotateY");

        if (rotateY)
        {
            transform.Rotate(new Vector3(0, angle, 0), Space.Self);
            CrossPlatformInputManager.SetButtonUp("rotateY");
        }
        if (rotateX)
        {
            transform.Rotate(new Vector3(angle, 0, 0), Space.Self);
            CrossPlatformInputManager.SetButtonUp("rotateX");
        }

    }

    private void FixedUpdate()
    {
        
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
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("hello");
        stopFlag = false;
    }
    void OnTriggerStay(Collider col)
    {
        Debug.Log("hello2");
        stopFlag = false;
    }
}