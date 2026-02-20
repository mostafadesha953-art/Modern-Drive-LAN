using UnityEngine;
using Mirror;

public class MapSelectionUI : MonoBehaviour
{
    public MapManager mapManager;

    public void OnTrackButtonClicked(int trackIndex)
    {
        // Only the host (server) should be able to trigger map change
        if (NetworkServer.active)
        {
            mapManager.hostSelectMap(trackIndex);
        }
        else
        {
            Debug.Log("Only the Host can select the map!");
        }
    }
}
