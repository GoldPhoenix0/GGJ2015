using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DynamicButtonOnClickListener : MonoBehaviour 
{
    private Button button;
    private Text title;
    private PopulateDetailedView detailedView = null;

    // Use this for initialization
    void Start () 
    {
        button = this.GetComponent<Button>();
        title = this.GetComponentInChildren<Text>();

        button.onClick.AddListener(() => { OnButtonClicked(); }); 
    }

    public void SetDetailedView(PopulateDetailedView detail)
    {
        detailedView = detail;
    }
    
    public void OnButtonClicked()
    {
        detailedView.InitialiseDetailedView(title.text);
    }
}
