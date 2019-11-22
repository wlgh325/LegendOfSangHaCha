using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour{
    public Slider expBar;
    private int[] boxSize = {4, 6, 8};  // 박스 사이즈가 level 0일때 4이하만, level 1일때 6이하만... 
    private float[] truckSize = {1.0f, 1.4f, 2.0f}; // 트럭 사이즈 증가. 수치 조정 need
    private float[] scoreSize = {1.0f, 1.4f, 2.0f}; // 점수환산표 default 규모 증가. 수치 조정 need
    private float[] boxToScore = {1.0f, 1.1f, 1.2f, 1.3f, 1.5f, 2.0f}; // 0~30%, 30~50%, 50~70%, 70~90%, 90~99%, 100%
    private UserStatus character;
    public static int[] levelExp = {100, 300, 1000, 2000, 5000};
    public static int gridWidth = 16;
    public static int gridHeight = 14;
    public static int gridDepth = 32;
    public static int score;
    public static Transform[,,] grid = new Transform[gridWidth,gridHeight, gridDepth];

    void Start(){
        character = FindObjectOfType<UserStatus>();
        int level = character.GetLevel();
        score = 0;
    }

    // Update is called once per frame
    private void Update(){
        /*
        if(Input.GetKey(KeyCode.E)){
            expBar.value += exp;
        }
        */
    }
    public float GetTruckSize(int level) {
        return truckSize[level];
    }
    public float GetScoreSize(int level) {
        return scoreSize[level];
    }
    public float GetBoxToScore(int idx) {
        return boxToScore[idx];
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
