using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class LoadingManager : MonoBehaviourPunCallbacks{
    
    public Image loadingPanel_main;

    public Image[] loadingPanel_number;

    public Button startButton;
    public Text numberTxt;

    private bool isClickedStartBtn;


    void Awake(){
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start(){
        
        startButton.interactable = false;
        startButton.gameObject.SetActive(false);
        numberTxt.text = PhotonNetwork.PlayerList.Length.ToString() + " / 2";
        if(loadingPanel_number != null){
            for(int i=0; i<loadingPanel_number.Length; i++){
                loadingPanel_number[i].gameObject.SetActive(false);
            }
        }
    }

    void Update(){
        isClickedStartBtn = CrossPlatformInputManager.GetButtonDown("GoStart");

        numberTxt.text = PhotonNetwork.PlayerList.Length.ToString() + " / 2";
        if (PhotonNetwork.PlayerList.Length == 2){
            loadingPanel_main.gameObject.SetActive(false);
            if(PhotonNetwork.IsMasterClient){
                startButton.gameObject.SetActive(true);
                startButton.interactable = true;
            }
                
        }
        
        if(isClickedStartBtn){
            startGame();
        }
    }

    public void startGame(){
        StartCoroutine(showWaitImage());
    }

    IEnumerator showWaitImage(){
        for(int i=0; i<loadingPanel_number.Length; i++){
            loadingPanel_number[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            loadingPanel_number[i].gameObject.SetActive(false);
        }
    
        if(PhotonNetwork.IsMasterClient){
            PhotonNetwork.LoadLevel("GameScene");
        }
    }
}