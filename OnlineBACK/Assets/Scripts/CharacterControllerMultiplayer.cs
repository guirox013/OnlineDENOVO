using UnityEngine;
using System.Collections;

public class CharacterControllerMultiplayer : MonoBehaviour {
	// Declarando variaveis
	public static GameObject character;
	public float speed;
	public Transform mainCamera;
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX;
	public float sensitivityY;
	public float maximumX = 360F;
	public float maximumY = 60F;
	public float minimumY;
	public float runMultiplier;
	public float jumpForce;
	float rotationY = 0F;

	Vector3 down;
	bool isGrounded;
	// Start que nao serve pra porra nenhuma por enquanto
	void Start () {
	}

	void FixedUpdate ()
	{
		//Checa se existe char
		if (character != null) {

			// Movimentaçao da camera
			DatabaseCharacter DBchar = (DatabaseCharacter)character.GetComponent (typeof(DatabaseCharacter));
			DBchar.isMine = true;
			mainCamera.parent = DBchar.head.transform;
			mainCamera.position = new Vector3 (DBchar.head.position.x, DBchar.head.position.y + 0.3f, DBchar.head.position.z);

			// Movimentaçao de X e Y simultaneos
			if (axes == RotationAxes.MouseXAndY) {
				float rotationX = DBchar.transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * sensitivityX;	
				rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				DBchar.head.transform.localEulerAngles = new Vector3 (rotationY, 0, 0);
				DBchar.transform.localEulerAngles = new Vector3 (0, rotationX, 0);

			// Movimentaçao de X
			} else if (axes == RotationAxes.MouseX) {
				DBchar.transform.Rotate (0, Input.GetAxis ("Mouse X") * sensitivityX, 0);

			// Movimentaçao de Y
			} else {
				rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				DBchar.head.transform.localEulerAngles = new Vector3 (rotationY, DBchar.head.transform.localEulerAngles.y, 0);
			}

			// Movimentaçao do personagem
			if (Input.GetKey (KeyCode.LeftShift))
				DBchar.transform.Translate (Input.GetAxis ("Horizontal") * (speed * runMultiplier) * Time.deltaTime, 0, Input.GetAxis ("Vertical") * (speed * runMultiplier) * Time.deltaTime);
			else DBchar.transform.Translate (Input.GetAxis ("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis ("Vertical") * speed * Time.deltaTime);

			// Pulo do Personagem
			down = DBchar.transform.TransformDirection(Vector3.down);
			isGrounded = DBchar.isGrounded(DBchar.transform.position, down, 2.4f);
			if (Input.GetKey (KeyCode.Space) && isGrounded){
				DBchar.rigidbody.AddForce (0,jumpForce,0);
			}

			if (Input.GetKey (KeyCode.Mouse0) && DBchar.isMine){
				DBchar.characterState = 1;
				DBchar.myAnimator.SetInteger ("animationState", DBchar.characterState);
			}
			else{ 
				DBchar.characterState = 0;
				DBchar.myAnimator.SetInteger ("animationState", DBchar.characterState);
			}

		}
	}
		



}
