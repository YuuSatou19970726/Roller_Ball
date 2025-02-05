using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RollerBall
{
    public class VirtualJoystick : CustomMonobehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        private Image bgImg;
        private Image joystickImg;

        [HideInInspector]
        public Vector3 InputDirection { set; get; }

        protected override void Start()
        {
            base.Start();
            InputDirection = Vector3.zero;
        }

        protected override void LoadComponents()
        {
            LoadComponentImage();
        }

        private void LoadComponentImage()
        {
            bgImg = GetComponent<Image>();
            joystickImg = transform.GetChild(0).GetComponent<Image>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 pos = Vector2.zero;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle
            (bgImg.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
            {
                pos.x /= this.bgImg.rectTransform.sizeDelta.x;
                pos.y /= this.bgImg.rectTransform.sizeDelta.y;

                float x = (this.bgImg.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
                float y = (this.bgImg.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

                InputDirection = new Vector3(x, 0, y);
                InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

                joystickImg.rectTransform.anchoredPosition = new Vector3(
                    InputDirection.x * (this.bgImg.rectTransform.sizeDelta.x / 3),
                    InputDirection.z * (this.bgImg.rectTransform.sizeDelta.y / 3)
                );
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            InputDirection = Vector3.zero;
            joystickImg.rectTransform.anchoredPosition = Vector3.zero;
        }
    }
}
