using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	
	public static bool musicOn = false;

	void Awake() {
		
		if(musicOn){
			Object.Destroy(gameObject);
		}
		else{
			musicOn = true;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}
}
