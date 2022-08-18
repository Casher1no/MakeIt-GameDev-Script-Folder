using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreyWolf
{
    public class GlobalEnemyController : MonoBehaviour, IStats
    {
        [Header("Global Stats")]
        [SerializeField] protected float damage;
        [SerializeField] protected float health;
        [SerializeField] protected int dropXpAmount;


        protected float disolveTimer = 1f;
        protected float disolveTime;
        protected bool isOnFire = false;

        //Timers
        protected float fireTimer = 0f;
        protected float aimTimer = 0f;

        protected ServiceLocator _service;

        private void Awake() {
            _service = FindObjectOfType<ServiceLocator>();
        }
        private void Start()
        {
            
            disolveTime = disolveTimer;
        }
        protected void Dying(MeshRenderer dissolveShader, ServiceLocator _service, int dropXpAmount)
        {
            disolveTimer -= Time.deltaTime;
            float shaderFloat = (disolveTimer * 15) / disolveTime;

            dissolveShader.material.SetFloat("_CutoffHeight", shaderFloat);
            if (disolveTimer <= 0)
            {
                _service.experienceManager.gainXp(dropXpAmount);
                Destroy(gameObject);
            }
        }
        protected bool HealthStatus(float health, bool isDead)
        {
            if (health <= 0)
            {
                isDead = true;
            }
            if (isDead)
            {
                return false;
            }
            return true;
        }
        protected bool isObstacleBetween(GameObject player, float viewRadiuss) // Checks if there is obstacle between enemy and player in range
        {
            var ray = new Ray(transform.position, player.transform.position - transform.position);
            RaycastHit hit;
            Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.blue);
            if (Physics.Raycast(ray, out hit, viewRadiuss))
            {
                if (hit.transform.gameObject == player || Vector3.Distance(transform.position, player.transform.position) < 1)
                {
                    return true;
                }
            }
            return false;
        }
        public void Attack()
        {
            _service.characterStats.Health = -damage;
        }
        public void TakeDamage(float amount)
        {
            health -= damage;
        }
        public void SetOnFire() => isOnFire = true;
        public void ResetFireTimer() => fireTimer = 0f;
    }
}
