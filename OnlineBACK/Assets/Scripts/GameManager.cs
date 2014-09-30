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
		CharacterControllerMultiplayer.character = (GameObject)PhotonNetwork.Instantiate ("SteveLite 1", respawns [Random.Range(0,respawns.Length-1)].transform.position, players[limite].transform.rotation, 0);
		limite++;
	}
	
	void OnGUI()
	{
		if (PhotonNetwork.room == null) return; //Only display this GUI when inside a room
		
		if (GUI.Button(new Rect (Screen.width - 100,Screen.height - 30,100,30),"Leave Room"))
		{
			PhotonNetwork.LeaveRoom();
		}
	}
	
	void OnDisconnectedFromPhoton()
	{
		Debug.LogWarning("OnDisconnectedFromPhoton");
	}    
}
