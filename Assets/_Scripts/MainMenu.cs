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
        private UIManager uIManager;

        public Material playerMaterial;

        private Transform cameraTransform;
        private Transform cameraDesiredLookAt;

        private bool nextLevelLocked = false;

        private int[] costs = {
            0, 150, 150, 150,
            300, 300, 300, 300,
            500, 500, 500, 500,
            1000, 1250, 1500, 2000
        };

        protected override void LoadComponents()
        {
            cameraTransform = Camera.main.transform;
            LoadUIManager();
        }

        protected override void Start()
        {
            ChangePlayerSkin(GameManager.Instance.currentSkinIndex);
            this.uIManager.SetCurrency(GameManager.Instance.currency);
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

            foreach (Sprite thumbnail in thumbnails)
            {
                GameObject container = Instantiate(levelButtonPrefab);
                container.GetComponent<Image>().sprite = thumbnail;
                container.transform.SetParent(levelButtonContainer.transform, false);
                LevelData level = new LevelData(thumbnail.name);

                string minutes = ((int)level.BestTime / 60).ToString("00");
                string seconds = (level.BestTime % 60).ToString("00");

                container.transform.GetChild(0).GetChild(0).GetComponent<Text>().text =
                (level.BestTime != 0.0f) ? minutes + ":" + seconds : "LOCKED";

                container.transform.GetChild(1).GetComponent<Image>().enabled = nextLevelLocked;
                container.GetComponent<Button>().interactable = !nextLevelLocked;

                if (level.BestTime == 0.0f)
                    nextLevelLocked = true;

                string sceneName = thumbnail.name;
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
                container.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = costs[index].ToString();
                // container.transform.GetComponentInChildren<Text>()
                if ((GameManager.Instance.skinAvailability & 1 << index) == 1 << index)
                    container.transform.GetChild(0).gameObject.SetActive(false);
                textureIndex++;
            }
        }

        private void ChangePlayerSkin(int index)
        {
            if ((GameManager.Instance.skinAvailability & 1 << index) == 1 << index)
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
                GameManager.Instance.currentSkinIndex = index;
                GameManager.Instance.SavePlayerPrefs();
            }
            else
            {
                int cost = costs[index];
                if (GameManager.Instance.currency >= cost)
                {
                    GameManager.Instance.currency -= cost;
                    GameManager.Instance.skinAvailability += 1 << index;
                    GameManager.Instance.SavePlayerPrefs();
                    uIManager.SetCurrency(GameManager.Instance.currency);
                    shopButtonContainer.transform.GetChild(index).GetChild(0).gameObject.SetActive(false);
                    ChangePlayerSkin(index);
                }
            }
        }

        private void LoadLevel(string sceneName)
        {
            // Debug.Log(sceneName);
            SceneManager.LoadScene(sceneName);
        }

        private void LoadUIManager()
        {
            uIManager = FindAnyObjectByType<UIManager>();
        }

        public void LookAtMenu(Transform menuTransform)
        {
            cameraDesiredLookAt = menuTransform;
            // Camera.main.transform.LookAt(menuTransform.position);
        }
    }
}