using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreyWolf
{
    public class FireTrapController : MonoBehaviour
    {
        float damageToPlayer = 0;
        float damageToEnemy = 0f;

        ServiceLocator _service;

        private void Start()
        {
            _service = FindObjectOfType<ServiceLocator>();
            damageToPlayer = _service.trapDamageManager.FireDamageToPlayer;
            damageToEnemy = _service.trapDamageManager.FireDamageToEnemy;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var player = other.GetComponent<PlayerController>();

                player.ResetFireTimer();
                player.SetOnFire();

                player.TakeDamage(damageToPlayer);
            }

            if (other.gameObject.CompareTag("Enemy"))
            {
                var enemy = other.GetComponentInParent<IStats>();

                enemy.ResetFireTimer();
                enemy.SetOnFire();

                enemy.TakeDamage(damageToEnemy);
            }
        }
    }
}
