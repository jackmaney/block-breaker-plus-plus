using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameParameters : MonoBehaviour {

    public static string Version = "1.1.0";
    
    public static bool TakeScreenShotOfLevel = false;

    public static bool AutoPlay = false;

    public static bool GoToLoadingScreen = false;
    public static bool ShowFPS = false;

    public static Level CurrentLevel;
    public static List<Level> BuiltInLevels = new List<Level>();
    
    public static Dictionary<int, Color> ColorMap = 
        new Dictionary<int, Color>();
        
    public static Vector3 DefaultBrickSize;
    public static Sprite DefaultBrickSprite;
    
    public static Sprite[] Backgrounds;
    
    public static int BackgroundIndex;
    
    public static Dictionary<string, string> CustomLevels =
        new Dictionary<string, string>();
    
    private static bool computationsDone = false;
    
    private static GameParameters instance;
    
    public static string CustomLevelPath;
    
    void Awake() {
        if(instance == null){
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if(instance != this){
            Destroy(gameObject);
        }
    }
    
    static void InitializeBuiltInLevels(){
        
        BuiltInLevels.Clear();

        Dictionary<int, int[]> brickRows1 = 
            new Dictionary<int, int[]>();
        brickRows1[20] = new int[]{1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0};
        brickRows1[21] = new int[]{0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1};
        
        BuiltInLevels.Add(new Level("Level01", brickRows1, 0));
        
        Dictionary<int, int[]> brickRows2 = 
            new Dictionary<int, int[]>();
        brickRows2[25] = new int[]{1,1,1,1,1,1,2,2,2,2,1,1,1,1,1,1};
        brickRows2[26] = new int[]{2,2,2,2,3,3,3,3,3,3,3,3,2,2,2,2};
        
        BuiltInLevels.Add(new Level("Level02", brickRows2, 1));
    }
    
    
	// Use this for initialization
	void Start () {
	    
        if(!computationsDone){
            Reset();
        }
        
	}
	
	public static void Reset(){
        CurrentLevel = null;
        GoToLoadingScreen = false;
    
        CustomLevelPath = 
            Application.persistentDataPath + "/CustomLevels";
        
        computationsDone = true;
        
        ColorMap.Clear();
        
        ColorMap[0] = Color.white;
        ColorMap[1] = Color.green;
        ColorMap[2] = Color.blue;
        ColorMap[3] = Color.red;
        
        DefaultBrickSize.x = 1.0f;
        DefaultBrickSize.y = 0.3203125f; // 41 / 128
        DefaultBrickSize.z = 1.0f;
        
        Sprite[] brickSprites = 
            Resources.LoadAll<Sprite>("Sprites/full_half_brick_sprite_sheet");
        DefaultBrickSprite = brickSprites[0];
        
        InitializeBuiltInLevels();
        
        Backgrounds = Resources.LoadAll<Sprite>("Backgrounds");
        BackgroundIndex = 0;
    }
}
