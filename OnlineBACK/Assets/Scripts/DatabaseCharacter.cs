using UnityEngine;
using System.Collections;

public class DatabaseCharacter : MonoBehaviour {
	public Transform head; 
	public bool isMine;
	public Animator myAnimator;
	public int characterState;
	// Use this for initialization
	void Start () {
		myAnimator = GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}

	public bool isGrounded (Vector3 position, Vector3 down, float distance){
		return (Physics.Raycast(position, down, distance));
	}

	public void isPunching (){
		}
}
