using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DatabaseManager : MonoBehaviour 
{
    public TextAsset csvFile;
    private Dictionary<string, GeneralMetadata> database;

    // Use this for initialization
    void Awake () 
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
        string[] words = csv.text.Split('|');

        foreach(string line in words)
        {
            Debug.Log (line);

            // Ignore comments starting with #
            if(!line[0].Equals('#'))
            {
                int i = 0;
                float f = 0;
                GeneralMetadata entry = new GeneralMetadata();
                // [0] = Heading
                // [1] = Body
                // [2] = Icon (Not IMPLEMENTED)
                string[] vals = line.Split('^');

                entry.title = vals[0];
                entry.body = vals[1];
                
                if(vals[2] != "")
                {
                    entry.icon = (Texture)Resources.Load(vals[2], typeof(Texture));
                }
                if(vals[3] == "" || !float.TryParse(vals[3], out f))
                {
                    entry.startTime = -1;
                }
                else
                {
                    entry.startTime = f;
                }
                if(vals[4] == "" || !float.TryParse(vals[4], out f))
                {
                    entry.endTime = -1;
                }
                else
                {
                    entry.endTime = f;
                }
                if(vals[5] != "")
                {
                    entry.animationName = vals[5];
                }
                if(vals[6] == "" || int.TryParse(vals[6], out i))
                {
                    entry.cameraIndex = -1;
                }
                else
                {
                    entry.cameraIndex = i;
                }
                
                // add item to the database
                database.Add(entry.title, entry);
            }
        }
    }
    
    /*private void ReadFile(TextAsset csv)
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
                string[] vals = line.Split('^');
                
                entry.title = vals[0];
                entry.body = vals[1];

                //if(vals.Length > 2)
                //{
                    //entry.icon = Resources.Load(vals[2], typeof(Texture));
                //}
                
                // add item to the database
                database.Add(entry.title, entry);
            }
        }

        // close the reader after use
        reader.Close();
    }*/


}
