using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviourPunCallbacks{

    public Image loadingPanel_main;
    public Image[] loadingPanel_number;
    // singleton
    public static GameManager Instance{
        get{
            if(instance == null) instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    private static GameManager instance;

    public Text scoreText;
    public Transform[] spawnTruckPositions;
    public GameObject playerTruckPrefab;

    public Transform[] spawnPlayerPositions;
    public GameObject playerPrefab;

    public Transform[] spawnBoxpositions;
    public GameObject makeBoxPrefab;
    private int[] playerScores;

    private bool updated = false;

    private void Start() {
        playerScores = new[] {0, 0};
    }

    private void Update(){
        if (PhotonNetwork.PlayerList.Length < 2) return;

        if(updated){
            loadingPanel_main.gameObject.SetActive(false);

            /*
            for(int i=0; i<loadingPanel_number.Length; i++){
                loadingPanel_number[i].gameObject.SetActive(true);
                //StartCoroutine(waitSeconds());
                loadingPanel_number[i].gameObject.SetActive(false);
            }
            */
            SpawnPlayer();
        }
        
    }

    IEnumerator waitSeconds(){
        yield return new WaitForSeconds(1);
    }

    private void SpawnPlayer(){

        var localPlayerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;   //player 번호

        var spawnPosition = spawnTruckPositions[localPlayerIndex % spawnTruckPositions.Length];
        var spawnPlayerPosition = spawnPlayerPositions[localPlayerIndex % spawnPlayerPositions.Length];
        var spawnBoxPosition = spawnBoxpositions[localPlayerIndex % spawnBoxpositions.Length];

        // Truck, boxSpawn, player character 생성
        PhotonNetwork.Instantiate(playerTruckPrefab.name, spawnPosition.position, Quaternion.identity);
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPlayerPosition.position, Quaternion.identity);
        PhotonNetwork.Instantiate(makeBoxPrefab.name, spawnBoxPosition.position, Quaternion.identity);
        updated = false;
    }
    
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void AddScore(int playerNumber, int score)
    {
        playerScores[playerNumber - 1] += score;
        
        photonView.RPC("RPCUpdateScoreText", RpcTarget.All, playerScores[0].ToString(), playerScores[1].ToString());
    }

    
    [PunRPC]
    private void RPCUpdateScoreText(string player1ScoreText, string player2ScoreText)
    {
        scoreText.text = $"{player1ScoreText} : {player2ScoreText}";
    }    
}
