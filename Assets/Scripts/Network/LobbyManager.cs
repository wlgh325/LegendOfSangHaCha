using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks{
    
    // gameVersion이 같아야 게임 매칭이 된다
    private readonly string gameVersion = "1";

    public Text connectionInfoText;
    public Button startBtn;

    public Image fadeImage;

    float totalFadeTime = 1;

    public Color destColor;
    Color oriColor;

    private string gameRoomName = "GameRoom";
    public bool isCreatedRoom = false;

    // Start is called before the first frame update
    void Start(){
        oriColor = fadeImage.color;
        PhotonNetwork.GameVersion  = gameVersion;

        // master server에 접속 시도
        PhotonNetwork.ConnectUsingSettings();

        startBtn.interactable = false;
        connectionInfoText.text = "Connecting to Master Server...";
    }

    // Master Server에 접속 성공시 자동 실행
    public override void OnConnectedToMaster(){
        startBtn.interactable = true;
        connectionInfoText.text = "Online : Connected to Master Server";
    }

    // 접속 시도 실패
    // 접속이 끊어진 경우 자동 실행
    public override void OnDisconnected(DisconnectCause cause){
        startBtn.interactable = false;
        connectionInfoText.text = $"Offline : Connection Disabled{cause.ToString()} - Try reconnectiong...";

        // 재접속 시도
        PhotonNetwork.ConnectUsingSettings();

    }

    public void Connect(){
        // 접속 시도 끝나기전에 다시 누르는것 방지
        startBtn.interactable = false;

        if(PhotonNetwork.IsConnected){
            connectionInfoText.text = "Connecting to Random Room...";

            // 빈 방으로 접속
            PhotonNetwork.JoinRandomRoom();

            /*
            if(isCreatedRoom == false){
                Debug.Log("create room");
                PhotonNetwork.CreateRoom(gameRoomName, new RoomOptions { MaxPlayers = 2});
                
            }
            else{
                Debug.Log("join Room");
                PhotonNetwork.JoinRoom(gameRoomName);
            }
            */
        }
        else{
            // 접속 버튼 눌렀는데 갑자기 끊긴 경우
            // 재접속 시도
            connectionInfoText.text = "Offline : Connection Disabled - Try reconnecting...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnCreatedRoom(){
        isCreatedRoom = true;
        connectionInfoText.text = "Success Creating Room!";
    }

    // 빈방이 없어서 Room에 접속하는데 실패한 경우
    public override void OnJoinRandomFailed(short returnCode, string message){
        // 빈방을 만들어줌
        connectionInfoText.text = "There is no empty room, Creating new room";

        // CreateRoom(방이름, roomoptions)
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2});
    }

    // 방 접속에 성공할때 실행
    public override void OnJoinedRoom(){
        connectionInfoText.text = "Connected with Room";
        SceneManager.LoadScene("LoadingScene");
    }

    public string getGameRoomName(){
        return gameRoomName;
    }
}
