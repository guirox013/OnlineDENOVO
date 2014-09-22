using UnityEngine;
using System.Collections;

public class CharacterControllerMultiplayer : MonoBehaviour {
	public static GameObject character;
	public float speed;
	public static Transform mainCamera;
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX;
	public float sensitivityY;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -60F;
	public float maximumY = 60F;
	float rotationY = 0F;
	void Start () {

	}

	void Update ()
	{
		if (character != null) {
						DatabaseCharacter DBchar = (DatabaseCharacter)character.GetComponent (typeof(DatabaseCharacter));
						mainCamera.parent = DBchar.head.transform;
						mainCamera.position = new Vector3 (DBchar.head.position.x, DBchar.head.position.y + 0.3f, DBchar.head.position.z);
					
						if (axes == RotationAxes.MouseXAndY) {
								float rotationX = DBchar.transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * sensitivityX;
			
								rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
								rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
								DBchar.head.transform.localEulerAngles = new Vector3 (rotationY, 0, 0);
								DBchar.transform.localEulerAngles = new Vector3 (0, rotationX, 0);
						} else if (axes == RotationAxes.MouseX) {
								DBchar.transform.Rotate (0, Input.GetAxis ("Mouse X") * sensitivityX, 0);
						} else {
								rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
								rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
								DBchar.head.transform.localEulerAngles = new Vector3 (rotationY, DBchar.head.transform.localEulerAngles.y, 0);
						}
			DBchar.transform.Translate (Input.GetAxis ("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis ("Vertical") * speed * Time.deltaTime);
				}
	}



	// Update is called once per frame
//	void Update () {
//		if (character != null) {
//			DatabaseCharacter DBchar = (DatabaseCharacter)character.GetComponent(typeof(DatabaseCharacter));
//			mainCamera.parent = DBchar.head.transform;
//			mainCamera.position = new Vector3 (DBchar.head.position.x,DBchar.head.position.y+0.3f, DBchar.head.position.z);
//			yRotation += Input.GetAxis ("Mouse Y")* rotationSpeed; 
//			//xRotation -= Input.GetAxis ("Mouse Y") * lookSensitivity; 
//			//xRotation = Mathf.Clamp (xRotation, -lookUpAndDownLimit, lookUpAndDownLimit); 
//			//yRotation = Mathf.Clamp (yRotation, -lookLeftAndRightLimit, lookLeftAndRightLimit); 
//			//currentXRotation = Mathf.SmoothDamp (currentXRotation, xRotation, ref xRotationV, lookSmoothDamp); 
//			//currentYRotation = Mathf.SmoothDamp (currentYRotation, yRotation, ref yRotationV, lookSmoothDamp); 
//			//character.transform.rotation = Quaternion.Euler (currentXRotation, 0, 0); 
//			DBchar.transform.Translate (Input.GetAxis ("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis ("Vertical") * speed * Time.deltaTime);
//			DBchar.transform.Rotate(0 ,Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime,0);
//			if (yRotation < lookUpAndDownLimit && Input.GetAxis ("Mouse Y") > -yRotation)
//			DBchar.head.transform.Rotate(Input.GetAxis("Mouse Y") *rotationSpeed* Time.deltaTime ,0,0);
//			//if (Input.GetKey (KeyCode.W)) DBchar.setBool("isRunning", true);
//			//else setBool("isRunning", false);
//			print (Input.GetAxis ("Mouse Y"));
//		}
//	}
}
