using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayIPAddress : MonoBehaviour
{
    [SerializeField] private Text displayText = null; // assign in the editor
    public bool showJoined = false;

    // Use this for initialization
    IEnumerator Start () 
    {
		yield return new WaitForSeconds(0.5f);
        displayText = GetComponent<Text>();

        string ipInfo = "Your IP Address is \n";

		ipInfo += Network.player.ipAddress;
        
        int lastComma = ipInfo.LastIndexOf(',');
        
        if(lastComma >= 0)
        {
            ipInfo = ipInfo.Substring(0, lastComma);
        }
		Debug.Log(Network.peerType);
		Debug.Log(ipInfo);
        displayText.text = ipInfo;
    }

    void Update()
    {
        if(!showJoined)
        {
            return;
        }
        
        string ipInfo = (Network.connections.Length + 1) + " Players Joined";
        
        displayText.text = ipInfo;
    }

}
