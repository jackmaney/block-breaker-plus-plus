using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadedGamePrefab : MonoBehaviour {
    
    public CustomLevel CustomLevel;
	// Use this for initialization
	void Start () {
	    GetComponent<Button>().
                onClick.AddListener(Clicked);
	}
	
	void Clicked(){
        CustomLevel.Play();
    }
}
