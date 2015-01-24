using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour 
{
    public string[] database;

    [SerializeField] private InputField inputField = null; // assign in the editor
    [SerializeField] private ScrollableList scrollList = null; // assign in the editor
    [SerializeField] private DatabaseManager dbManager = null; // assign in the editor

    // Use this for initialization
    void Start () 
    {
        inputField = GetComponent<InputField>();
        database = dbManager.GetTitles().ToArray();
    }
    
    public void DisplayResult()
    {
        string searchString = inputField.text;

        List<string> results = SearchResults(database, searchString);

        // Display the results as separate buttons on a list
        scrollList.PopulateList(results);
    }

    private List<string> SearchResults(string[] db, string searchString)
    {
        List<string> results = new List<string>();
        
        foreach(string item in db)
        {
            // Search the string ignoring case
            if(item.IndexOf(searchString, System.StringComparison.OrdinalIgnoreCase) >= 0)
            {
                results.Add(item);
            }
        }

        return results;
    }
}
