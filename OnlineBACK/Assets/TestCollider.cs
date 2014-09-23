using UnityEngine;
using System.Collections;

public class TestCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision hit){
				Debug.Log ("Enter called.");
		}
}
