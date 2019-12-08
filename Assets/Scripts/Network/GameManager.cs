using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviourPunCallbacks, IPunObservable{
    // singleton
    public static GameManager Instance{
        get{
            if(instance == null) instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }
    public Image loadingPanel_main;
    private static GameManager instance;

    public Text scoreText;
    public Transform[] spawnTruckPosition;
    public GameObject[] playerTruckPrefab;

    public Transform spawnPlayerPosition;
    public GameObject playerPrefab;

    public Transform spawnBoxPosition;
    public GameObject makeBoxPrefab;
    private int truckSizeLevel;

    public Transform spawnNextBoxPosition;
    public GameObject makeNextBoxPrefab;
    

    private int[] playerScores;

    public bool isMaster;
    private int[] BoxRange;
    public Queue<int> randomQueue = new Queue<int>();
    public GameObject truckInstance;

    // time limit textUI
    public Text timeText;
    public float limitTime;
    private bool isGameover;

    private void Start() {
        isGameover = false;
        limitTime = 180.0f;

        isMaster = PhotonNetwork.IsMasterClient;
        BoxRange = new int[] { 9, 12, 14};
        playerScores = new[] {0, 0};
        truckSizeLevel = 0;

        fillRandomQueue();
        SpawnPlayer();
        SpawnBox();

        BoxControl.start = true;
    }

    private void Update(){
        if(BoxControl.start){
            if (!isGameover) {
                    int minute = (int)limitTime / 60;
                    int second = (int)(limitTime - minute * 60);

                    if (second >= 10)
                    {
                        if (limitTime < 11)
                        {
                            // 버닝
                            Time.timeScale = 8.0f; // 게임 속도
                            limitTime -= 0.125f * Time.deltaTime;
                            timeText.color = Color.red;
                            timeText.fontSize = 30;
                        }
                        else {
                            limitTime -= Time.deltaTime;
                        }
                        //timeText.text = "0" + minute + ":" + second;
                    }
                    else
                    {
                         if (limitTime <= 10)
                        {
                            // 버닝
                            Time.timeScale = 8.0f;
                            limitTime -= 0.125f * Time.deltaTime;
                            timeText.color = Color.red;
                            timeText.fontSize = 30;
                        }
                        else {
                           
                            limitTime -= Time.deltaTime;
                        }
                        //timeText.text = "0" + minute + ":0" + second;
                    }

                    if (limitTime < 0)
                    {
                        isGameover = true;
                    }
                }
                else {
                    // 게임 종료하고 점수 집계
                    FindObjectOfType<GameControl>().GameOver();
          
                }
                /*
                if(Input.GetKey(KeyCode.E)){
                    expBar.value += exp;
                }
                */

        }
    }

    private void SpawnPlayer(){
        // Truck, boxSpawn, player character 생성
        truckInstance = Instantiate(playerTruckPrefab[truckSizeLevel], spawnTruckPosition[truckSizeLevel].position, Quaternion.identity);
        Instantiate(playerPrefab, spawnPlayerPosition.position, Quaternion.identity);
        //PhotonNetwork.Instantiate(playerTruckPrefab.name, spawnTruckPosition.position, Quaternion.identity);
        //PhotonNetwork.Instantiate(playerPrefab.name, spawnPlayerPosition.position, Quaternion.identity);
    }
    public void SpawnTruck()
    {
        truckSizeLevel = UserStatus.Instance.GetTruckSizeLevel();
        Destroy(truckInstance);
        truckInstance = Instantiate(playerTruckPrefab[truckSizeLevel], spawnTruckPosition[truckSizeLevel].position, Quaternion.identity);
    }
    private void SpawnBox(){
        Instantiate(makeBoxPrefab, spawnBoxPosition.position, Quaternion.identity);
        Instantiate(makeNextBoxPrefab, spawnNextBoxPosition.position, Quaternion.identity);
        //PhotonNetwork.Instantiate(makeBoxPrefab.name, spawnBoxPosition.position, Quaternion.identity);
    }

    public override void OnLeftRoom() {
        SceneManager.LoadScene("LobbyScene");
        SceneManager.UnloadScene("GameScene");
    }

    // sync Method
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        var localPlayerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        // master이 보냄
        if(stream.IsWriting){
            stream.SendNext(playerScores[localPlayerIndex % 2]);
            stream.SendNext(limitTime);
        }
        else{
            // local에서 씀
            if(localPlayerIndex == 0)
                playerScores[1] = (int) stream.ReceiveNext();
            else
                playerScores[0] = (int) stream.ReceiveNext();
            
            limitTime = (float) stream.ReceiveNext();
            photonView.RPC("RPCUpdateTimeText", RpcTarget.All, limitTime);
            photonView.RPC("RPCUpdateScoreText", RpcTarget.All, playerScores[0].ToString(), playerScores[1].ToString());
        }

        
    }

    public void AddScore(int playerNumber, int score) {
        playerScores[playerNumber] += score;

        if(!PhotonNetwork.IsMasterClient){
            photonView.RPC("RPCUpdateScoreText", RpcTarget.All, playerScores[0].ToString(), playerScores[1].ToString());
        }
    }

    
    public void compareScore()
    {
        if (isMaster)
        {
            //자기가 마스터 0이 높아야 이김
            if(playerScores[0]> playerScores[1])
                isMaster = true;
            else
                isMaster = false;
        }
        else
        {
            if (playerScores[0] > playerScores[1])
            {
                isMaster = false;
            }
            else
            {
                isMaster = true;
            }
        }
    }

    public bool getIsMaster()
    {
        return isMaster;
    }

    [PunRPC]
    private void RPCUpdateScoreText(string player1ScoreText, string player2ScoreText)
    {
        scoreText.text = $"{player1ScoreText} : {player2ScoreText}";
    }

    [PunRPC]
    private void RPCUpdateTimeText(float limitTime){
        int minute = (int)limitTime / 60;
        int second = (int)(limitTime - minute * 60);

        timeText.text = "0" + minute + ":" + second;
    }

    public void fillRandomQueue(){
        for(int i=0; i<10; i++){
            int j = Random.Range(0, BoxRange[UserStatus.Instance.GetBoxSizeLevel()]);
            randomQueue.Enqueue(j);
        }
    }
}
