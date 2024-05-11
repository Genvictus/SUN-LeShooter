using System;
using Unity.VisualScripting;
using UnityEngine;
using Nightmare;

public class PlayerAttack : PausibleObject
{
    public Action attackInput;

    void Awake()
    {
        StartPausible();
    }

    void OnDestroy()
    {
        StopPausible();
    }
    private void Update()
    {
        if (isPaused)
            return;
        
        if (Input.GetMouseButton(0))
            attackInput?.Invoke();
    }
}
