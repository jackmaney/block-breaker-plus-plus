using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class EditorController : MonoBehaviour {
    
    private int hitsChosen;
    private static Dictionary<int, Color> colorMap = GameParameters.ColorMap;
    private Dictionary<int, int[]> brickRows = new Dictionary<int, int[]>();
    private SceneManager sceneManager;
    private Text hitsIndicator;
    private Image bgPreview;
    private Image editorBGPreview;
    private Sprite[] backgrounds;
    
    private int currentBGIndex = 0;
    private string levelName;
    
    private Text levelNameText;
    
	// Use this for initialization
	void Start () {
    
        #if UNITY_WEBPLAYER
        Destroy(GameObject.Find("SavePanel"));
        Destroy(GameObject.Find("NamePanel"));
        #endif
    
        hitsIndicator = GameObject.Find("HitsIndicator")
                            .GetComponent<Text>();
        sceneManager = GameObject.FindObjectOfType<SceneManager>();
        bgPreview = GameObject.Find("BackgroundMiniPreview")
                        .GetComponent<Image>();
        backgrounds = GameParameters.Backgrounds;
        
        editorBGPreview = GameObject.Find("EditorPanel")
                            .GetComponent<Image>();
        
        levelNameText = GameObject.Find("LevelNameText")
                            .GetComponent<Text>();
        
        UpdateBackgrounds();                     
	}
    
    public void GetName(){
        levelName = levelNameText.text;
    }
    
    public void NextBackground(){
        currentBGIndex++;
        UpdateBackgrounds();
    }
    
    public void PrevBackground(){
        currentBGIndex--;
        UpdateBackgrounds();
    }
    
    public void UpdateBackgrounds(){
        while(currentBGIndex < 0){
            currentBGIndex += backgrounds.Length;
        }
        currentBGIndex = currentBGIndex % backgrounds.Length;
        bgPreview.sprite = backgrounds[currentBGIndex];
        editorBGPreview.sprite = backgrounds[currentBGIndex];
    }
    
    
    public void ChangeHits(int value){
        hitsChosen += value;
        hitsChosen = (int)Mathf.Clamp(hitsChosen, 0, 3);
        
        UpdateIndicator();
    }
    
    
    public void UpdateIndicator(){
        hitsIndicator.text = hitsChosen.ToString();
        
        hitsIndicator.color = colorMap[hitsChosen];
    }
    
    public void ChangeButton(EditorButton button){
        
        if(!brickRows.ContainsKey(button.Row)){
            brickRows[button.Row] = new int[16];
        }
        
        brickRows[button.Row][button.Column - 1] = hitsChosen;
        ColorBlock colorBlock = button.GetComponent<Button>().colors;
        colorBlock.highlightedColor = colorMap[hitsChosen];
        colorBlock.normalColor = colorMap[hitsChosen];
        colorBlock.pressedColor = colorMap[hitsChosen];
        button.GetComponent<Button>().colors = colorBlock;
        
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.anyKeyDown){
            int value = hitsChosen;
            if(Input.GetKeyDown(KeyCode.Alpha0)){
                value = 0;
            }
            else if(Input.GetKeyDown(KeyCode.Alpha1)){
                value = 1;
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2)){
                value = 2;
            }
            else if(Input.GetKeyDown(KeyCode.Alpha3)){
                value = 3;
            }
            
            if(value != hitsChosen){
                hitsChosen = value;
                UpdateIndicator();
            }
        }
	}
    

    
    private CustomLevel GetLevel(){
        string fileName;
        
        #if UNITY_WEBPLAYER
        fileName = "doesnt_matter_we_cant_save_this.bbpp-level";
        #else
        fileName = levelName.Replace(" ", "_") + ".bbpp-level";
        #endif
        return new CustomLevel(levelName, brickRows, fileName, currentBGIndex);
    }
    
    public void SaveCustomLevel(){
        GetLevel().Save();
    }
    
    public void PlayCustomLevel(){
        GameParameters.BackgroundIndex = currentBGIndex;
        
        CustomLevel customLevel = GetLevel();
        customLevel.Save();
        GameParameters.CurrentLevel = customLevel;
        
        sceneManager.LoadScene("Level");
    }
}
