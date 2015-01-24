using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeGUIState : MonoBehaviour 
{
    public GameObject otherDialog; // assign in the editor
    public GameObject thisDialog;
    private bool isSetup = false;

    // Use this for initialization
    void Start () 
    {
        if(otherDialog != null && thisDialog != null)
        {
            isSetup = true;
        }
    }
    
    public void ToggleDialog()
    {
        if(isSetup)
        {
            thisDialog.SetActive(false);
            otherDialog.SetActive(true);
        }
    }
}
