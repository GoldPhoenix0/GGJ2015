using UnityEngine;
using System.Collections;

public class NetworkCore : MonoBehaviour {

	static int PORTNUM = 25038;

	[SerializeField]int mainSceneNum;

	public void StartServer() {
		Network.InitializeServer(10, PORTNUM, false);
	}

	public void JoinServer(string ip) {
		Network.Connect(ip, PORTNUM);
	}

	public void StartGame() {
		networkView.RPC("LoadLevel", RPCMode.All, mainSceneNum);
	}

	[RPC]
	void LoadLevel(int level) {
		Application.LoadLevel(level);
	}

	public bool IsConnected() {
		return Network.peerType != NetworkPeerType.Disconnected;
	}
	public int GetConnections() {
		return Network.connections.Length;
	}
}
