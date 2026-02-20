using UnityEngine;
using Mirror;

public class FinishLine : NetworkBehaviour
{
    [SyncVar] private string winnerName = "";
    private bool raceFinished = false;

    void OnTriggerEnter(Collider other)
    {
        if (raceFinished) return;

        // Check if the object is a car
        if (other.CompareTag("Player"))
        {
            raceFinished = true;
            
            // Get player name or identity
            string name = other.gameObject.name;
            
            // Call server to notify everyone
            RpcAnnounceWinner(name);
        }
    }

    [ClientRpc]
    void RpcAnnounceWinner(string name)
    {
        winnerName = name;
        Debug.Log("Race Over! Winner: " + name);
        
        // Show result on UI
        FindObjectOfType<GameLauncherUI>().ShowVictoryScreen(name);
    }
}
