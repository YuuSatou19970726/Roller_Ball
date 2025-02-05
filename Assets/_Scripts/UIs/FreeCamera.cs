using System.Collections;
using System.Collections.Generic;
using RollerBall;
using UnityEngine;

public class FreeCamera : CustomMonobehaviour
{
    private VirtualJoystick cameraJoystick;

    private Transform lookAt;

    private float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensivityX = 3.0f;
    private float sensivityY = 1.0f;

    protected override void Update()
    {
        currentX += cameraJoystick.InputDirection.x * sensivityX;
        currentY += cameraJoystick.InputDirection.z * sensivityY;
    }

    void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = lookAt.position + rotation * dir;
        transform.LookAt(lookAt);
    }

    protected override void LoadComponents()
    {
        LoadVirtualJoystick();
        LoadTransform();
    }

    private void LoadVirtualJoystick()
    {
        cameraJoystick = FindAnyObjectByType<VirtualJoystick>();
    }

    private void LoadTransform()
    {
        lookAt = GameObject.FindWithTag(Tags.PLAYER).transform;
    }
}
