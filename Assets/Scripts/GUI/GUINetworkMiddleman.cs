using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUINetworkMiddleman : MonoBehaviour 
{
    public float checkNetworkConnectionTime = 0.5f;
    public int maxIterations = 30;
    [SerializeField] private InputField ipInputField = null; // assign in the editor
    [SerializeField] private NetworkCore networkCore;
    private ChangeGUIState guiState;

    // Use this for initialization
    void Start () 
    {
        guiState = GetComponent<ChangeGUIState>();
    }
    
    public void SubmitIPAddress()
    {
        string ipAddress = ipInputField.text;

        // Error Checking can go here

        Debug.Log("IP Address = " + ipAddress);

        networkCore.JoinServer(ipAddress);

        StartCoroutine(ConnectionEstablished());
    }

    private IEnumerator ConnectionEstablished()
    {
        for(int i = 0; i < maxIterations; i++)
        {
            Debug.Log("Connection attempt " + i);

            if(networkCore.GetConnections() > 0)
            {
                guiState.ToggleDialog();
                break;
            }
            yield return new WaitForSeconds(checkNetworkConnectionTime);
        }
    }
}
