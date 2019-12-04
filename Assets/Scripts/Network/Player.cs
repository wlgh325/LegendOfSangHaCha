using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviourPun{

    private void Start(){

        if (photonView.IsMine)
        {
            Debug.Log("isMine");    
        }
        else
        {
            Debug.Log("!!!isMine");
        }
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }        
    }
}
