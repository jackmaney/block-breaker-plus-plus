using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VersionText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    GetComponent<Text>().text = 
            "Version " + GameParameters.Version;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
