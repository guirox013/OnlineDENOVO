using UnityEngine;
using System.Collections;

public class Animatorcontroller : MonoBehaviour {
	public Vector3 lastPosition;
	public Animator myAnimator;
	public static bool isGrounded;

	// Use this for initialization
	void Start () {
		myAnimator = GetComponent <Animator> ();
		lastPosition = transform.position;
	}

	void Update (){

	}


	// Update is called once per frame
	void FixedUpdate () {
		if (transform.position.x >= lastPosition.x + 0.14f || transform.position.x <= lastPosition.x - 0.14f || transform.position.z >= lastPosition.z + 0.14f || transform.position.z <= lastPosition.z - 0.14f){
			if (transform.position.x >= lastPosition.x + (0.14f *2) || transform.position.x <= lastPosition.x - (0.14f *2) || transform.position.z >= lastPosition.z + (0.14f *2)|| transform.position.z <= lastPosition.z - (0.14f *2))
				myAnimator.SetBool ("isRunning", true);
			myAnimator.SetBool ("isWalking", true);
		} else {
			myAnimator.SetBool ("isWalking", false);
			myAnimator.SetBool ("isRunning", false);
		}
		lastPosition = transform.position;
		//if (Input.GetKeyDown (KeyCode.Mouse0 && isMine))
			//myAnimator.SetBool("isPunching", true);
		//else myAnimator.SetBool("isPunching", false);


	}
}
