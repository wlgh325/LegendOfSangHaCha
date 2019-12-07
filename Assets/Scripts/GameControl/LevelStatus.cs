using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatus : MonoBehaviour {

    public static LevelStatus Instance = null;

    void Awake(){
        if(Instance == null){
            Instance = this;
        }
        else if(Instance != this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    
    private int[] boxSize;  // 박스 사이즈가 level 0일때 4이하만, level 1일때 6이하만... 
    private float[] truckSize; // 트럭 사이즈 증가. 수치 조정 need
    private float[] scoreSize; // 점수환산표 default 규모 증가. 수치 조정 need
    private int[] totalLevelExp;
    // Start is called before the first frame update
    public LevelStatus()
    {
        //init variable
        boxSize = new int[] {9, 12, 14};  
        truckSize = new float[] {1.0f, 1.4f, 2.0f};
        scoreSize = new float[] {1.0f, 1.4f, 2.0f}; 
        totalLevelExp = new int[] {10, 300, 1000, 2000, 5000};
    }
    public float getScoreSize(int scoreSizeLevel){
        return scoreSize[scoreSizeLevel];
    }

    public int getTotalLevelExp(int level){
        return totalLevelExp[level];
    }
}
