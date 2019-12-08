using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CountDown : MonoBehaviour
{
    public Image[] loadingPanel_number;

    void Start(){
        if(loadingPanel_number != null){
        for(int i=0; i<loadingPanel_number.Length; i++){
                loadingPanel_number[i].gameObject.SetActive(false);
            }
        }

        PhotonNetwork.AutomaticallySyncScene = true;
        StartCoroutine(showWaitImage());
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void OnApplicationQuit(){
        PhotonNetwork.LeaveRoom();
    }
}
