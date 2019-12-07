using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeRandomBox : MonoBehaviour
{
    public GameObject[] boxes;
    private GameObject instance;

    private void Start(){
        // 빈 gameobject의 transform을 빌림
        // transform을 따로 new 할 수 없음
        /*
        nextBoxPosition = new GameObject().GetComponent<Transform>();
        nextBoxPosition.position = new Vector3(-1.17f, 11f, 0.0f);
        nextBoxPosition.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        nextBoxPosition.localScale = new Vector3(0,0,0);
        */

        makeRandomBox();
    }

    public void makeRandomBox(){
        int i = 0;
        
        if(GameManager.Instance.randomQueue.Count != 0){
            i = GameManager.Instance.randomQueue.Dequeue();
                    
        }
        else{
            GameManager.Instance.fillRandomQueue();
            i = GameManager.Instance.randomQueue.Dequeue();
        }
                
        instance = Instantiate(boxes[i], transform.position, Quaternion.identity) as GameObject;        
    }

}