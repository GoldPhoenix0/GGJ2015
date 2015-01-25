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

    public List<string> SearchKeywords(string searchTerm)
    {
        List<string> keysFound = new List<string>();
        string temp = "";

        foreach(KeyValuePair<string, GeneralMetadata> gm in database)
        {
            temp = SearchSingleMetadata(searchTerm, gm.Value);

            if(temp != "")
            {
                keysFound.Add(temp);
                temp = "";
            }
        }

        return keysFound;
    }

    private string SearchSingleMetadata(string searchTerm, GeneralMetadata metadata)
    {
        bool found = false;

        foreach(string keyword in metadata.keywords)
        {
            if(StringCompareIgnoreCase(searchTerm, keyword))
            {
                found = true;
                break;
            }
        }

        if(found || StringCompareIgnoreCase(searchTerm, metadata.title))
        {
            return metadata.title;
        }

        return "";
    }

    private bool StringCompareIgnoreCase(string str1, string str2)
    {
        return (str1.ToLower() == str2.ToLower());
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
                // [2] = Icon
                // [3] = Keywords
                // [4] = Start Animation
                // [5] = End Animation
                // [6] = Animation Name
                // [7] = Camera Index
                string[] vals = line.Split('^');

                entry.title = vals[0];
                entry.body = vals[1];
                
                if(vals[2] != "")
                {
                    entry.icon = (Texture)Resources.Load(vals[2], typeof(Texture));
                }

                if(vals[3] != "")
                {
                    //split keywords into comma separated items
                    entry.keywords = vals[3].Split(',');
                }
                else
                {
                    entry.keywords = new string[0];
                }

                if(vals[4] == "" || !float.TryParse(vals[4], out f))
                {
                    entry.startTime = -1;
                }
                else
                {
                    entry.startTime = f;
                }
                if(vals[5] == "" || !float.TryParse(vals[5], out f))
                {
                    entry.endTime = -1;
                }
                else
                {
                    entry.endTime = f;
                }
                if(vals[6] != "")
                {
                    entry.animationName = vals[6];
                }
                if(vals[7] == "" || !int.TryParse(vals[7], out i))
                {
                    entry.cameraIndex = -1;
                }
                else
                {
                    entry.cameraIndex = i;
                }
				entry.title = entry.title.Trim();
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
