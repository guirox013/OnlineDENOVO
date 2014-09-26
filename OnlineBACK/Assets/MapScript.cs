using UnityEngine;
using System.Collections;

public class MapScript : MonoBehaviour {
	public float gravity;

	// Use this for initialization
	void Start () {
		Physics.gravity = new Vector3(0, -gravity, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
