using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
    
    
    public void LoadScene(string sceneName){
        
        #if UNITY_WEBPLAYER
        if(
            sceneName == "LoadCustomLevel"
        ){
            Debug.Log(
                "The web editor is unable to load or save custom levels.");
            return;
        }
        #endif
        Application.LoadLevel(sceneName);
    }
    
    public void QuitRequest() {
        Debug.Log("Quit requested");
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
