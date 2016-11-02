using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	const string VERSION = "v0.0.1";
	public string roomName = "BattlePong";
	public string playerPrefabName = "Paddle";
	public Transform playerOneSpawnPoint;
	public Transform playerTwoSpawnPoint;

	void Start () {
		PhotonNetwork.ConnectUsingSettings (VERSION);	
	}

	void OnJoinedLobby() {
		RoomOptions roomOptions = new RoomOptions () { isVisible = false, maxPlayers = 2 };
		PhotonNetwork.JoinOrCreateRoom (roomName, roomOptions, TypedLobby.Default);
	}

	void OnJoinedRoom() {
		PhotonNetwork.Instantiate (playerPrefabName, playerOneSpawnPoint.position, playerOneSpawnPoint.rotation, 0);
		PhotonNetwork.Instantiate (playerPrefabName, playerTwoSpawnPoint.position, playerTwoSpawnPoint.rotation, 0);
	}
}