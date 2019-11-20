using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeRandomBox : MonoBehaviour
{
    public GameObject[] boxes;
    private GameObject instance;

    void Start(){

        makeRandomBox();
    }

    public void makeRandomBox(){
        int i = Random.Range(0, boxes.Length);
        instance = Instantiate(boxes[i], transform.position, Quaternion.identity) as GameObject;
    }
}