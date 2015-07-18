using System.Collections;
using UnityEngine;

public class LoseCollider : MonoBehaviour {
	
	private SceneManager sceneManager;
	
	void Start() {
		sceneManager = GameObject.FindObjectOfType<SceneManager>();
	}


	void OnTriggerEnter(Collider col){
    
        if(col.gameObject.name == "Ball"){
		    sceneManager.LoadScene("Lose");
        }
        else{
            Debug.LogWarning(
                col.gameObject.name + " is colliding with the LoseCollider!");
        }
	}
	
}
