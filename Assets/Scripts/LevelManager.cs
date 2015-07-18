using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class LevelManager : MonoBehaviour {
    
    private Vector3 defaultBrickSize = GameParameters.DefaultBrickSize;
    private Sprite brickSprite = GameParameters.DefaultBrickSprite;
    
    private List<Brick> brickList = new List<Brick>();
    private bool bricksLoaded = false;
    
    private GameObject background;
    
    private SpriteRenderer bgRenderer;
    
    private SceneManager sceneManager;
    
    
	// Use this for initialization
	void Start(){
    
        sceneManager = GameObject.Find("SceneManager").
                            GetComponent<SceneManager>();
        
        background = GameObject.Find("PlaySpace/Background");
        

        bgRenderer = background.GetComponent<SpriteRenderer>();
        
        bricksLoaded = false;
        brickList.Clear();
        
        if(GameParameters.GoToLoadingScreen){
            GoToLoadingScreen();
        }
        else if(GameParameters.CurrentLevel == null){
            LoadFirstBuiltInLevel();
        }
        else{
            LoadLevel();
        }
    }

    public void LoadLevel(){
        LoadLevel(GameParameters.CurrentLevel);
    }

    public void LoadLevel(Level level){
        GameParameters.CurrentLevel = level;
        
        
        bgRenderer.sprite = level.Background();
        foreach(KeyValuePair<int, int[]> kv in level.BrickRows){
            SpawnBrickRow(kv.Key, kv.Value);
        }
        
        bricksLoaded = true;
    }
    
    void SpawnBrickRow(int yIndex, int[] brickHits){
        
        for(int i=0; i<brickHits.Length; i++){
            if(brickHits[i] > 0){
                Vector3 position = new Vector3(
                    i * defaultBrickSize.x,
                    yIndex * defaultBrickSize.y,
                    -1
                    );
                
                // Offsetting, since the transform position of
                // our blocks is at the center.
                
                position += new Vector3(
                    0.5f * defaultBrickSize.x,
                    0.5f * defaultBrickSize.y,
                    0.5f * defaultBrickSize.z
                    );
                
                GameObject brickPrefab = 
                    (GameObject)Resources.Load(
                        "Prefabs/brick_prefab", typeof(GameObject));
                Brick brick = brickPrefab.GetComponent<Brick>();
                brick.MaxHits = brickHits[i];
                Instantiate(brick, position, Quaternion.identity);
                brick.SetColor();
                brick.GetComponent<Renderer>().sharedMaterial.SetTexture(
                    "_EmissionMap", brickSprite.texture
                    );
                
                
                brickList.Add(brick);
                
            }
        }
        
    }
    
    int NumberOfBricksLeft() {
        return GameObject.FindObjectsOfType<Brick>().Length;
    }
	
	// Update is called once per frame
	void Update () {
        if(bricksLoaded && NumberOfBricksLeft() == 0){
            LoadNextLevel();
        }
	}
    
    public void LoadLevelByName(string levelName){
        if(levelName == GameParameters.CurrentLevel.Name){
            LoadLevel();
        }
        else{
            Level level = null;
            
            foreach(Level bl in GameParameters.BuiltInLevels){
                if(name == bl.Name){
                    level = bl;
                    break;
                }
            }
            
            if(level != null){
                LoadLevel(level);
            }
        }
    }
    
    public void GoToLoadingScreen(){
        if(GameParameters.GoToLoadingScreen){
            sceneManager.LoadScene("Loading");
        }
    }
    
    
    public void LoadNextLevel(){
        
        if(GameParameters.GoToLoadingScreen){
            GoToLoadingScreen();
            return;
        }
        
        Level currentLevel = GameParameters.CurrentLevel;
        List<Level> builtInLevels = GameParameters.BuiltInLevels;
        int index = builtInLevels.IndexOf(currentLevel);
        
        if(currentLevel == null){
            GameParameters.CurrentLevel = builtInLevels[0];
            GameParameters.GoToLoadingScreen = true;
            GoToLoadingScreen();
        }
        if(index >= 0 && index < builtInLevels.Count - 1){
            Level nextLevel = builtInLevels[index + 1];
            GameParameters.CurrentLevel = nextLevel;
            GameParameters.GoToLoadingScreen = true;
            GoToLoadingScreen();
        }
        else{
            Application.LoadLevel("Win");
        }
        
    }
    
    public void LoadFirstBuiltInLevel(){
        GameParameters.CurrentLevel = GameParameters.BuiltInLevels[0];
        LoadLevel();
    }
}
