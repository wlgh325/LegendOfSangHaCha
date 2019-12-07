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
    public Image[] loadingPanel_number;
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

    private void Start() {
        isMaster = PhotonNetwork.IsMasterClient;
        BoxRange = new int[] { 9, 12, 14};
        fillRandomQueue();

        if(loadingPanel_number != null){
            for(int i=0; i<loadingPanel_number.Length; i++){
                loadingPanel_number[i].gameObject.SetActive(false);
            }
        }
        
        playerScores = new[] {0, 0};
        truckSizeLevel = 0;
        SpawnPlayer();
        SpawnBox();
    }

    private void Update(){
        //if (PhotonNetwork.PlayerList.Length < 2) return;
        //loadingPanel_main.gameObject.SetActive(false);
        //BoxControl.start = true;            
    }

    IEnumerator waitSeconds(){
        yield return new WaitForSeconds(1);
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
            stream.SendNext(playerScores[localPlayerIndex]);
        }
        else{
            // local에서 씀
            if(localPlayerIndex == 0)
                playerScores[1] = (int) stream.ReceiveNext();
            else
                playerScores[0] = (int) stream.ReceiveNext();
            
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
            {
                isMaster = true;
            }
            else
            {
                isMaster = false;
            }
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

    public void fillRandomQueue(){
        for(int i=0; i<10; i++){
            int j = Random.Range(0, BoxRange[UserStatus.Instance.GetBoxSizeLevel()]);
            randomQueue.Enqueue(j);
        }
    }
}
