using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeRandomBox : MonoBehaviour
{
    public GameObject[] boxes;
    private GameObject instance;

    private void Start(){
        instance = Instantiate(boxes[0], transform.position, Quaternion.identity) as GameObject;
    }

    public void makeRandomBox(int i){
        instance = Instantiate(boxes[i], transform.position, Quaternion.identity) as GameObject;
    }


}