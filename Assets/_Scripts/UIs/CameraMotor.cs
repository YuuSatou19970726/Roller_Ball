using System.Collections;
using System.Collections.Generic;
using RollerBall;
using UnityEngine;

public class CameraMotor : CustomMonobehaviour
{
    private const float TIME_BEFORE_START = 2.5f;
    private Transform lookAt;

    private Vector3 desiredPosition;
    private Vector3 offset;

    private Vector2 touchPosition;
    private float swipeResistance = 200.0f;

    private float smoothSpeed = 7.5f;
    private float distance = 5.0f;
    private float yOffset = 3.5f;

    private float startTime = 0;

    protected override void Start()
    {
        offset = new Vector3(0, yOffset, -1f * distance);
        // transform.rotation = Quaternion.Euler(30, 0, 0);
        this.startTime = Time.time;
    }

    protected override void Update()
    {
        if (Time.time - this.startTime < TIME_BEFORE_START)
            return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            SlideCamera(true);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            SlideCamera(false);

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            touchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            float swipeForce = touchPosition.x - Input.mousePosition.x;
            if (Mathf.Abs(swipeForce) > swipeResistance)
            {
                if (swipeForce < 0)
                    SlideCamera(true);
                else
                    SlideCamera(false);
            }
        }
    }

    protected override void FixedUpdate()
    {
        if (Time.time - this.startTime < TIME_BEFORE_START)
            return;

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
