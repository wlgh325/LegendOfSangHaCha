using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class GotoLobby : MonoBehaviour {
    private bool isClickedGotoLobby;

    private void Start(){
        isClickedGotoLobby = CrossPlatformInputManager.GetButtonDown("GotoLobby");
    }

    private void Update(){
        if(isClickedGotoLobby){
            SceneManager.LoadScene("LobbyScene");
        }
    }
}
