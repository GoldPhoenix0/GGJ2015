using UnityEngine;
using System.Collections;

public class NetworkCore : MonoBehaviour {

	static int PORTNUM = 25038;

	[SerializeField]string mainSceneName;

	public void StartServer() {
		Network.InitializeServer(10, PORTNUM, !Network.HavePublicAddress());
	}

	public void JoinServer(string ip) {
		Network.Connect(ip, PORTNUM);
	}

	public void StartGame() {
		Application.LoadLevel(mainSceneName);
	}

	public bool IsConnected() {
		return Network.peerType != NetworkPeerType.Disconnected;
	}
	public int GetConnections() {
		return Network.connections.Length;
	}
}
