using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] Image primary;
    [SerializeField] Image secondary;
    [SerializeField] Image melee;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("CrosshairUpdate", UpdateCrosshair);
    }

    void UpdateCrosshair(Vector3 crosshairSelect)
    {
        // Debug.Log("Crosshair " + crosshairSelect.x);
        primary.enabled = crosshairSelect.x == 0;
        secondary.enabled = crosshairSelect.x == 1;
        melee.enabled = crosshairSelect.x == 2;
    }

    void OnDestroy()
    {
        EventManager.StopListening("CrosshairUpdate", UpdateCrosshair);
    }
}
