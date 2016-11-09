using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	const string VERSION = "1";
	public string status;
	public string roomName = "BattlePong";
	public string playerPrefabName = "Paddle";
	public Transform playerOneSpawnPoint;
	public Transform playerTwoSpawnPoint;
	RoomOptions roomOptions;
	int counter = 1;

	void Start() {
		PhotonNetwork.autoJoinLobby = true;
		PhotonNetwork.PhotonServerSettings.ServerPort = 5055;
		roomOptions = new RoomOptions (); //{ isVisible = false, maxPlayers = 2 };
		Connect ();
		InvokeRepeating ("UpdateStatus", 2, 1);
	}

	void Connect() {
		PhotonNetwork.ConnectUsingSettings (VERSION);
	}

	void UpdateStatus(){
		status = PhotonNetwork.connectionStateDetailed.ToString ();

		if (status == "JoinedLobby") {
			PhotonNetwork.JoinOrCreateRoom (roomName, roomOptions, TypedLobby.Default);
		}

		if (status == "Joined" && counter == 1) {
			OnJoinedRoom ();
			counter = 2;
		}
	}

	void OnJoinedRoom() {
		PhotonNetwork.Instantiate (playerPrefabName, playerOneSpawnPoint.position, playerOneSpawnPoint.rotation, 0);
		PhotonNetwork.Instantiate (playerPrefabName, playerTwoSpawnPoint.position, playerTwoSpawnPoint.rotation, 0);
	}
}