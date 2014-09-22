using UnityEngine;
using System.Collections;

public class CharacterControllerMultiplayer : MonoBehaviour {
	public static GameObject character;
	public float speed;
	public float rotationSpeed; 
	public static Transform mainCamera;
	//protected Animator animator;
	//public float lookSensitivity = 5f;
	//public float lookSmoothDamp = 0.1f;
	public float lookUpAndDownLimit = 60.0f;
	//public float lookLeftAndRightLimit = 360.0f;
	//public float yRotation, xRotation, currentYRotation, currentXRotation, yRotationV, xRotationV;
	// Use this for initialization
	void Start () {

	}




	// Update is called once per frame
	void Update () {
		if (character != null) {
			DatabaseCharacter DBchar = (DatabaseCharacter)character.GetComponent(typeof(DatabaseCharacter));
			mainCamera.parent = DBchar.head.transform;
			mainCamera.position = new Vector3 (DBchar.head.position.x,DBchar.head.position.y+0.3f, DBchar.head.position.z);
			//yRotation += Input.GetAxis ("Mouse X") * lookSensitivity; 
			//xRotation -= Input.GetAxis ("Mouse Y") * lookSensitivity; 
			//xRotation = Mathf.Clamp (xRotation, -lookUpAndDownLimit, lookUpAndDownLimit); 
			//yRotation = Mathf.Clamp (yRotation, -lookLeftAndRightLimit, lookLeftAndRightLimit); 
			//currentXRotation = Mathf.SmoothDamp (currentXRotation, xRotation, ref xRotationV, lookSmoothDamp); 
			//currentYRotation = Mathf.SmoothDamp (currentYRotation, yRotation, ref yRotationV, lookSmoothDamp); 
			//character.transform.rotation = Quaternion.Euler (currentXRotation, 0, 0); 
			DBchar.transform.Translate (Input.GetAxis ("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis ("Vertical") * speed * Time.deltaTime);
			DBchar.transform.Rotate(0 ,Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime,0);
			if (Input.GetAxis ("Mouse Y") < lookUpAndDownLimit && Input.GetAxis ("Mouse Y") > -lookUpAndDownLimit)
			DBchar.head.transform.Rotate(Input.GetAxis("Mouse Y") *rotationSpeed* Time.deltaTime ,0,0);
			//if (Input.GetKey (KeyCode.W)) DBchar.setBool("isRunning", true);
			//else setBool("isRunning", false);
		}
	}
}
