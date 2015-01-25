using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopulateDetailedView : MonoBehaviour 
{
    public GameObject videoButton;
    [SerializeField]private DatabaseManager dbManager;
    [SerializeField]private RectTransform contentView;
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
            icon.gameObject.SetActive(true);
        }
        else
        {
            icon.gameObject.SetActive(false);
            icon.texture = null;
        }

        if(item.startTime >= 0 && item.endTime >= 0 &&
           item.animationName != "" && item.cameraIndex >= 0)
        {
            // Send the button the metadata file
            videoButton.GetComponent<PlayFootageTrigger>().SetMetadata(item);
            videoButton.SetActive(true);
        }
        else
        {
            videoButton.SetActive(false);
        }

        this.gameObject.SetActive(true);
    }
}
