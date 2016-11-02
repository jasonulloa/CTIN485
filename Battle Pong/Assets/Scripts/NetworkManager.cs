using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	const string VERSION = "v0.0.1";
	public string roomName = "BP";
	public string playerPrefabName = "Player One";
	public Transform playerOneSpawnPoint;

	void Start () {
		PhotonNetwork.ConnectUsingSettings (VERSION);	
	}

	void OnJoinedLobby() {
		RoomOptions roomOptions = new RoomOptions () { isVisible = false, maxPlayers = 2 };
		PhotonNetwork.JoinOrCreateRoom (roomName, roomOptions, TypedLobby.Default);
	}

	void OnJoinedRoom() {
		PhotonNetwork.Instantiate (playerPrefabName, playerOneSpawnPoint.position, playerOneSpawnPoint.rotation, 0);
	}
}