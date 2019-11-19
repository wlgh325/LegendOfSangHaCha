using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class BoxControl : MonoBehaviour{

    private float box_size = 0.5f;
    private float speed = 1;

    private float directionX, directionY;
    private bool rotateX, rotateY;

    private Rigidbody2D rb;

    private float angle = 90;

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        directionX = CrossPlatformInputManager.GetAxis("Horizontal");
        directionY = CrossPlatformInputManager.GetAxis("Vertical");


        rotateX = CrossPlatformInputManager.GetButtonDown("rotateX");
        rotateY = CrossPlatformInputManager.GetButtonDown("rotateY");

        if(rotateY ){
            transform.Rotate(new Vector3(0, angle, 0), Space.Self);
            CrossPlatformInputManager.SetButtonUp("rotateY");
        }
        if (rotateX){
            transform.Rotate(new Vector3(angle, 0, 0), Space.Self);
            CrossPlatformInputManager.SetButtonUp("rotateX");
        }

    }

    private void FixedUpdate(){
        rb.velocity = new Vector2(directionX, directionY);
    }
}
