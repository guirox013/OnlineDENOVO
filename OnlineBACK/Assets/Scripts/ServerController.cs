using UnityEngine;
using System.Collections;



public class ServerController : MonoBehaviour {

	//Variaveis
	public string ip;
	public int port;
	public GameObject[] respawns;
	public GameObject[] players;
	public int limite;

	// Use this for initialization
	void Start () {
		ip = Network.player.ipAddress;
		port = 8080;
		limite = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Inicializador do server
	void StartServer (int maxPlayers, int port, bool userNat) {
		if (Network.peerType == NetworkPeerType.Disconnected) 
			Network.InitializeServer(maxPlayers, port, userNat);
	}


	// Conecta a algum server
	void ConnectServer(string Ip, int Port){
		if (Network.peerType == NetworkPeerType.Disconnected){
			Network.Connect (Ip, Port);
		}
	}


	// Basic GUI atual
	void OnGUI () {

		// Gera GUI para o desconectado
		if (Network.peerType == NetworkPeerType.Disconnected){
			GUI.Label(new Rect(10,120,Screen.width,30),"Connection status: Disconnected");
			ip = GUI.TextField(new Rect(10, 10, 200, 20), ip, 25);
			if (GUI.Button(new Rect(10,40,100,30), "Start Server"))
			    StartServer (32, port, false);
			if (GUI.Button(new Rect(10,80,100,30), "Connect Server"))
				ConnectServer (ip, port);

		// Gera GUI para conectado
		}else{
			if (Network.peerType == NetworkPeerType.Connecting)
				GUI.Label(new Rect(10,120,Screen.width,30),"Connection status: Connecting");

			// GUI para Client
			else if (Network.peerType == NetworkPeerType.Client){
				GUI.Label(new Rect(10,30,Screen.width,30),"Connection status: Client");
				GUI.Label (new Rect(10,10,Screen.width,30), "Ping: "+Network.GetAveragePing (Network.connections[0]));

			// GUI para Servidor
			} else if (Network.peerType == NetworkPeerType.Server){
				GUI.Label(new Rect(10,30,Screen.width,30),"Connection status: Server");
				GUI.Label (new Rect(10,10,Screen.width,30), "Connections: " + Network.connections.Length);
				if(Network.connections.Length >= 1)
					GUI.Label (new Rect(110,10,Screen.width,30), "Ping to first player: "+Network.GetAveragePing (Network.connections[0]));
			}

			// Call no metodo que spawna char caso ele ainda nao esteja spawnado
			if (CharacterControllerMultiplayer.character == null)
			if (GUI.Button(new Rect(10,50,100,30), "Respawn")) respawnCharacter(players[limite]);
		}

		if (GUI.Button(new Rect( 450,10,100,30),"Disconnect")){
			Network.Disconnect ();
			destroyCharacter(CharacterControllerMultiplayer.character);
			Application.LoadLevel("Menu");
			Debug.Log ("Loading Complete");
		}
	}

	// Instancia um char no servidor
	void respawnCharacter (GameObject personagem){
		if (CharacterControllerMultiplayer.character == null) {
			CharacterControllerMultiplayer.character = (GameObject)Network.Instantiate (personagem, respawns [limite].transform.position, personagem.transform.rotation, 0);	
			limite++;
		}
	}

	// Destroi o char ao desconectar
	void destroyCharacter (GameObject personagem) {
		Network.Destroy (personagem);
	}

	// Os proximos metodos sao apenas para tratamento de erros/acompanhamento
	void OnConnectedToServer() {
		Debug.Log("This CLIENT has connected to a server");	
	}
	
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		Debug.Log("This SERVER OR CLIENT has disconnected from a server");
	}
	
	void OnFailedToConnect(NetworkConnectionError error){
		Debug.Log("Could not connect to server: "+ error);
	}

	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("Player connected from: " + player.ipAddress +":" + player.port);
	}
	
	void OnServerInitialized() {
		Debug.Log("Server initialized and ready");
	}
	
	void OnPlayerDisconnected(NetworkPlayer player) {
		Debug.Log("Player disconnected from: " + player.ipAddress+":" + player.port);
	}

	void OnNetworkInstantiate (NetworkMessageInfo info) {
		Debug.Log("New object instantiated by " + info.sender);
	}

	//Adicionar RPCS Codes (estudar)
}
