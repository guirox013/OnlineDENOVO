using UnityEngine;
using System.Collections;

public class AnimatorScript : MonoBehaviour {
	Animator myAnimator;
	// Use this for initialization
	void Start () {
		myAnimator = GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (myAnimator) {
			if (Input.GetKey (KeyCode.W)) myAnimator.SetBool("isRunning", true);
			else myAnimator.SetBool ("isRunning", false);
		}

	}
}
