using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DatabaseManager : MonoBehaviour 
{
    public TextAsset csvFile;
    private Dictionary<string, GeneralMetadata> database;

    // Use this for initialization
    void Start () 
    {
        database = new Dictionary<string, GeneralMetadata>();

        if(csvFile == null)
        {
            Debug.LogError("csv Database not found");
        }

        ReadFile(csvFile);
    }

    // Return just the keys in the game
    public List<string> GetTitles()
    {
        return new List<string>(database.Keys);
    }

    public GeneralMetadata GetMetadata(string key)
    {
        return database[key];
    }

    public string GetBody(string key)
    {
        return database[key].body;
    }

    public Texture GetIcon(string key)
    {
        return database[key].icon;
    }
    
    private void ReadFile(TextAsset csv)
    {
        string line = "";
        StringReader reader = new StringReader(csv.text);

        while((line = reader.ReadLine()) != null)
        {
            // Ignore comments starting with #
            if(!line[0].Equals('#'))
            {
                GeneralMetadata entry = new GeneralMetadata();
                // [0] = Heading
                // [1] = Body
                // [2] = Icon (Not IMPLEMENTED)
                string[] vals = line.Split(',');
                
                entry.title = vals[0];
                entry.body = vals[1];

                /*if(vals.Length > 2)
                {
                    //entry.icon = Resources.Load(vals[2], typeof(Texture));
                }*/
                
                // add item to the database
                database.Add(entry.title, entry);
            }
        }

        // close the reader after use
        reader.Close();
    }


}
