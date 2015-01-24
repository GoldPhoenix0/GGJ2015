using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopulateDetailedView : MonoBehaviour 
{
    [SerializeField]private DatabaseManager dbManager;
    [SerializeField]private Text title;
    [SerializeField]private Text body;
    [SerializeField]private RawImage icon;

    public void InitialiseDetailedView(string key)
    {
        GeneralMetadata item = dbManager.GetMetadata(key);

        title.text = item.title;
        body.text = item.body;

        if(item.icon != null)
        {
            icon.texture = item.icon;
        }

        this.gameObject.SetActive(true);
    }
}
