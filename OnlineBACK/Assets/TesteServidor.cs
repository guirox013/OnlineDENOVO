using UnityEngine;
using System.Collections;

public class TesteServidor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Network.isServer){
			Vector3 moveDirection = new Vector3 (-1*Input.GetAxis("Vertical"), 0,Input.GetAxis("Horizontal"));
			float speed = 5;
			transform.Translate(speed * moveDirection * Time.deltaTime);
		}
	}
}
