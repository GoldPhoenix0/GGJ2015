using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartNetworkGame : MonoBehaviour 
{
    [SerializeField] private Button startButton = null; // assign in the editor
    private NetworkCore networkCore;

    // Use this for initialization
    void Start () 
    {
        startButton = GetComponent<Button>();
        networkCore = GetComponent<NetworkCore>();
    }
    
    // Update is called once per frame
    void Update () 
    {
        startButton.interactable = (networkCore.GetConnections() > 1);
    }
}
