using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreyWolf
{
    public class SpikeTrapController : MonoBehaviour
    {
        float damageToPlayer, damageToEnemy;
        [SerializeField]
        [Range(5, 15)]
        float attackTime = 10f;

        [SerializeField]
        [Tooltip("Recovery Time")]
        float damageTime = 2f;
        float damageTimer, attackTimer = 0f;

        int attackCount = 1;

        bool isActive, attackTrigger = false;

        ServiceLocator _service;
        [SerializeField] Animator anim;

        [SerializeField] List<LayerMask> ignoreEnemyLayer;

        private void Start()
        {
            _service = FindObjectOfType<ServiceLocator>();
            damageToPlayer = _service.trapDamageManager.SpikeDamageToPlayer;
            damageToEnemy = _service.trapDamageManager.SpikeDamageToEnemy;
            attackTime = _service.trapDamageManager.SpikeDelayTime;
        }

        private void Update()
        {
            anim.SetBool("isActive", attackTrigger);

            if (isActive)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackTime)
                {

                    attackTimer = 0;
                    attackCount = 1;
                    attackTrigger = true;
                    isActive = false;
                }
            }

            damageTimer += Time.deltaTime;

            if (damageTimer >= damageTime)
            {
                damageTimer = 0;
                attackTrigger = false;
                attackCount = 0;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && !isActive)
            {
                isActive = true;
            }

            if (attackTrigger && attackCount == 1)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    var player = other.GetComponent<PlayerController>();

                    player.TakeDamage(damageToPlayer);
                }

                if (other.gameObject.CompareTag("Enemy"))
                {
                    var enemy = other.GetComponentInParent<IStats>();
                    enemy.TakeDamage(damageToEnemy);
                }
                attackCount = 0;
            }
        }
    }
}
