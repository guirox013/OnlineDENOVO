using UnityEngine;
using System.Collections;



public class ServerController : MonoBehaviour {

	//Variaveis
	public string ip;
	public int port;
	public GameObject[] respawns;
	public GameObject[] players;
	public int limite;
	public string serverName;

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings ("Teste servidor");
		ip = Network.player.ipAddress;
		serverName = "Nome da sala";
		port = 8080;
		limite = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPhotomRandomJoinFailed() {
		Debug.Log ("Failed to connect to a random server");
	}

	void OnJoinedLobby()
	{
				PhotonNetwork.JoinRandomRoom ();
	}

	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("Can't join random room!");
		PhotonNetwork.CreateRoom(null);
	}

	// Inicializador do server
	void StartServer (int maxPlayers, int port, bool userNat) {
		PhotonNetwork.CreateRoom (serverName);
		PhotonNetwork.JoinRoom (serverName);
		//if (Network.peerType == NetworkPeerType.Disconnected) 
		//	Network.InitializeServer(maxPlayers, port, userNat);
	}


	// Conecta a algum server
	void ConnectServer(string Ip, int Port){
		PhotonNetwork.JoinRandomRoom ();
				//if (Network.peerType == NetworkPeerType.Disconnected){
				//	PhotonNetwork.JoinRandomRoom();
				//PhotonNetwork.Connect (Ip, Port);
		//}
	}


	// Basic GUI atual
	void OnGUI () {

		// Gera GUI para o desconectado
		if (!PhotonNetwork.inRoom){
			GUILayout.Label	(PhotonNetwork.connectionStateDetailed.ToString());
			serverName = GUI.TextField(new Rect(10, 10, 200, 20), serverName, 25);

			if (GUI.Button(new Rect(10,40,100,30), "Start Server"))
			    StartServer (32, port, false);
			if (GUI.Button(new Rect(10,80,100,30), "Connect Server"))
				ConnectServer (ip, port);

		// Gera GUI para conectado
		}else{
			if (PhotonNetwork.inRoom)
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
			CharacterControllerMultiplayer.character = (GameObject)PhotonNetwork.Instantiate ("SteveLite 1", respawns [limite].transform.position, personagem.transform.rotation, 0);
			limite++;
		}
	}

	// Destroi o char ao desconectar
	void destroyCharacter (GameObject personagem) {
		PhotonNetwork.Destroy (personagem);
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
