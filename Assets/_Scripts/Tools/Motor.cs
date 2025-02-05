using System.Collections;
using System.Collections.Generic;
using RollerBall;
using UnityEngine;

public class Motor : CustomMonobehaviour
{
    private float moveSpeed = 5.0f;
    private float drag = 0.5f;
    private float terminalRotationSpeed = 25.0f;

    private Rigidbody controller;
    private Transform camTransform;

    // Start is called before the first frame update
    protected override void Start()
    {
        this.controller = GetComponent<Rigidbody>();
        this.controller.maxAngularVelocity = terminalRotationSpeed;
        controller.drag = drag;
    }

    // Update is called once per frame
    protected override void Update()
    {
        Movement();
    }

    protected override void LoadComponents()
    {
        LoadCameraTransform();
    }

    private void Movement()
    {
        Vector3 dir = Vector3.zero;
        dir.x = Input.GetAxis(AxisTags.HORIZONTAL);
        dir.z = Input.GetAxis(AxisTags.VERTICAL);

        if (dir.magnitude > 1)
            dir.Normalize();

        Vector3 rotateDir = camTransform.TransformDirection(dir);
        rotateDir = new Vector3(rotateDir.x, 0, rotateDir.z);
        rotateDir = rotateDir.normalized * dir.magnitude;

        this.controller.AddForce(rotateDir * moveSpeed);
    }

    private void LoadCameraTransform()
    {
        camTransform = Camera.main.transform;
    }
}
