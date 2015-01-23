using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour 
{
    public string[] database;

    [SerializeField] private InputField inputField = null; // assign in the editor
    [SerializeField] private Text outputField = null; // assign in the editor

    // Use this for initialization
    void Start () 
    {
        inputField = GetComponent<InputField>();
        //inputField.onEndEdit.AddListener(() => { DisplayResult(); });
    }
    
    public void DisplayResult()
    {
        string searchString = inputField.text;

        List<string> results = SearchResults(database, searchString);

        string display = "";

        foreach(string result in results)
        {
            display += result + "\n";
        }

        outputField.text = display;
    }

    private List<string> SearchResults(string[] db, string searchString)
    {
        List<string> results = new List<string>();
        
        foreach(string item in db)
        {
            //if(item.Contains(searchString))
            // Search the string ignoring case
            if(item.IndexOf(searchString, System.StringComparison.OrdinalIgnoreCase) >= 0)
            {
                results.Add(item);
            }
        }

        return results;
    }
}
