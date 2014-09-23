using UnityEngine;
using System.Collections;

public class DatabaseCharacter : MonoBehaviour {
	public Transform head; 
	public Animator myAnimator;

	// Use this for initialization
	void Start () {
		myAnimator = GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
