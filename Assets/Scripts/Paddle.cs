using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

    private bool AutoPlay;
    private Ball ball;
    private GameObject bulletPrefab;
    private float firerate;
    private bool canFire;
    
    void Start(){
        AutoPlay = GameParameters.AutoPlay;
        ball = GameObject.FindObjectOfType<Ball>();
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet")
                            as GameObject;
        firerate = 0.5f;
        canFire = true;
    }
    
    IEnumerator ShootBullet() {
        GameObject bullet = Instantiate(bulletPrefab,
                                transform.position, Quaternion.identity)
                                    as GameObject;
        bullet.GetComponent<Rigidbody>().velocity = 
            new Vector3(0f, 30f, 0f);
            
        
        //bullet.GetComponent<ParticleSystem>().Play();
        
        
        canFire = false;
        yield return new WaitForSeconds(firerate);
        canFire = true;
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
        
        if(Input.GetMouseButtonDown(0) && ball.HasStarted && canFire){
            StartCoroutine(ShootBullet());
        }
		
	}

    
    void MoveAutomatically(){
    
        if(!ball.HasStarted){
            ball.StartGame();
        }
        Vector3 paddlePos = gameObject.transform.position;
        Vector3 ballPos = ball.transform.position;
        paddlePos.x = Mathf.Clamp(ballPos.x, 1.277f, 14.7f);
        gameObject.transform.position = paddlePos;
    }
	
    void MoveWithMouse() {
        float mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
        Vector3 position = gameObject.transform.position;
        position.x = Mathf.Clamp(mousePosInBlocks, 1.277f, 14.7f);
        gameObject.transform.position = position;
    }
}
