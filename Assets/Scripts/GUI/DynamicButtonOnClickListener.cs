using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DynamicButtonOnClickListener : MonoBehaviour 
{
    private Button button;
    private PopulateDetailedView detailedView = null;
    private GeneralMetadata metadata;
    [SerializeField]private Text title;
    [SerializeField]private Text body;

    // Use this for initialization
    void Start () 
    {
        button = this.GetComponent<Button>();

        button.onClick.AddListener(() => { OnButtonClicked(); }); 
    }

    public void SetDetailedView(PopulateDetailedView detail, GeneralMetadata meta)
    {
        detailedView = detail;
        metadata = meta;

        title.text = metadata.title;
        body.text = metadata.body;
    }
    
    public void OnButtonClicked()
    {
        detailedView.InitialiseDetailedView(metadata.title);
    }
}
