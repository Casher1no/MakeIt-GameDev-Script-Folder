using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreyWolf
{
    public class FirePedistalController : GlobalEnemyController, IStats
    {
        [Header("Fire Pedistal Stats")]
        [SerializeField][Range(10, 30)] protected float viewRadius;

        [Header("Attack Configuration")]
        [SerializeField]
        [Tooltip("For how long will it take for enemy to shoot after player stays in trigger area")]
        protected float aimTime = 2f;
        [SerializeField]
        [Tooltip("For how long will enemy be on fire")]
        protected float fireTime;
        [SerializeField]
        [Tooltip("Adjust aiming center for enemy")]
        Vector3 adjustAimingCenter;

        [Header("Inputs")]
        [SerializeField] ParticleSystem magicLoading;
        [SerializeField] Animator anim;
        [SerializeField] Transform aimPointDirection;

        //-------------------------


        protected bool isDead = false;
        protected bool isTargetSet = false;



        [SerializeField] Transform startPoint;


        GameObject player;
        SphereCollider viewTrigger;

        [SerializeField] MeshRenderer dissolveShader;

        private void Start()
        {

            player = GameObject.FindGameObjectWithTag("Player");
            viewTrigger = GetComponent<SphereCollider>();
            viewTrigger.radius = viewRadius;
        }

        private void Update()
        {

            aimPointDirection.LookAt(player.transform.position + adjustAimingCenter);

            var emissionModule = magicLoading.emission;

            if (isTargetSet)
            {
                emissionModule.enabled = true;

                aimTimer += Time.deltaTime;

                if (aimTimer >= aimTime)
                {
                    anim.SetTrigger("attack");


                    isTargetSet = false;
                }
            }
            else
            {
                aimTimer = 0;
                emissionModule.enabled = false;
            }

            if (!HealthStatus(health, isDead)) Dying(dissolveShader, _service, dropXpAmount);
        }
        private void OnTriggerStay(Collider other)
        {
            if (!isTargetSet)
            {
                if (other.gameObject.tag == "Player" && isObstacleBetween(player, viewRadius))
                {
                    isTargetSet = true;
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (isTargetSet)
            {
                if (other.gameObject.tag == "Player")
                {
                    isTargetSet = false;
                }
            }
        }
        private void OnParticleCollision(GameObject other)
        {
            if (other.transform.parent.tag == "Player")
            {
                TakeDamage(_service.characterStats.Damage);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, viewRadius);
        }

        // Methods ------------------------



    }
}
