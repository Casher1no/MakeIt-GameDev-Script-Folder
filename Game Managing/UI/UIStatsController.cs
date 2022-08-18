using UnityEngine;
using TMPro;

namespace GreyWolf
{
    public class UIStatsController : MonoBehaviour
    {
        ServiceLocator _service;

        [SerializeField] TextMeshProUGUI healthUI, xpUI, upgradePointsUI, enemiesLeftUI;

        private void Start()
        {
            _service = FindObjectOfType<ServiceLocator>();
        }

        private void Update()
        {
            SyncHealth();
            SyncXp();
            SyncEnemiesLeft();
        }

        private void SyncHealth()
        {
            float maxHealth = _service.characterStats.MaxHealth;
            float health = _service.characterStats.Health;

            healthUI.text = $"{health}|{maxHealth}";
        }
        private void SyncXp()
        {
            int xp = _service.experienceManager.Xp;
            int xpGoal = _service.experienceManager.XpRequaired;
            int level = _service.experienceManager.Level;
            int upgradePoints = _service.experienceManager.UpgradePoints;

            xpUI.text = $"{xp}|{xpGoal}";
            upgradePointsUI.text = $"{upgradePoints}";
        }
        private void SyncEnemiesLeft()
        {
            enemiesLeftUI.text = $"{_service.levelManager.GetEnemyCount()}";
        }
    }
}
