using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class EditorButton : MonoBehaviour {
    
    public int Row;
    public int Column;
    
    private Button button;
    private EditorController editorController;
    
    
    void ChangeCallback(){
        editorController.ChangeButton(this);
    }
    
	// Use this for initialization
	void Start () {
	    button = GetComponent<Button>();
        editorController = 
            GameObject.Find("Editor Controller").
                GetComponent<EditorController>();
        button.onClick.AddListener(ChangeCallback);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
