using UnityEngine;
using System.Collections;

public class ControladorServer : MonoBehaviour{

	
	void Awake(){
		
		//Conecta ao main server. Por enquanto nao precisa de Ip e Port, usa cloud
		if (!PhotonNetwork.connected)
			PhotonNetwork.ConnectUsingSettings("v0.01"); // versao do jogo
		
		//Carrega nome
		PhotonNetwork.playerName = PlayerPrefs.GetString("playerName", "Guest" + Random.Range(1, 9999));

		
	}
	
	private string roomName = "myRoom";
	private Vector2 scrollPos = Vector2.zero;
	
	void OnGUI(){	

		//Espera por conexao
		if (!PhotonNetwork.connected){
			ShowConnectingGUI();
			return;   
		}
		
		//Enquanto nao estamos em uma sala retorna nulo
		if (PhotonNetwork.room != null)
			return; 
		
		
		GUILayout.BeginArea(new Rect((Screen.width - 400) / 2, (Screen.height - 300) / 2, 400, 300));
		
		GUILayout.Label("Main Menu");
		
		//Player name
		GUILayout.BeginHorizontal();
		GUILayout.Label("Player name:", GUILayout.Width(150));
		PhotonNetwork.playerName = GUILayout.TextField(PhotonNetwork.playerName);
		if (GUI.changed)//Save name
			PlayerPrefs.SetString("playerName", PhotonNetwork.playerName);
		GUILayout.EndHorizontal();
		
		GUILayout.Space(15);
		
		
		//Join room por nome
		GUILayout.BeginHorizontal();
		GUILayout.Label("JOIN ROOM:", GUILayout.Width(150));
		roomName = GUILayout.TextField(roomName);
		if (GUILayout.Button("GO")){
			PhotonNetwork.JoinRoom(roomName);
		}
		GUILayout.EndHorizontal();

		
		//Cria sala e falha se o nome ja existir
		GUILayout.BeginHorizontal();
		GUILayout.Label("CREATE ROOM:", GUILayout.Width(150));
		roomName = GUILayout.TextField(roomName);
		if (GUILayout.Button("GO")){
			PhotonNetwork.CreateRoom(roomName, new RoomOptions() { maxPlayers = 10 }, TypedLobby.Default);
		}
		GUILayout.EndHorizontal();
		
		//Join random room
		GUILayout.BeginHorizontal();
		GUILayout.Label("JOIN RANDOM ROOM:", GUILayout.Width(150));
		if (PhotonNetwork.GetRoomList().Length == 0){
			GUILayout.Label("..no games available...");
		}
		else{
			if (GUILayout.Button("GO")){
				PhotonNetwork.JoinRandomRoom();
			}
		}
		GUILayout.EndHorizontal();
		
		GUILayout.Space(30);
		GUILayout.Label("ROOM LISTING:");
		if (PhotonNetwork.GetRoomList().Length == 0){
			GUILayout.Label("..no games available..");
		}
		else{


			//Lista de salas
			scrollPos = GUILayout.BeginScrollView(scrollPos);
			foreach (RoomInfo game in PhotonNetwork.GetRoomList()){
				GUILayout.BeginHorizontal();
				GUILayout.Label(game.name + " " + game.playerCount + "/" + game.maxPlayers);
				if (GUILayout.Button("JOIN"))
				{
					PhotonNetwork.JoinRoom(game.name);

				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndScrollView();
		}
		
		GUILayout.EndArea();
	}

	
	// Mostra o GUI ao conectar
	void ShowConnectingGUI(){
		GUILayout.BeginArea(new Rect((Screen.width - 400) / 2, (Screen.height - 300) / 2, 400, 300));
		
		GUILayout.Label("Connecting to Photon server.");
		GUILayout.Label("Teste Alpha Early Beta Access v0.01");
		
		GUILayout.EndArea();
	}

	// Instacia um char


	// Destroi um char
	void destroyCharacter (GameObject personagem) {
		PhotonNetwork.Destroy (personagem);
	}


}
