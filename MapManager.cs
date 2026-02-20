using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class MapManager : NetworkBehaviour
{
    // Names of the scenes for your racing tracks
    public string[] trackScenes = { "CityTrack", "DesertTrack", "ForestTrack" };

    [Server]
    public void hostSelectMap(int index)
    {
        if (index >= 0 && index < trackScenes.Length)
        {
            // NetworkManager.ServerChangeScene moves all connected clients
            NetworkManager.singleton.ServerChangeScene(trackScenes[index]);
        }
    }
}
