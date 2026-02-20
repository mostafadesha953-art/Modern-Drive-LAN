using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class GameLauncherUI : MonoBehaviour
{
    public NetworkManager manager;
    public string networkAddress = "localhost";

    public void StartSinglePlayer()
    {
        // Start as Host but with AI spawned
        manager.StartHost();
        Debug.Log("Single Player Mode Started");
    }

    public void StartLanHost()
    {
        manager.StartHost();
        Debug.Log("LAN Server Started");
    }

    public void JoinLanGame()
    {
        manager.networkAddress = networkAddress;
        manager.StartClient();
        Debug.Log("Joining LAN Game...");
    }

    public void SetIPAddress(string ip)
    {
        networkAddress = ip;
    }
}
