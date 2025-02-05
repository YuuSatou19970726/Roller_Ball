using System.Collections;
using System.Collections.Generic;
using RollerBall;
using UnityEngine;

namespace RollerBall
{
    public class GameManager : CustomMonobehaviour
    {
        private static GameManager instance;
        public static GameManager Instance => instance;

        public int currentSkinIndex = 0;
        public int currency = 0;
        public int skinAvailability = 1;

        protected override void Awake()
        {
            base.Awake();

            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }

        protected override void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        protected override void LoadComponents()
        {
            // PlayerPrefs.DeleteAll();
            LoadPlayerPrefs();
        }

        private void LoadPlayerPrefs()
        {
            if (PlayerPrefs.HasKey(PlayerPrefTags.CURRENT_SKIN))
            {
                currentSkinIndex = PlayerPrefs.GetInt(PlayerPrefTags.CURRENT_SKIN);
                currency = PlayerPrefs.GetInt(PlayerPrefTags.CURRENTCY);
                skinAvailability = PlayerPrefs.GetInt(PlayerPrefTags.SKINAVAILABILITY);
            }
            else
            {
                SavePlayerPrefs();
            }
        }

        public void SavePlayerPrefs()
        {
            PlayerPrefs.SetInt(PlayerPrefTags.CURRENT_SKIN, currentSkinIndex);
            PlayerPrefs.SetInt(PlayerPrefTags.CURRENTCY, currency);
            PlayerPrefs.SetInt(PlayerPrefTags.SKINAVAILABILITY, skinAvailability);
        }
    }
}
