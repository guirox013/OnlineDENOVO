using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		if (GUI.Button(new Rect(260,120,100,30),"Enter Game")){
			Application.LoadLevel("Main");
			Debug.Log ("Loading Complete");
		}
	}
}
