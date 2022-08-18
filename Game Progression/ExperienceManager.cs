using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreyWolf
{
    public class ExperienceManager : MonoBehaviour
    {
        private int level = 1;
        public int Level { get => level; }

        private int xp = 0;
        public int Xp { get => xp; }

        private int currentLevel = 1;
        public int CurrentLevel { get => currentLevel; }

        private int xpRequired = 10;
        public int XpRequired { get => xpRequired; }

        private int upgradePoints = 0;
        public int UpgradePoints { get => upgradePoints; }
        // ? Upgrade points are not needed now, but later maybe there will be use case for it.


        [SerializeField] float xpMultiplierByLevel = 1.5f;


        private void Update()
        {
            if (xp >= xpRequired)
            {
                LevelUp();
            }
        }

        public void LevelUp()
        {
            currentLevel++;
            xpRequired = Mathf.RoundToInt(xpRequired * xpMultiplierByLevel);

            level++;
            upgradePoints++;
        }
        public void gainXp(int amount)
        {
            xp += amount;
        }


    }
}
