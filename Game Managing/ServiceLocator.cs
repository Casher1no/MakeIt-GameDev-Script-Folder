using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreyWolf
{
    public class ServiceLocator : MonoBehaviour
    {
        public static ServiceLocator Instance { get; private set; }

        public LevelManager levelManager { get; private set; }
        public CharacterStats characterStats { get; private set; }
        public PauseManager pauseManager { get; private set; }
        public SettingsManager settingsManager { get; private set; }
        public TrapDamageManager trapDamageManager { get; private set; }
        public ExperienceManager experienceManager { get; private set; }
        public PlayerController playerController { get; private set; }
        public UpgradeManager upgradeManager { get; private set; }

        public GameObject gameUI { get; private set; }
        public GameObject pauseMenu { get; private set; }






        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);



            levelManager = GetComponentInChildren<LevelManager>();
            characterStats = GetComponentInChildren<CharacterStats>();
            pauseManager = GetComponentInChildren<PauseManager>();
            settingsManager = GetComponentInChildren<SettingsManager>();
            trapDamageManager = GetComponentInChildren<TrapDamageManager>();
            experienceManager = GetComponentInChildren<ExperienceManager>();
            playerController = GetComponentInChildren<PlayerController>();
            upgradeManager = GetComponentInChildren<UpgradeManager>();

            gameUI = GameObject.Find("GameUI");
            pauseMenu = GameObject.Find("PauseMenu");
        }

    }
}
