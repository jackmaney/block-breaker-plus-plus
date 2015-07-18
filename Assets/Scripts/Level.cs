using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;


[Serializable]
public class Level{
    
    public string Name;
    public Dictionary<int, int[]> BrickRows;
    public int BackgroundIndex;
    
    public Level(){}
    
    public Level(string name, 
                    Dictionary<int, int[]> brickRows, 
                    int backgroundIndex){
        this.Name = name;
        this.BrickRows = brickRows;
        this.BackgroundIndex = backgroundIndex;
        
        Validate();
    }
    
    protected void Validate(){
        foreach(KeyValuePair<int, int[]> kv in BrickRows){
            if(kv.Key < 3 || kv.Key > 36){
                throw new ArgumentException("Brick row number " + 
                    kv.Key + " needs to be between 3 and 36!");
            }
            
            if(kv.Value.Length != 16){
                throw new ArgumentException(
                    "Brick rows need to be 16 entries, not " + 
                        kv.Value.Length);
            }
            
        }
    }
    
    
    
    public Sprite Background(){
        Sprite[] backgrounds = GameParameters.Backgrounds;
        return backgrounds[BackgroundIndex];
    }
    
    public void Play(){
        GameParameters.CurrentLevel = this;
        GameParameters.GoToLoadingScreen = true;
        GameObject.FindObjectOfType<SceneManager>().LoadScene("Level");
    }
    
}
