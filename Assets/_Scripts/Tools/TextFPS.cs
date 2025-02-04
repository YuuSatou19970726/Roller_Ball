using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RollerBall
{
    public class TextFPS : CustomMonobehaviour
    {
        private Text txtFPS;

        protected override void Awake()
        {
            this.txtFPS = GetComponent<Text>();

            InvokeRepeating(nameof(UpdateFPS), 0f, 1f);

            // Set frame rate to the default
            // Application.targetFrameRate = -1;

            // Set target FPS in game
            // Application.targetFrameRate = 120;
        }

        private void UpdateFPS()
        {
            float fps = 1 / Time.deltaTime;
            this.txtFPS.text = fps.ToString("F2");
        }
    }
}