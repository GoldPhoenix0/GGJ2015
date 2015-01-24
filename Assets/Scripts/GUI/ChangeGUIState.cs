using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeGUIState : MonoBehaviour 
{
    public GameObject otherDialog; // assign in the editor
    public GameObject thisDialog;
    public bool hideSelf = false;
    private bool isSetup = false;

    // Use this for initialization
    void Start () 
    {
        // Allows just itself to be hidden to be hidden
        if((otherDialog != null || hideSelf) && thisDialog != null)
        {
            isSetup = true;
        }
    }
    
    public void ToggleDialog()
    {
        if(isSetup)
        {
            thisDialog.SetActive(false);

            if(!hideSelf)
            {
                otherDialog.SetActive(true);
            }
        }
    }
}
