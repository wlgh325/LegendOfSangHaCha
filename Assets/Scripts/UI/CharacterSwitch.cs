using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSwitch : MonoBehaviour{

    public Button leftBtn, rightBtn;
    public List<Transform> characterBuffer;
 
    bool updateUI;

    int currentIndex;

    // Start is called before the first frame update
    void Start(){
        currentIndex = 0;
        UpdateUI();
        UpdateCharacter();
    }

    // Update is called once per frame
    void Update(){
        GlobalVariables.characterIndex = currentIndex;
        if(updateUI){
            updateUI = false;
            UpdateUI();
        }
    }

    void UpdateCharacter(){
        for (int i = 0; i < characterBuffer.Count; i++){
            characterBuffer[i].gameObject.SetActive(i == currentIndex);
        }
    }

    void UpdateUI(){
        // 가장 왼쪽에 있지 않은 경우 => true(버튼 활성화)
        leftBtn.interactable = currentIndex > 0;

        // 가장 오른쪽에 있지 않은 경우 => true(버튼 활성화)
        rightBtn.interactable = currentIndex < characterBuffer.Count - 1;
    }

    public void SwitchCharacter(bool leftOrRight){
        // leftBtn이 true면 즉 leftBtn이 눌렸으면 -1 아니면 +1
        currentIndex += leftOrRight ? -1 : 1;

        updateUI = true;
        UpdateCharacter();
    }
}