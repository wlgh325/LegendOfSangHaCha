using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeRandomBox : MonoBehaviour
{
    public GameObject[] boxes;
    private GameObject instance;
    Queue<int> randomQueue = new Queue<int>();
    private int[] BoxRange;

    private void Start(){
        BoxRange = new int[] { 9, 12, 14};
        fillRandomQueue();
        makeRandomBox();
    }

    public void makeRandomBox(){
        int i = 0;
        if(randomQueue != null){
            i = randomQueue.Dequeue();
        }
        else{
            fillRandomQueue();
            i = randomQueue.Dequeue();
        }

        instance = Instantiate(boxes[i], transform.position, Quaternion.identity) as GameObject;
    }

    private void fillRandomQueue(){
        for(int i=0; i<10; i++){
            int j = Random.Range(0, BoxRange[UserStatus.Instance.GetBoxSizeLevel()]);
            randomQueue.Enqueue(j);
        }
    }
}