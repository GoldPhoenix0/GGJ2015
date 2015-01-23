using UnityEngine;
using System.Collections;

public class NetworkCore : MonoBehaviour {

	static int PORTNUM = 25038;

	public void StartServer() {

	}

	public void JoinServer(string ip) {

	}

	public void StartGame() {

	}

	public bool IsConnected() {
		return Network.peerType != NetworkPeerType.Disconnected;
	}
	public int GetConnections() {
		return Network.connections.Length;
	}
}
