using UnityEngine;
using UnityEngine.UI;

public class NitroUI : MonoBehaviour
{
    public Slider nitroSlider;
    private NitroSystem playerNitro;

    void Update()
    {
        // Find the local player's nitro system if not already found
        if (playerNitro == null)
        {
            foreach (var car in FindObjectsOfType<NitroSystem>())
            {
                if (car.isLocalPlayer)
                {
                    playerNitro = car;
                    nitroSlider.maxValue = 100f;
                    break;
                }
            }
        }

        // Update the slider value
        if (playerNitro != null)
        {
            nitroSlider.value = playerNitro.currentNitro;
        }
    }
}
