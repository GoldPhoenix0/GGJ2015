using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

// Created with assistance from Stuart Spence (https://www.youtube.com/watch?v=TRLsmuYMs8Q)

public class ScrollableList : MonoBehaviour
{
    public GameObject itemPrefab;
    public int itemCount = 10, columnCount = 1;
    [SerializeField]private PopulateDetailedView detailedView;
    [SerializeField]private DatabaseManager dbManager;
    private RectTransform rowRectTransform, containerRectTransform;

    void Start()
    {
        rowRectTransform = itemPrefab.GetComponent<RectTransform>();
        containerRectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void PopulateList(List<string> results)
    {
        //Remove all previous items from the list before calculating
        DeleteList();

        itemCount = results.Count;

        // Break if there is no items to count
        if(itemCount <= 0)
        {
            return;
        }

        //calculate the width and height of each child item.
        float width = containerRectTransform.rect.width / columnCount;
        float ratio = width / rowRectTransform.rect.width;
        float height = rowRectTransform.rect.height * ratio;
        int rowCount = itemCount / columnCount;
        if (itemCount % rowCount > 0)
            rowCount++;
        
        //adjust the height of the container so that it will just barely fit all its children
        float scrollHeight = height * rowCount;
        containerRectTransform.offsetMin = new Vector2(containerRectTransform.offsetMin.x, -scrollHeight / 2);
        containerRectTransform.offsetMax = new Vector2(containerRectTransform.offsetMax.x, scrollHeight / 2);
        
        int j = 0;
        for (int i = 0; i < itemCount; i++)
        {
			Debug.Log(results[i]);
            //this is used instead of a double for loop because itemCount may not fit perfectly into the rows/columns
            if (i % columnCount == 0)
            {
                j++;
            }

            //create a new item, name it, and set the parent
            GameObject newItem = Instantiate(itemPrefab) as GameObject;
            newItem.name = results[i] + "Button";
            newItem.transform.SetParent(gameObject.transform, false);

            // Set the title of the button as the result
            //newItem.GetComponent<Button>().guiText.text = results[i];

            GeneralMetadata item = dbManager.GetMetadata(results[i]);
            //newItem.GetComponentInChildren<Text>().text = results[i];

            // Adds the listener and sets the detailed view in the button
            newItem.GetComponent<DynamicButtonOnClickListener>().SetDetailedView(detailedView, item);

            //move and size the new item
            RectTransform rectTransform = newItem.GetComponent<RectTransform>();
            
            float x = -containerRectTransform.rect.width / 2 + width * (i % columnCount);
            float y = containerRectTransform.rect.height / 2 - height * j;
            rectTransform.offsetMin = new Vector2(x, y);
            
            x = rectTransform.offsetMin.x + width;
            y = rectTransform.offsetMin.y + height;
            rectTransform.offsetMax = new Vector2(x, y);
        }
    }

    public void DeleteList()
    {
        var children = new List<GameObject>();
        foreach (Transform child in transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
    }
}
