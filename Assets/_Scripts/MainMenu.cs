using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RollerBall
{
    public class MainMenu : CustomMonobehaviour
    {
        [SerializeField]
        private GameObject levelButtonPrefab;
        [SerializeField]
        private GameObject levelButtonContainer;

        protected override void Start()
        {
            Sprite[] thumbnails = Resources.LoadAll<Sprite>("Levels");

            foreach (Sprite thumnail in thumbnails)
            {
                GameObject container = Instantiate(levelButtonPrefab);
                container.GetComponent<Image>().sprite = thumnail;
                container.transform.SetParent(levelButtonContainer.transform, false);
            }
        }
    }
}