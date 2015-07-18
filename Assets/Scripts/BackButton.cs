using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackButton : MonoBehaviour {

	void Start () {
	    GetComponent<Button>().onClick.AddListener(GoBackToStart);
	}
    
    void GoBackToStart(){
        GameParameters.Reset();
        
        Application.LoadLevel("Start");
    }

}
