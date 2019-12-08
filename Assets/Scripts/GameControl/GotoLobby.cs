using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class GotoLobby : MonoBehaviour {
    private void Start(){

    }

    private void Update(){

    }

    public void gotoLobby(){
        SceneManager.LoadScene("LobbyScene");
    }
}
