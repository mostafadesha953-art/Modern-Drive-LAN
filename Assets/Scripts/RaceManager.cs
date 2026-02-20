using UnityEngine;
using Mirror;
using System.Collections;
using TMPro;

public class RaceManager : NetworkBehaviour
{
    public TextMeshProUGUI countdownText;
    
    [SyncVar] private bool canMove = false;

    void Start()
    {
        if (isServer)
        {
            StartCoroutine(StartCountdown());
        }
    }

    IEnumerator StartCountdown()
    {
        int timer = 3;
        while (timer > 0)
        {
            RpcUpdateCountdown(timer.ToString());
            yield return new WaitForSeconds(1);
            timer--;
        }
        RpcUpdateCountdown("GO!");
        canMove = true;
        yield return new WaitForSeconds(1);
        RpcUpdateCountdown("");
    }

    [ClientRpc]
    void RpcUpdateCountdown(string msg)
    {
        countdownText.text = msg;
    }

    public bool IsRaceStarted()
    {
        return canMove;
    }
}
