using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopulateDetailedView : MonoBehaviour 
{
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
        }
      
        //adjust the height of the container so that it will just barely fit all its children
        /*float scrollHeight = body.rectTransform.rect.height;
        contentView.offsetMin = new Vector2(contentView.offsetMin.x, -scrollHeight / 2);
        contentView.offsetMax = new Vector2(contentView.offsetMax.x, scrollHeight / 2);
        */

        this.gameObject.SetActive(true);
    }
}
