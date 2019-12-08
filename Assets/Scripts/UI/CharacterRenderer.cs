using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRenderer : MonoBehaviour
{
    public List<Transform> CharacterBuffer;
    void Start() {
        for (int i = 0; i < CharacterBuffer.Count; i++) {
            CharacterBuffer[i].gameObject.SetActive(i==GlobalVariables.characterIndex);
        }
    }
}