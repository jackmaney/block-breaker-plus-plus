using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Brick : MonoBehaviour {
	
	public int MaxHits;
	private int numHitsTaken;
    public Renderer Rend;
    
    private static readonly Dictionary<int, Color> colorMap = 
        GameParameters.ColorMap;
	
	public int NumHitsTaken{
		get{
		    return numHitsTaken;
		}
	}
	
    
    public void SetColor() {
        if(colorMap.ContainsKey(MaxHits - numHitsTaken)){
            Rend.sharedMaterial.SetColor("_EmissionColor", 
                colorMap[MaxHits - numHitsTaken]);
        }
        else{
            Debug.LogWarning(
                String.Format(
                    "Attempted to SetColor with MaxHits = {0} " +
                    "and numHitsTaken = {1}", MaxHits, numHitsTaken
                )
            );
        }
        
    }
    
	void Awake() {
    
		numHitsTaken = 0;
        Rend = gameObject.GetComponent<Renderer>();
        Rend.material.shader = Shader.Find("Standard");
        SetColor();
        
	}

	void OnCollisionEnter(Collision other){
		numHitsTaken++;
		
		if(numHitsTaken >= MaxHits){
            
            UnityEngine.Object.Destroy(gameObject);
			
		}
		else{
            SetColor();
		}
	}
    
}
