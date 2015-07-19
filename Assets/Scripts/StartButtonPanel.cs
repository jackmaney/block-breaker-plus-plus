using UnityEngine;
using System.Collections;

public class StartButtonPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        #if UNITY_WEBPLAYER
        Destroy(GameObject.Find("Load Button"));
        #endif
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
