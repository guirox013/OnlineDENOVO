using UnityEngine;
using System.Collections;

public class Animatorcontroller : MonoBehaviour {
	public Vector3 lastPosition;
	public Animator myAnimator;

	// Use this for initialization
	void Start () {
		myAnimator = GetComponent <Animator> ();
		lastPosition = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (transform.position.x >= lastPosition.x + 0.1f || transform.position.x <= lastPosition.x - 0.1f || transform.position.z >= lastPosition.z + 0.1f || transform.position.z <= lastPosition.z - 0.1f){
			lastPosition = transform.position;
			myAnimator.SetBool ("isWalking", true);
		} else myAnimator.SetBool ("isWalking", false);
	}
}
