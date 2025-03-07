using System.Collections;
using System.Collections.Generic;
using RollerBall;
using UnityEngine;

namespace RollerBall
{
    public class Motor : CustomMonobehaviour
    {
        private const float TIME_BEFORE_START = 3.0f;

        private float moveSpeed = 5.0f;
        private float drag = 0.5f;
        private float terminalRotationSpeed = 25.0f;
        private VirtualJoystick moveJoystick;

        private float boostSpeed = 5.0f;
        private float boostCooldown = 2.0f;
        private float lastBoost;

        private Rigidbody controller;
        private Transform camTransform;

        private float startTime = 0;

        // Start is called before the first frame update
        protected override void Start()
        {
            this.lastBoost = Time.time - boostCooldown;
            this.startTime = Time.time;

            this.controller = GetComponent<Rigidbody>();
            this.controller.maxAngularVelocity = terminalRotationSpeed;
            controller.drag = drag;
        }

        // Update is called once per frame
        protected override void Update()
        {
            if (Time.time - this.startTime < TIME_BEFORE_START)
                return;

            Movement();
        }

        protected override void LoadComponents()
        {
            LoadCameraTransform();
            LoadVirtualJoystick();
        }

        private void Movement()
        {
            Vector3 dir = Vector3.zero;
            dir.x = Input.GetAxis(AxisTags.HORIZONTAL);
            dir.z = Input.GetAxis(AxisTags.VERTICAL);

            if (moveJoystick.InputDirection != Vector3.zero)
            {
                dir = moveJoystick.InputDirection;
            }

            if (dir.magnitude > 1)
                dir.Normalize();

            Vector3 rotateDir = camTransform.TransformDirection(dir);
            rotateDir = new Vector3(rotateDir.x, 0, rotateDir.z);
            rotateDir = rotateDir.normalized * dir.magnitude;

            this.controller.AddForce(rotateDir * moveSpeed);
        }

        public void Boost()
        {
            if (Time.time - this.startTime < TIME_BEFORE_START)
                return;

            if (Time.time - lastBoost > boostCooldown)
            {
                lastBoost = Time.time;
                this.controller.AddForce(controller.velocity.normalized * boostSpeed, ForceMode.VelocityChange);
            }
        }

        private void LoadCameraTransform()
        {
            camTransform = Camera.main.transform;
        }

        private void LoadVirtualJoystick()
        {
            moveJoystick = FindAnyObjectByType<VirtualJoystick>();
        }
    }
}
