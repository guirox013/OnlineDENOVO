	using UnityEngine;
using System.Collections;

public class CharacterControllerMultiplayer : MonoBehaviour {
	// Variaveis para o personagem
	public static GameObject character;
	public Transform mainCamera;

	// Variaveis de rotacao e ajuste da camera
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX;
	public float sensitivityY;
	public float maximumX = 360F;
	public float maximumY = 60F;
	public float minimumY;

	// Variaveis de movimentacao
	public float speed;
	public float runMultiplier;
	public float jumpForce;
	float rotationY = 0F;
	public float maxVelocityChange = 10.0f;

	// Variaveis de apoio
	public float auxTime= 0f;
	Vector3 down;
	Vector3 front;
	private bool isOnInventory = false;

	// Start que nao serve pra porra nenhuma por enquanto
	void Start () {
		Screen.showCursor = false;
	}

	void FixedUpdate ()
	{
		// Checa se esta no inventario para desativar a movimentaçao
		if (Input.GetButtonDown ("Inventory"))
			isOnInventory = !isOnInventory;
		
		//Checa se existe char
		if (character != null && !isOnInventory) {
			// Criaçao do personagem e ajuste da camera
			DatabaseCharacter DBchar = (DatabaseCharacter)character.GetComponent (typeof(DatabaseCharacter));
			DBchar.isMine = true;
			mainCamera.parent = DBchar.head.transform;
			mainCamera.position = new Vector3 (DBchar.head.position.x, DBchar.head.position.y + 0.3f, DBchar.head.position.z);
			//print (DBchar.health);

			// Atualiza variaveis de apoio
			down = DBchar.transform.TransformDirection(Vector3.down);
			auxTime += Time.deltaTime;

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


			// Antiga Movimentaçao do personagem
			//if (Input.GetKey (KeyCode.LeftShift))
				//DBchar.transform.Translate (Input.GetAxis ("Horizontal") * (speed * runMultiplier) * Time.deltaTime, 0, Input.GetAxis ("Vertical") * (speed * runMultiplier) * Time.deltaTime);
			//DBchar.rigidbody.AddForce (Input.GetAxis ("Horizontal")*1000*Time.deltaTime, 0,Input.GetAxis ("Vertical")*1000*Time.deltaTime);
			//else DBchar.transform.Translate (Input.GetAxis ("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis ("Vertical") * speed * Time.deltaTime);

			// Nova Movimentaçao do personagem
			Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal")* Time.deltaTime, 0, Input.GetAxis("Vertical")*Time.deltaTime);
			targetVelocity = DBchar.transform.TransformDirection(targetVelocity);
			if (Input.GetKey (KeyCode.LeftShift) && DBchar.stamina > 0){
				targetVelocity *= speed*runMultiplier;
				DBchar.stamina -= 0.2f; 
			}
			else targetVelocity *= speed;
			Vector3 velocity = DBchar.rigidbody.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			DBchar.rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

			// Pulo do Personagem

			if (Input.GetKeyDown (KeyCode.Space) && DBchar.isGrounded(DBchar.transform.position, down, 2.4f) && DBchar.stamina > 9){
				DBchar.rigidbody.AddForce (0,jumpForce,0);
				DBchar.stamina = DBchar.stamina - 10;
			}

			// Stamina recovery
			if (DBchar.stamina <= 100)
				DBchar.stamina += 0.02f;

			// Chama o soco
			if (Input.GetKeyDown (KeyCode.Mouse0) && DBchar.isMine && auxTime>0.8 ){
				auxTime =0;
				front = DBchar.transform.TransformDirection(Vector3.forward);
				DBchar.characterState = 1;
				DBchar.myAnimator.SetInteger ("animationState", DBchar.characterState);
				DBchar.havePlayer (DBchar.transform.position, front, 10f);
			}
			else{ 
				DBchar.characterState = 0;
				DBchar.myAnimator.SetInteger ("animationState", DBchar.characterState);
			}


		}
	}
}
