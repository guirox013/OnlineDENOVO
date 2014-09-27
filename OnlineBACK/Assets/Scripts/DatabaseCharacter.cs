using UnityEngine;
using System.Collections;

public class DatabaseCharacter : MonoBehaviour {
	public Transform head; 
	public bool isMine;
	public Animator myAnimator;
	public int characterState;
	public int health;
	// Use this for initialization
	void Start () {
		myAnimator = GetComponent <Animator> ();	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	}

	// Retorna se o personagem esta a uma distancia do chao
	public bool isGrounded (Vector3 position, Vector3 down, float distance){
		return (Physics.Raycast(position, down, distance));
	}

	// Chama a funçao RPC que aplica o dano
	void takeDamage (int damageAmount){
		transform.GetComponent<PhotonView>().RPC ("changeHealth", PhotonTargets.AllBuffered, damageAmount);
	}

	// Chama a funçao RPC que aplica o knockback
	void applyKnockBack (Vector3 knock){
		transform.GetComponent<PhotonView>().RPC ("knockBack", PhotonTargets.AllBuffered, knock);
	}

	// Checa se existe um player na frente e aplica um dano
	public bool havePlayer (Vector3 position, Vector3 front, float distance){
		RaycastHit hit;
		Physics.Raycast (position, front, out hit, distance);	
		if (hit.collider.transform.tag == "Player"){
			Debug.Log ("Tentando hitar " + hit.collider.transform.name);
			hit.collider.transform.SendMessage ("takeDamage", 10);
			hit.collider.transform.SendMessage ("applyKnockBack", hit.collider.transform.TransformDirection(Vector3.back*1000));
			return true;
		}
		else{ 
			if (hit.transform.tag == null)
				print ("Nao acertou nada");
			print ("Nao hitou um player");
			return false;
		}
	}

	// RPCS

	[RPC]
	void changeHealth(int damage){
		health -= damage;
	}

	[RPC]
	void knockBack(Vector3 force){
		rigidbody.AddForce (force);
	}

	
}
