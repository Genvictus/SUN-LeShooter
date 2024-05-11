using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nightmare;

public class WeaponSway : PausibleObject {

    [Header("Sway Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float sensitivityMultiplier;

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
        
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivityMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivityMultiplier;

		Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
		Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

		Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, speed * Time.deltaTime);
    }
}
