using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	void OnCollisionEnter(Collision col){
        int layer = LayerMask.NameToLayer("Bullet");
        if(col.gameObject.layer == layer){
            Destroy(col.gameObject);
        }
    }
}
