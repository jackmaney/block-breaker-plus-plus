using UnityEngine;
using System.Collections;
using System;

public class Ball : MonoBehaviour {
	
	
    public float SpeedUpFactor;
    public float SpeedUpFrequency;
    public float InitialSpeed;
    public float MaxSpeed;
    public bool HasStarted;
    
    private Paddle GamePaddle;
	private Vector3 paddleToBallVector;
	
	private Rigidbody rb;
    
    private AudioSource beep;
    private AudioSource momp;
	
	void Reset() {
		HasStarted = false;
        
	}
    
    public void StartGame(){
        HasStarted = true;
        SetRandomVelocity();
    }
    
    
    void SetRandomVelocity(){
        float x = UnityEngine.Random.Range(-5f,5f);
        float y = UnityEngine.Random.Range(1f, 30f);
        
        
        Vector3 velocity = new Vector3(x, y, 0f);
        velocity.Normalize();
        velocity = velocity * InitialSpeed;
        velocity = Vector3.ClampMagnitude(velocity, MaxSpeed);
        rb.velocity = velocity;
        
    }
    
    void SpeedUp() {
        
        rb.velocity = 
            Vector3.ClampMagnitude(rb.velocity * SpeedUpFactor, MaxSpeed);
        
    }
	
	
	void Start () {
    
        GamePaddle = GameObject.FindObjectOfType<Paddle>();
        
        AudioSource[] tracks = GetComponents<AudioSource>();
        
        foreach(AudioSource track in tracks){
            if(track.clip.name == "beep"){
                beep = track;
            }
            else if(track.clip.name == "momp"){
                momp = track;
            }
            else{
                print("WTF: " + track.clip.name);
            }
        }
        
        // This is really just to make the compiler shut up...
        if(InitialSpeed <= 0){
            Debug.LogWarning("Negative initial speed of " + 
                InitialSpeed + 
                " found, setting to 10 instead"
            );
            InitialSpeed = 10f;
        }
		
		rb = gameObject.GetComponent<Rigidbody>();
		
		paddleToBallVector = 
			gameObject.transform.position - GamePaddle.transform.position;
		
		Reset();
        
        // Speed up every SpeedUpFrequency seconds
        InvokeRepeating("SpeedUp", 0, SpeedUpFrequency);
		
	}
	
	void Update() {
	     
        if(Input.GetMouseButtonDown(0) && !HasStarted){
            StartGame();
        }
		else if(!HasStarted){
			gameObject.transform.position = 
				GamePaddle.transform.position + paddleToBallVector;
		}
			
	}
    
    bool IsAudioPlaying(){
        return beep.isPlaying || momp.isPlaying;
    }
    
    void OnCollisionEnter(Collision col){
    
        bool tweakVelocity = false;
        
        
        string objName = col.gameObject.name;
        if(objName == "Paddle"){
            Paddle paddle = col.gameObject.GetComponent<Paddle>();
            Rigidbody paddleRB = paddle.GetComponent<Rigidbody>();
            rb.AddForce(paddleRB.velocity, ForceMode.VelocityChange);
        }
        else if(objName.EndsWith("Wall")){
            tweakVelocity = true;
            beep.Play();
        }
        else if(objName.StartsWith("brick")){
            tweakVelocity = true;
            AudioSource.PlayClipAtPoint(momp.clip, 
                col.gameObject.transform.position, 0.25f);
        }
        
        if(tweakVelocity){
            Vector3 tweak = new Vector3(
                UnityEngine.Random.Range(-0.2f, 0.2f),
                UnityEngine.Random.Range(-0.2f, 0.2f),
                0);
            
            // If we're near vertical, max out
            // the x component of the tweak.
            if(Math.Abs(rb.velocity.x) < 0.2f){
                // Shuffle x to the left or right depending
                // on whether we're on the right or left
                // side of the screen (respectively).
                if(gameObject.transform.position.x <= 8){
                    tweak.x = 0.2f;
                }
                else{
                    tweak.x = -0.2f;
                }
            }
            
            // Likewise with y
            if(Math.Abs(rb.velocity.y) < 0.2f){
                if(gameObject.transform.position.y <= 6){
                    tweak.y = 0.2f;
                }
                else{
                    tweak.y = -0.2f;
                }
            }
   
            rb.velocity = Vector3.ClampMagnitude(
                    rb.velocity + tweak, MaxSpeed);
        }
    }
	
}
