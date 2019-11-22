using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeRandomBox : MonoBehaviour
{
    public GameObject[] boxes;
    private GameObject instance;
    public int[] BoxRange;

    void Start(){
        BoxRange = new int[] { 9, 12, 14};
        makeRandomBox();
    }

    public void makeRandomBox(){
        int i = Random.Range(0, BoxRange[FindObjectOfType<UserStatus>().GetBoxSizeLevel()]);
        instance = Instantiate(boxes[i], transform.position, Quaternion.identity) as GameObject;
    }
}