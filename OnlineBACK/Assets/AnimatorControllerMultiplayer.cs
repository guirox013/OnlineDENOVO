using UnityEngine;
using System.Collections;

public class AnimatorControllerMultiplayer : MonoBehaviour {
	public Animator myAnimator;
	// Use this for initialization
	void Start () {
		myAnimator = GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (myAnimator) {
			if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S))
				myAnimator.SetBool("isWalking", true);
			else myAnimator.SetBool ("isWalking", false);
			if ((Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S)) && Input.GetKey (KeyCode.LeftShift))
				myAnimator.SetBool ("isRunning", true);
			else myAnimator.SetBool ("isRunning", false);
		}
		
		
	}
}
