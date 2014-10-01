using UnityEngine;
using System.Collections;

public class DefaultAI : MonoBehaviour {
	RaycastHit hit;
	public float range;
	public float rangeAttack;
	public float speedAI;
	private float auxTime;
	private float pointX;
	private float pointZ;
	public Transform target;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		auxTime += Time.deltaTime;
		// Movimentaçao
		if (auxTime > 8) {
			CreatePoint();
			transform.GetComponent<NavMeshAgent>().destination = new Vector3(pointX, 0, pointZ);
			auxTime = 0;
		}
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
		foreach (Collider collider in hitColliders){
			if (collider.tag == "Player"){
				target = collider.transform;
				print (Vector3.Distance (target.position, transform.position));
				if (Vector3.Distance (target.position, transform.position) < rangeAttack){
					renderer.material.color = Color.red;
					transform.GetComponent<NavMeshAgent>().destination = target.position;
				} else renderer.material.color = Color.yellow;
	
			}
			else {
				renderer.material.color = Color.green;

			}
		}
	}

	void CreatePoint (){
		pointX = transform.position.x + Random.Range (-3,3);
		pointZ =transform.position.z + Random.Range (-3,3);
	}
}
