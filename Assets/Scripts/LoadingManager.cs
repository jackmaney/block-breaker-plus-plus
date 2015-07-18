using UnityEngine;
using System.Collections;

public class LoadingManager : MonoBehaviour {

	void Start () {
        GameParameters.GoToLoadingScreen = false;
	    GameObject.Find("SceneManager").
                GetComponent<SceneManager>().LoadScene("Level");
	}
}
