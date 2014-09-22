using UnityEngine;
using System.Collections;

public class AnimatorMultiplayer : MonoBehaviour {

	public Animator myAnimator;
	// Use this for initialization
	void Start () {
		//myAnimator = (Animator) GetComponent (typeof (Animator));
		myAnimator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	if (myAnimator) {
		if (Input.GetKey (KeyCode.W))
			myAnimator.SetBool ("isRunning", true);
		else
			myAnimator.SetBool ("isRunning", false);
		}
		if (!myAnimator)
			print ("TA FALHANDO");
	}
}
