using UnityEngine;
using System.Collections;

public class GameManager : Photon.MonoBehaviour {
	public GameObject[] respawns;
	public GameObject[] players;
	public int limite;
	
	void OnJoinedRoom()
	{
		StartGame();
	}

	
	void StartGame()
	{   
		CharacterControllerMultiplayer.character = (GameObject)PhotonNetwork.Instantiate ("SteveLite 1", respawns [limite].transform.position, players[limite].transform.rotation, 0);
		limite++;
	}
	
	void OnGUI()
	{
		if (PhotonNetwork.room == null) return; //Only display this GUI when inside a room
		
		if (GUILayout.Button("Leave Room"))
		{
			PhotonNetwork.LeaveRoom();
		}
	}
	
	void OnDisconnectedFromPhoton()
	{
		Debug.LogWarning("OnDisconnectedFromPhoton");
	}    
}
