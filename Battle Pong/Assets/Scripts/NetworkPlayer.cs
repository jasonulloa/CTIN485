﻿using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour {

	void Start () {
		if (photonView.isMine) {

		} else {

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