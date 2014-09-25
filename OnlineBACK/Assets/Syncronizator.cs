using UnityEngine;
using System.Collections;

public class Syncronizator : MonoBehaviour {
	Vector3 trueLoc;
	Quaternion trueRot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!networkView.isMine){
			transform.position = Vector3.Lerp(transform.position, trueLoc, Time.deltaTime * 5);
			transform.rotation = Quaternion.Lerp(transform.rotation, trueRot, Time.deltaTime * 5);
		}
	
	}

	public void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		//we are reicieving data
		if (stream.isReading)
		{
			//receive the next data from the stream and set it to the truLoc varible
			if(!networkView.isMine){//do we own this photonView?????
				this.trueLoc = transform.position; //the stream send data types of "object" we must typecast the data into a Vector3 format
				this.trueRot = transform.rotation;
			}
		}
		//we need to send our data
		else
		{
			//send our posistion in the data stream
			if(networkView.isMine){
				Vector3 pos = transform.position;
				Quaternion rPos = transform.rotation;
				stream.Serialize(ref pos);
				stream.Serialize (ref rPos);
			}
		}
	}
}
