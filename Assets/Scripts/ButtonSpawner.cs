using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonSpawner : MonoBehaviour {
    
    void SpawnEditorButton(int row, int column){
        GameObject buttonPrefab = 
            (GameObject)Resources.Load(
                "Prefabs/editor_brick_position_prefab",
                typeof(GameObject));
        
        GameObject instantiatedButton = (GameObject)Instantiate(buttonPrefab);
        instantiatedButton.GetComponent<EditorButton>().Row = row;
        instantiatedButton.GetComponent<EditorButton>().Column = column;
        
        instantiatedButton.transform.SetParent(gameObject.transform);
        
    }
    
	// Use this for initialization
	void Start () {
	    for(int row=36; row>=3; row--){
            for(int col=1; col<=16; col++){
                SpawnEditorButton(row, col);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

	}
}
