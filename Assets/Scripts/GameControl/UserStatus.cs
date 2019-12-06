using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserStatus : MonoBehaviourPunCallbacks {

    public static UserStatus Instance = null;

    void Awake(){
        if(Instance == null){
            Instance = this;
        }
        else if(Instance != this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public int level; // 다음 레벨로 가기 위한 경험치 요구치를 위한 레벨 status
    private int totalExp;
    private int exp;
    private int charge;
    private int boxSizeLevel; // 박스 size는 3,4,6,8이 있는데 레벨 0기준 3,4만
    private int truckSizeLevel;
    private int scoreSizeLevel;
    private float[] scoreRatio = {1.0f, 1.1f, 1.2f, 1.3f, 1.5f, 2.0f}; // 0~30%, 30~50%, 50~70%, 70~90%, 90~99%, 100%

    public ProgressBarCircle expBar;
    public Text levelText;

    void Start() {
        level = 0;
        levelText.text = "Level : " + (level + 1).ToString();
        totalExp = LevelStatus.Instance.getTotalLevelExp(level);
        
        exp = 0;
        charge = 0;
        boxSizeLevel = 0;
        truckSizeLevel = 0;
        scoreSizeLevel  = 0;
        //expBar.BarValue = 0.0f;
    }

    void Update() {

        if (exp >= totalExp)
        {
            LevelUp();
            GetComponent<UIUpdate>().showLevelUpStatus();
            levelText.text = "Level : " + (level + 1).ToString();            
        }
        // Debug.Log("BarValue: " + expBar.BarValue);
        // Debug.Log("Exp: " + exp);
        // Debug.Log("Total exp: " + totalExp);
        //expBar.BarValue = (float)exp / totalExp * 100;
    }

    public int GetLevel()
    {
        return level;
    }
    public void LevelUp()
    {
        level += 1;
        exp -= totalExp;
        totalExp = LevelStatus.Instance.getTotalLevelExp(level);
        ChooseAttribute();
        PopupLevelUpEvent();
    }

    private void PopupLevelUpEvent() {

    }
    public void ChooseAttribute() {
        // 특성 선택
        BoxSizeStatus.Instance.triggerLevelUp();
        scoreSizeStatus.Instance.triggerLevelUp();
        TruckSizeStatus.Instance.triggerLevelUp();
    }
    public void SetExp(int plus)
    {
        exp += plus;
    }
    public void SetCharge(int plus)
    {
        charge += plus;
    }

    public int GetBoxSizeLevel(){
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
        
        var localPlayerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        GameManager.Instance.AddScore(localPlayerIndex % 2, score);

        SetExp(score);
        SetCharge(score);
        //Debug.Log(score);
    }

    private int CalculateScore() {
        int truckVolume = GameControl.gridWidth * GameControl.gridHeight * GameControl.gridDepth;
        int boxVolume = FindObjectOfType<BoxControl>().getBoxNum();
        int ratio = boxVolume / truckVolume * 100;

        if (ratio >= 0 && ratio < 30) {
            return (int)(boxVolume * scoreRatio[0] * LevelStatus.Instance.getScoreSize(scoreSizeLevel));
        } else if (ratio < 50) {
            return (int)(boxVolume * scoreRatio[1] * LevelStatus.Instance.getScoreSize(scoreSizeLevel));
        } else if (ratio < 70) {
            return (int)(boxVolume * scoreRatio[2] * LevelStatus.Instance.getScoreSize(scoreSizeLevel));
        } else if (ratio < 90) {
            return (int)(boxVolume * scoreRatio[3] * LevelStatus.Instance.getScoreSize(scoreSizeLevel));
        } else if (ratio <= 99) {
            return (int)(boxVolume * scoreRatio[4] * LevelStatus.Instance.getScoreSize(scoreSizeLevel));
        } else {
            return (int)(boxVolume * scoreRatio[5] * LevelStatus.Instance.getScoreSize(scoreSizeLevel));
        }
        
    }
}
