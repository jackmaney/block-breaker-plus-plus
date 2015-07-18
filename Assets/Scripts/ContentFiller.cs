using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ContentFiller : MonoBehaviour {
    
    CustomLevel[] customLevels;
    public GameObject LevelPrefab;
    public RectTransform ContentPanel;
    
    CustomLevel chosenLevel;
    
	// Use this for initialization
	void Start () {
	    customLevels = CustomLevel.AllCustomLevels();
        
        
        PopulateList();
        
	}
    
    
    void PopulateList(){
        for(int i=0; i<customLevels.Length; i++){
            
            GameObject loadLevelButton = 
                    Instantiate(LevelPrefab) as GameObject;
            
            Text nameText = loadLevelButton.GetComponentInChildren<Text>();
            
            
            nameText.text = customLevels[i].Name;
            
            loadLevelButton.GetComponent<LoadedGamePrefab>().
                    CustomLevel = customLevels[i];
            
            loadLevelButton.GetComponent<RectTransform>().
                SetParent(ContentPanel, false);
            
            //RectTransform rt = loadLevelButton.GetComponent<RectTransform>();
            //RectTransform cprt = ContentPanel.GetComponent<RectTransform>();
            
            //rt.sizeDelta = new Vector2(cprt.sizeDelta.x, cprt.sizeDelta.y);
            
            //rt.sizeDelta = new Vector2(cprt.sizeDelta.x, 100);
        }
    }
    

}
