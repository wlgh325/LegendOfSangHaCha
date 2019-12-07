using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    bool isMaster;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameManager.Instance);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
