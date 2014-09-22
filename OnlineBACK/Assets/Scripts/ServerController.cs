using UnityEngine;
using System.Collections;



public class ServerController : MonoBehaviour {
	public string ip;
	public int port;
	public GameObject[] respawns;
	public GameObject[] players;
	public Transform[] cameras;

	// Use this for initialization
	void Start () {
		ip = Network.player.ipAddress;
		port = 8080;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void StartServer (int maxPlayers, int port, bool userNat) {
		if (Network.peerType == NetworkPeerType.Disconnected) 
			Network.InitializeServer(maxPlayers, port, userNat);
	}

	void ConnectServer(string Ip, int Port){
		if (Network.peerType == NetworkPeerType.Disconnected){
			Network.Connect (Ip, Port);
		}
	}

	void OnGUI () {
		if (Network.peerType == NetworkPeerType.Disconnected){
			ip = GUI.TextField(new Rect(10, 10, 200, 20), ip, 25);
			if (GUI.Button(new Rect(10,40,100,30), "Start Server"))
			    StartServer (32, port, false);
			if (GUI.Button(new Rect(10,80,100,30), "Connect Server"))
				ConnectServer (ip, port);
		}
		if (Network.peerType == NetworkPeerType.Client || Network.peerType == NetworkPeerType.Server){
			GUI.Label(new Rect(10, 10, Screen.width, 20), Network.player.ipAddress + " is connected in " + ip);
			if (CharacterControllerMultiplayer.character == null)
			if (GUI.Button(new Rect(10,40,100,30), "Respawn")) respawnCharacter(players[0], cameras[0]);
		}
		if (GUI.Button(new Rect( 450,10,100,30),"Disconnect")){
			Network.Disconnect ();
			destroyCharacter(CharacterControllerMultiplayer.character);
		}
	}

	void respawnCharacter (GameObject personagem, Transform camera){
		CharacterControllerMultiplayer.character = (GameObject)Network.Instantiate (personagem, respawns [0].transform.position, personagem.transform.rotation, 0);
		CharacterControllerMultiplayer.mainCamera = (Transform)Network.Instantiate (camera, respawns [0].transform.position, personagem.transform.rotation, 0);
	}

	void destroyCharacter (GameObject personagem) {
		Network.Destroy (personagem);
	}
}
