using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUINetworkMiddleman : MonoBehaviour 
{
    [SerializeField] private InputField ipInputField = null; // assign in the editor
    private NetworkCore networkCore;

    // Use this for initialization
    void Start () 
    {
        networkCore = GetComponent<NetworkCore>();
    }
    
    public void SubmitIPAddress()
    {
        string ipAddress = ipInputField.text;

        // Error Checking can go here

        Debug.Log("IP Address = " + ipAddress);

        networkCore.JoinServer(ipAddress);
    }
}
