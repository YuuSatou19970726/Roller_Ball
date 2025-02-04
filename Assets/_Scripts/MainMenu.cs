using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RollerBall
{
    public class MainMenu : CustomMonobehaviour
    {
        private const float CAMERA_TRANSITION_SPEED = 3.0F;
        [SerializeField]
        private GameObject levelButtonPrefab;
        [SerializeField]
        private GameObject levelButtonContainer;

        [SerializeField]
        private GameObject shopButtonPrefab;
        [SerializeField]
        private GameObject shopButtonContainer;

        public Material playerMaterial;

        private Transform cameraTransform;
        private Transform cameraDesiredLookAt;

        protected override void LoadComponents()
        {
            cameraTransform = Camera.main.transform;
        }

        protected override void Start()
        {
            ChangePlayerSkin(5);
            ThumnailSprite();
            TextureSprite();
        }

        protected override void Update()
        {
            if (cameraDesiredLookAt != null)
            {
                cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation,
                cameraDesiredLookAt.rotation, CAMERA_TRANSITION_SPEED * Time.deltaTime);
            }
        }

        private void ThumnailSprite()
        {
            Sprite[] thumbnails = Resources.LoadAll<Sprite>(ResourceTags.LEVELS);

            foreach (Sprite thumnail in thumbnails)
            {
                GameObject container = Instantiate(levelButtonPrefab);
                container.GetComponent<Image>().sprite = thumnail;
                container.transform.SetParent(levelButtonContainer.transform, false);

                string sceneName = thumnail.name;
                container.GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneName));
            }
        }

        private void TextureSprite()
        {
            int textureIndex = 0;
            Sprite[] textures = Resources.LoadAll<Sprite>(ResourceTags.PLAYER);

            foreach (Sprite texture in textures)
            {
                GameObject container = Instantiate(shopButtonPrefab);
                container.GetComponent<Image>().sprite = texture;
                container.transform.SetParent(shopButtonContainer.transform, false);

                int index = textureIndex;
                container.GetComponent<Button>().onClick.AddListener(() => ChangePlayerSkin(index));
                textureIndex++;
            }
        }

        private void ChangePlayerSkin(int index)
        {
            float x = (index % 4) * 0.25f;
            float y = ((int)index / 4) * 0.25f;

            if (y == 0.0f)
                y = 0.75f;
            else if (y == 0.25f)
                y = 0.5f;
            else if (y == 0.5f)
                y = 0.25f;
            else if (y == 0.75f)
                y = 0f;

            this.playerMaterial.SetTextureOffset("_MainTex", new Vector2(x, y));
        }

        private void LoadLevel(string sceneName)
        {
            // Debug.Log(sceneName);
            SceneManager.LoadScene(sceneName);
        }

        public void LookAtMenu(Transform menuTransform)
        {
            cameraDesiredLookAt = menuTransform;
            // Camera.main.transform.LookAt(menuTransform.position);
        }
    }
}