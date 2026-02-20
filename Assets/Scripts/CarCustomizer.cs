using UnityEngine;
using Mirror;

public class CarCustomizer : NetworkBehaviour
{
    // SyncVar ensures the color is synchronized from the server to all clients
    [SyncVar(hook = nameof(OnColorChanged))]
    public Color carColor = Color.white;

    public Renderer carRenderer; // Drag the car body mesh here

    public override void OnStartServer()
    {
        // Assign a random color when the car is spawned on the server
        carColor = new Color(Random.value, Random.value, Random.value);
    }

    // This method is called on all clients when the carColor variable changes
    void OnColorChanged(Color oldColor, Color newColor)
    {
        if (carRenderer != null)
        {
            carRenderer.material.color = newColor;
        }
    }

    [Command]
    public void CmdChangeColor(Color newColor)
    {
        carColor = newColor;
    }
}
