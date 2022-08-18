using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreyWolf
{
    public class CharacterStats : MonoBehaviour
    {
        public static CharacterStats Instance;


        // Player max stats
        [SerializeField] float maxHealth = 100f;
        [SerializeField] float maxDamage = 10f;
        [SerializeField] float maxMovementSpeed = 580f;
        [SerializeField] float maxArmor;
        [SerializeField] float maxProjectileSpeed;

        public float MaxHealth { get => maxHealth; }
        public float MaxDamage { get => maxDamage; }
        public float MaxMovementSpeed { get => maxMovementSpeed; }
        public float MaxArmour { get => maxArmor; }
        public float MaxProjectileSpeed { get => maxProjectileSpeed; }


        // Player reactive Global stats 
        float health;
        float damage;
        float movementSpeed;
        float armor; // Calculate in Player script
        float projectileSpeed;


        // Prototype => Item statistics effects on player stats

        // Buffs 
        float addHealth = 0;
        float addDamage = 0;
        float addMovementSpeed = 0;
        float addArmor = 0;
        float addProjectileSpeed = 0;


        // Player stats Setters/Getters
        public float Health
        {
            get => health;
            set
            {
                health += value;
                if (health < 0)
                {
                    health = 0;
                    print("You Lost!");
                }
            }
        }
        public float Armor
        {
            get => armor;
            set
            {
                armor += value;
                if (armor < 0) armor = 0;
            }
        }
        public float Damage { get => damage; set => damage += value; }
        public float MovementSpeed { get => movementSpeed; set => movementSpeed += value; }
        public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed += value; }

        // Players stat buff Setters
        public float AddArmor { set => addArmor = value; }
        public float AddHealth { set => addHealth = value; }
        public float AddDamage { set => addDamage = value; }
        public float AddMovementSpeed { set => addMovementSpeed = value; }
        public float AddProjectileSpeed { set => addProjectileSpeed = value; }


        private void Start()
        {
            BuffMaxStats();
            ResetMaxStats();
        }

        private void Update()
        {
            // Calculate player stats
        }

        private void ResetMaxStats()
        {
            health = maxHealth;
            damage = maxDamage;
            movementSpeed = maxMovementSpeed;
            armor = maxArmor;
            projectileSpeed = maxProjectileSpeed;
        }

        private void BuffMaxStats() // Buff stats from items
        {
            maxHealth += addHealth;
            maxDamage += addDamage;
            maxMovementSpeed += addMovementSpeed;
            maxArmor += addArmor;
            maxProjectileSpeed += addProjectileSpeed;
        }

        //

    }
}
