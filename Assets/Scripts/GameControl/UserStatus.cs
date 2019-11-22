using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserStatus : MonoBehaviour
{
    private int level; // 다음 레벨로 가기 위한 경험치 요구치를 위한 레벨 status
    private int totalExp;
    private int exp;
    private int charge;
    private int boxSizeLevel; // 박스 size는 3,4,6,8이 있는데 레벨 0기준 3,4만
    private int truckSizeLevel;
    private int scoreSizeLevel;

    // Start is called before the first frame update
    void Start()
    {
        level = 0;
        totalExp = GameControl.levelExp[level];
        exp = 0;
        charge = 0;
        boxSizeLevel = 0;
        truckSizeLevel = 0;
        scoreSizeLevel  = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (exp >= totalExp)
        {
            LevelUp();
        }
    }

    public int GetLevel()
    {
        return level;
    }
    public void LevelUp()
    {
        level += 1;
        exp -= totalExp;
        totalExp = GameControl.levelExp[level];
        ChooseAttribute();
    }
    public void ChooseAttribute()
    {
        // 특성 선택
    }
    public void SetExp(int plus)
    {
        exp += plus;
    }
    public void SetCharge(int plus)
    {
        charge += plus;
    }
    public int GetBoxSizeLevel()
    {
        return boxSizeLevel;
    }
    public void BoxSizeLevelUP()
    {
        boxSizeLevel += 1;
    }
    public int GetTruckSizeLevel()
    {
        return truckSizeLevel;
    }
    public void TruckSizeLevelUp()
    {
        truckSizeLevel += 1;
    }
    public int GetScoreSizeLevel()
    {
        return scoreSizeLevel;
    }
    public void ScoreSizeLevelUp()
    {
        scoreSizeLevel += 1;
    }
}
