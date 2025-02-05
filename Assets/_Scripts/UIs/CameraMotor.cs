using System.Collections;
using System.Collections.Generic;
using RollerBall;
using UnityEngine;

public class CameraMotor : CustomMonobehaviour
{
    private Transform lookAt;

    private Vector3 desiredPosition;
    private Vector3 offset;

    private float smoothSpeed = 7.5f;
    private float distance = 5.0f;
    private float yOffset = 3.5f;

    protected override void Start()
    {
        offset = new Vector3(0, yOffset, -1f * distance);
        // transform.rotation = Quaternion.Euler(30, 0, 0);
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            SlideCamera(true);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            SlideCamera(false);
    }

    protected override void FixedUpdate()
    {
        desiredPosition = lookAt.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(lookAt.position + Vector3.up);
    }

    protected override void LoadComponents()
    {
        LoadTransform();
    }

    private void SlideCamera(bool left)
    {
        if (left)
            offset = Quaternion.Euler(0, 90, 0) * offset;
        else
            offset = Quaternion.Euler(0, -90, 0) * offset;
    }

    private void LoadTransform()
    {
        lookAt = GameObject.FindWithTag(Tags.PLAYER).transform;
    }
}
