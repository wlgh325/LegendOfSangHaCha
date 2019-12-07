using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeNextBox : MonoBehaviour {
    public GameObject[] boxes;
    private GameObject instance;
    
    private void Start(){

        showNextBox();
    }

    public void showNextBox(){
        int i = 0;

        Destroy(instance);
        if(GameManager.Instance.randomQueue.Count != 0){
            i = GameManager.Instance.randomQueue.Peek();
        }
        else{
            GameManager.Instance.fillRandomQueue();
            i = GameManager.Instance.randomQueue.Peek();
        }
        instance = Instantiate(boxes[i], transform.position, Quaternion.identity) as GameObject;
    }
}
