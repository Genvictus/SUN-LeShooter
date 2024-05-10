using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Action attackInput;
    private void Update()
    {
        if (Input.GetMouseButton(0))
            attackInput?.Invoke();
    }
}
