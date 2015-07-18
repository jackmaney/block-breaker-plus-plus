using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

    private bool AutoPlay;
    private Ball ball;
    
    void Start(){
        AutoPlay = GameParameters.AutoPlay;
        ball = GameObject.FindObjectOfType<Ball>();
    }

	void Update() {
        
        if(Input.GetKeyDown(KeyCode.Equals)){
            AutoPlay = !AutoPlay;
            GameParameters.AutoPlay = AutoPlay;
        }
		
        if(AutoPlay){
            MoveAutomatically();
        }
        else{
		    MoveWithMouse();
        }
		
	}
    
    void MoveAutomatically(){
    
        if(!ball.HasStarted){
            ball.StartGame();
        }
        Vector3 paddlePos = gameObject.transform.position;
        Vector3 ballPos = ball.transform.position;
        paddlePos.x = Mathf.Clamp(ballPos.x, 1f, 15f);
        gameObject.transform.position = paddlePos;
    }
	
    void MoveWithMouse() {
        float mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
        Vector3 position = gameObject.transform.position;
        position.x = Mathf.Clamp(mousePosInBlocks, 1f, 15f);
        gameObject.transform.position = position;
    }
}
