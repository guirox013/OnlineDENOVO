using UnityEngine;
using System.Collections;

public class DatabaseCharacter : MonoBehaviour {
	public Transform head; 
	public Vector3 lastPosition;
	// Use this for initialization
	void Start () {
		lastPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
