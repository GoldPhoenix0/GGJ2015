using UnityEngine;
using System.Collections;

public class PlayFootageTrigger : MonoBehaviour
{
    [SerializeField] private SynchronisedPlayback sync = null; // assign in the editor
    private GeneralMetadata metadata;

    public void SetMetadata (GeneralMetadata gm) 
    {
        metadata = gm;
    }
    
    public void UpdateVideo()
    {
        if(metadata == null)
        {
            return;
        }

        sync.SelectFootage(metadata);

        // Close the dialog when the button is pressed
        //this.gameObject.SetActive(false);
    }
}
