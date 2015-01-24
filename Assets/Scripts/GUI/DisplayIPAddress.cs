using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayIPAddress : MonoBehaviour
{
    [SerializeField] private Text displayText = null; // assign in the editor
    public bool showJoined = false;

    // Use this for initialization
    void Start () 
    {
        displayText = GetComponent<Text>();

        string ipInfo = "Your IP Address is \n";
        
        foreach(System.Net.IPAddress ip in System.Net.Dns.Resolve(System.Net.Dns.GetHostName()).AddressList)
        {
            ipInfo += ip.ToString() + ", ";
        }
        
        int lastComma = ipInfo.LastIndexOf(',');
        
        if(lastComma >= 0)
        {
            ipInfo = ipInfo.Substring(0, lastComma);
        }
        
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
