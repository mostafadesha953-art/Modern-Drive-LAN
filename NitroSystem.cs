using UnityEngine;
using Mirror;
using System.Collections;

public class NitroSystem : NetworkBehaviour
{
    public CarGameController carController; // Reference to our main controller
    public float nitroMultiplier = 2.0f;    // Double the force
    public float nitroDuration = 3.0f;      // Seconds it lasts
    public float rechargeRate = 0.5f;       // How fast it recharges
    
    [SyncVar] public float currentNitro = 100f;
    private bool isUsingNitro = false;

    void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && currentNitro > 10f && !isUsingNitro)
        {
            StartCoroutine(ActivateNitro());
        }

        if (!isUsingNitro && currentNitro < 100f)
        {
            currentNitro += rechargeRate * Time.deltaTime * 10f;
        }
    }

    IEnumerator ActivateNitro()
    {
        isUsingNitro = true;
        float originalForce = carController.motorForce;
        
        // Boost the force
        carController.motorForce *= nitroMultiplier;
        
        while (currentNitro > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            currentNitro -= 20f * Time.deltaTime; // Consume nitro
            yield return null;
        }

        // Reset force
        carController.motorForce = originalForce;
        isUsingNitro = false;
    }
}
