using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour {
	public Transform playerOneSpawnPoint;
	public Transform playerTwoSpawnPoint;

	void Start () {
		if (photonView.isMine) {
			GameObject pad1 = (GameObject)Instantiate (Resources.Load ("Paddle"));
			pad1.transform.position = playerOneSpawnPoint.position;
			pad1.transform.rotation = playerOneSpawnPoint.rotation;
		} else {
			GameObject pad2 = (GameObject)Instantiate (Resources.Load ("Paddle"));
			pad2.transform.position = playerTwoSpawnPoint.position;
			pad2.transform.rotation = playerTwoSpawnPoint.rotation;
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
		} else {
			transform.position = (Vector3)stream.ReceiveNext ();
			transform.rotation = (Quaternion)stream.ReceiveNext ();
		}
	}
}