using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float Speed = 30f;
    private ParticleSystem ps;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        ps = GetComponentInChildren<ParticleSystem>();
        ps.Play();
        ps.enableEmission = true;
        rb = GetComponent<Rigidbody>();
	}
	

	void FixedUpdate () {
	    rb.velocity = new Vector3(0f, Speed, 0f);
	}
}
