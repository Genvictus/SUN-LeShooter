using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public static Action shootInput;
    public static Action reloadInput;
    
    [SerializeField] private KeyCode reloadKey = KeyCode.R;

    private void Update()
    {
        if (Input.GetMouseButton(0))
            shootInput?.Invoke();

        if (Input.GetKeyDown(reloadKey))
            reloadInput?.Invoke();

    }
}
