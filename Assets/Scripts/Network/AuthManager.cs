using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AuthManager : MonoBehaviour{

    public Image fadeImage;
    float totalFadeTime = 1;

    public Color destColor;
    Color oriColor;



    // firebase를 구동할 수 있는 상황인가??
    public bool IsFirebaseReady {get; private set;}
    // 로그인 과정이 진행중인가?? (로그인중인데 또 로그인하지 않도록)
    public bool IsSignInOnProgress {get; private set;}

    public InputField emailField;
    public InputField passwordField;
    public Button startBtn;

    // firebaseapp 관리하는 object
    public static FirebaseApp firebaseApp;

    // firebaseAuth 관리하는 object
    public static FirebaseAuth firebaseAuth;

    // email, pw에 대응되는 user 정보를 가져옴
    public static FirebaseUser User;

    // Start is called before the first frame update
    void Start(){
        oriColor = fadeImage.color;

        startBtn.interactable = false;
        //CouontinueWithOnMainThread
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var result = task.Result;
            if(result != DependencyStatus.Available){
                Debug.LogError(result.ToString());
                IsFirebaseReady = false;
            }
            else{
                IsFirebaseReady = true;
                firebaseApp = FirebaseApp.DefaultInstance;  // firebaseapp의 전체를 관리하는 object를 가져옴
                firebaseAuth = FirebaseAuth.DefaultInstance;
            }

            startBtn.interactable = IsFirebaseReady;
        }); // 현재 firebase를 구동할 수 있는 환경인지 check
    }

    public void SignIn(){
        if(!IsFirebaseReady || IsSignInOnProgress || User != null){
            return;
        }

        IsSignInOnProgress = true;
        startBtn.interactable = false;
        firebaseAuth.SignInWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWithOnMainThread( task => {
            Debug.Log($"Sign in status : {task.Status}");
            IsSignInOnProgress = false;
            startBtn.interactable = true;

            if(task.IsFaulted){
                Debug.LogError(task.Exception);
            }
            else if(task.IsCanceled){
                Debug.LogError("Sign in Canceled");
            }
            else{
                User = task.Result;
                Debug.Log(User.Email);
                Fade();
            }
        });
    }

    public void Fade(){
        StartCoroutine(Fade_());
    }

    IEnumerator Fade_(){
        float curTime = 0;

        fadeImage.gameObject.SetActive(true);
        while(curTime < totalFadeTime){
            curTime += Time.deltaTime;
            fadeImage.color = Color.Lerp(oriColor, destColor, curTime);
            yield return null;
        }
        ClickStartBtn();
    }

    private void ClickStartBtn(){
        SceneManager.LoadScene("LobbyScene");
    }

    public void ClickCloseBtn(){
        Application.Quit();
    }
}
