using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour{
    public Slider expBar;
    private float exp;
    private float[] level_exp = {0.0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f};
    public int level;

    private float box_size = 0.5f;

    void Start(){
        exp = level_exp[level];
    }

    // Update is called once per frame
    private void Update(){
        if(Input.GetKey(KeyCode.E)){
            expBar.value += exp;
        }
        
    }

        /*
        Box Control Button Function
    */
    public void ClickupBtn(){
        Debug.Log("Up");
    }

    public void ClickdownBtn(){
        Debug.Log("Down");
        transform.position += new Vector3(0, 1, 0);
    }

    public void ClickleftBtn(){
        Vector3 position = transform.position;

       
    }

    public void ClickrightBtn(){
        Vector3 position = transform.position;
        
    }
}
