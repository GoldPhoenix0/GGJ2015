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

        /*metadata.startTime;
        metadata.endTime;
        metadata.animationName;
        metadata.cameraIndex;
        */
    }
}
