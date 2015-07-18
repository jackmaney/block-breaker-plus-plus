using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CursorHandler : MonoBehaviour {

    private List<string> noCursorLevels = new List<string>();
    
    bool CursorShouldBeVisible(){
        return !noCursorLevels.Contains(Application.loadedLevelName);
    }

	void Start () {
        
        noCursorLevels.Add("Level");
        noCursorLevels.Add("Loading");
        
        if(!CursorShouldBeVisible() && Cursor.visible){
            Cursor.visible = false;
        }
        else if(CursorShouldBeVisible() && !Cursor.visible){
            Cursor.visible = true;
        }
	}
}
