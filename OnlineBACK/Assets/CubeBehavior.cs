using UnityEngine;
using System.Collections;

public class TesteServidor : MonoBehaviour {
	public Vector3 lastPosition;

	// Use this for initialization
	void Start () {
		lastPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Network.isServer) {
			//Vector3 moveDirection = new Vector3 (-1 * Input.GetAxis ("Vertical"), 0, Input.GetAxis ("Horizontal") * -1);
			//float speed = 5;
			//transform.Translate (speed * moveDirection * Time.deltaTime);

			if(Vector3.Distance (transform.position, lastPosition) >=0.05){
				lastPosition = transform.position;
				networkView.RPC("atualizaCubo", RPCMode.Others, transform.position);
			}
		}
	}

	[RPC]
	void atualizaCubo (Vector3 newPos){
			transform.position=newPos;
	}
}