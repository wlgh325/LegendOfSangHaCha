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

    public static int gridHeight = 10;
    public static float gridWidth = 1.3f;

    void Start(){
        exp = level_exp[level];
    }

    // Update is called once per frame
    private void Update(){
        /*
        if(Input.GetKey(KeyCode.E)){
            expBar.value += exp;
        }
        */
    }

    public bool CheckIsInsideGrid(Vector3 pos){
        //return true;
        return ((int)pos.x >= 0 && pos.x <= gridWidth && (int)pos.y >= 0
        && (int)pos.y <= gridHeight && (int)pos.z >= 0);
    }

    // 반올림 하는 function
    public Vector3 Round(Vector3 pos){
        return new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
    }
}
