using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserStatus : MonoBehaviour
{
    private int level; // 다음 레벨로 가기 위한 경험치 요구치를 위한 레벨 status
    private int totalExp;
    private int exp;
    private int charge;
    private int boxSizeLevel; // 박스 size는 3,4,6,8이 있는데 레벨 0기준 3,4만
    private int truckSizeLevel;
    private int scoreSizeLevel;
    private float[] scoreRatio = {1.0f, 1.1f, 1.2f, 1.3f, 1.5f, 2.0f}; // 0~30%, 30~50%, 50~70%, 70~90%, 90~99%, 100%
    private LevelStatus levelStatus;
    public ProgressBarCircle expBar;

    // Start is called before the first frame update
    void Start()
    {
        levelStatus = new LevelStatus();
        level = 0;
        totalExp = levelStatus.totalLevelExp[level];
        exp = 0;
        charge = 0;
        boxSizeLevel = 0;
        truckSizeLevel = 0;
        scoreSizeLevel  = 0;
        expBar.BarValue = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (exp >= totalExp)
        {
            LevelUp();
            Debug.Log("levelUp");
        }
        // Debug.Log("BarValue: " + expBar.BarValue);
        // Debug.Log("Exp: " + exp);
        // Debug.Log("Total exp: " + totalExp);
        expBar.BarValue = (float)exp / totalExp * 100;
    }

    public int GetLevel()
    {
        return level;
    }
    public void LevelUp()
    {
        level += 1;
        exp -= totalExp;
        totalExp = levelStatus.totalLevelExp[level];
        ChooseAttribute();
        PopupLevelUpEvent();
    }
    private void PopupLevelUpEvent() {

    }
    public void ChooseAttribute()
    {
        FindObjectOfType<BoxSizeStatus>().triggerLeverUp();
        FindObjectOfType<scoreSizeStatus>().triggerLeverUp();
        FindObjectOfType<TruckSizeStatus>().triggerLeverUp();

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
    public int GetExp(){
        return exp;
    }
    public int GetTotalExp() {
        return totalExp;
    }
     public int GetCharge(){
        return charge;
    }
    public void UpdateExpAndCharge() {
        int score = CalculateScore();
        SetExp(score);
        SetCharge(score);
        Debug.Log(score);
    }
    private int CalculateScore() {
        int truckVolume = GameControl.gridWidth * GameControl.gridHeight * GameControl.gridDepth;
        int boxVolume = FindObjectOfType<BoxControl>().getBoxNum();
        int ratio = boxVolume / truckVolume * 100;

        if (ratio >= 0 && ratio < 30) {
            return (int)(boxVolume * scoreRatio[0] * levelStatus.getScoreSize(scoreSizeLevel));
        } else if (ratio < 50) {
            return (int)(boxVolume * scoreRatio[1] * levelStatus.getScoreSize(scoreSizeLevel));
        } else if (ratio < 70) {
            return (int)(boxVolume * scoreRatio[2] * levelStatus.getScoreSize(scoreSizeLevel));
        } else if (ratio < 90) {
            return (int)(boxVolume * scoreRatio[3] * levelStatus.getScoreSize(scoreSizeLevel));
        } else if (ratio <= 99) {
            return (int)(boxVolume * scoreRatio[4] * levelStatus.getScoreSize(scoreSizeLevel));
        } else {
            return (int)(boxVolume * scoreRatio[5] * levelStatus.getScoreSize(scoreSizeLevel));
        }
        
    }
}
