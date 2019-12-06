﻿using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

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
    public Transform spawnTruckPosition;
    public GameObject playerTruckPrefab;

    public Transform spawnPlayerPosition;
    public GameObject playerPrefab;

    public Transform spawnBoxPosition;
    public GameObject makeBoxPrefab;


    private int[] playerScores;

    private void Start() {
        Debug.Log(UserStatus.Instance.level);
        playerScores = new[] {0, 0};
        SpawnPlayer();
        SpawnBox();
    }

    private void Update(){
        //if (PhotonNetwork.PlayerList.Length < 2) return;

        loadingPanel_main.gameObject.SetActive(false);
        BoxControl.start = true;
            /*
            for(int i=0; i<loadingPanel_number.Length; i++){
                loadingPanel_number[i].gameObject.SetActive(true);
                //StartCoroutine(waitSeconds());
                loadingPanel_number[i].gameObject.SetActive(false);
            }
            */
    }

    IEnumerator waitSeconds(){
        yield return new WaitForSeconds(1);
    }

    private void SpawnPlayer(){
        // Truck, boxSpawn, player character 생성
        Instantiate(playerTruckPrefab, spawnTruckPosition.position, Quaternion.identity);
        Instantiate(playerPrefab, spawnPlayerPosition.position, Quaternion.identity);
        //PhotonNetwork.Instantiate(playerTruckPrefab.name, spawnTruckPosition.position, Quaternion.identity);
        //PhotonNetwork.Instantiate(playerPrefab.name, spawnPlayerPosition.position, Quaternion.identity);
    }
    
    private void SpawnBox(){
        Instantiate(makeBoxPrefab, spawnBoxPosition.position, Quaternion.identity);
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


    [PunRPC]
    private void RPCUpdateScoreText(string player1ScoreText, string player2ScoreText)
    {
        scoreText.text = $"{player1ScoreText} : {player2ScoreText}";
    }
}
