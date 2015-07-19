using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveStateController : MonoBehaviour {

    private Text buttonText;
    private Button saveButton;
    
    public bool IsSaving = false;
    private float saveTimeCounter = 0f;
    private float timeToSave = 0.5f;
    
	void Start () {
        GameObject saveButtonObj = GameObject.Find("Save Button");
        buttonText = saveButtonObj.GetComponent<Text>();
	}
    
    
    public void StartSaving(){
        IsSaving = true;
        saveTimeCounter = 0f;
        buttonText.text = "Saved!";
    }
    
    void StopSaving(){
        IsSaving = false;
        buttonText.text = "Save!";
    }
	
	// Update is called once per frame
	void Update () {
	    
        if(IsSaving){
            saveTimeCounter += Time.deltaTime;
            
            if(saveTimeCounter >= timeToSave){
                StopSaving();
            }
        }
        
	}
}
