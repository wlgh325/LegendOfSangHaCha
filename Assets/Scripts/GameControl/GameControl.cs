using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour{
    public static GameControl
    Instance{
        get{
            if(instance == null) instance = FindObjectOfType<GameControl>();
            return instance;
        }
    }
    private static GameControl instance;

    public Text timeText;
    public static int gridWidth;
    public static int gridHeight;
    public static int gridDepth;
    public int[,] gridSizes;
    public static Transform[,,] grid;
    public static List<BoxControl> boxList;
    private int gridLevel;

    void Start(){
        gridLevel = 0;
        gridSizes = new int[3,3]{{5,5,7},{7,7,9},{9,9,11}};
        gridWidth = gridSizes[gridLevel,0];
        gridHeight = gridSizes[gridLevel,1];
        gridDepth = gridSizes[gridLevel,2];
        grid = new Transform[gridWidth,gridHeight, gridDepth];
        boxList = new List<BoxControl>();
       
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
        SceneManager.LoadScene("GameOver");
    }
    public void updateGridLevel(){
        this.gridLevel += 1;
        gridWidth = gridSizes[gridLevel, 0];
        gridHeight = gridSizes[gridLevel, 1];
        gridDepth = gridSizes[gridLevel, 2];
        grid = new Transform[gridWidth,gridHeight, gridDepth];
    }
}
