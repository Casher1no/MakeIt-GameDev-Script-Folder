using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GreyWolf
{
    public class FlopyController : GlobalEnemyController
    {
        [Header("Flopy Stats")]
        [SerializeField][Range(4,20)] float movementSpeed; //8f
        [SerializeField][Range(5, 25f)] float viewRadiuss; //15f

        [Header("Attack Configuration")]
        [SerializeField][Range(.5f,3f)] float recoveryTime; //1f
        [SerializeField][Range(1f,6f)] float playerDisctance; //4f
        [SerializeField][Range(.05f,1f)] float chargeAttackTime; //.1f
        [SerializeField][Range(15,30)] float attackSpeed; //20f
        [SerializeField][Range(50,150)] float acceleration; //100f
        [SerializeField][Range(10,30)] float accelerationMultiplier; //20f
        [SerializeField][Range(1f,2f)] float attackRadiuss; //1f
        [SerializeField][Range(.5f,2f)] float fireTime; //1f
        // -------------

        protected bool damagedDealed = false;
        protected bool isTargetSet = false;
        protected bool isAttacking = false;

        public bool IsOnFire { get { return isOnFire; } }

        protected bool isDead = false;

        protected float chargeAttackTimer = 0;
        protected float recoveryTimer = 0f;


        GameObject player;
        SphereCollider viewTrigger;
        NavMeshAgent enemy;
        ParticleSystem fireParticles;

        Vector3 attackPosition;

        [Header("Components")]
        [Space(5)]
        [SerializeField] Animator e_Animator;
        [SerializeField] MeshRenderer dissolveShader;

        float shaderFloat;


        private void Start()
        {

            player = GameObject.FindGameObjectWithTag("Player");
            enemy = GetComponent<NavMeshAgent>();
            enemy.speed = movementSpeed;
            viewTrigger = GetComponent<SphereCollider>();
            viewTrigger.radius = viewRadiuss;
            fireParticles = GetComponentInChildren<ParticleSystem>();
        }
        private void Update()
        {
            if (!isDead) AttacksTarget();

            OnFire();
            if (!HealthStatus(health, isDead)) Dying(dissolveShader, _service, dropXpAmount);
        }

        private void OnTriggerStay(Collider other)
        {
            if (!isTargetSet)
            {
                if (other.gameObject.tag == "Player" && isObstacleBetween(player, viewRadiuss))
                {
                    FollowsPlayer();
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
            Gizmos.DrawWireSphere(transform.position, viewRadiuss);
        }

        // Methods ------------------------


        private void AttacksTarget()
        {
            if (isTargetSet)
            {
                if (chargeAttackTimer <= chargeAttackTime && !isAttacking)
                {
                    chargeAttackTimer += Time.deltaTime;
                }
                else
                {
                    ChargesAttack();

                    if (isAttacking)
                    {
                        if (recoveryTimer <= recoveryTime)
                        {
                            AttackPlayer();
                        }
                        else
                        {
                            ResetAttack();
                        }
                    }
                }
            }
        }
        private void FollowsPlayer()
        {
            transform.LookAt(player.transform);
            enemy.SetDestination(player.transform.position);

            AttackPlayerInRange();
        }

        private void AttackPlayerInRange()
        {
            if (Vector3.Distance(transform.position, player.transform.position) < playerDisctance)
            {
                enemy.ResetPath();
                attackPosition = player.transform.position;
                isTargetSet = true;
            }
        }

        private void ChargesAttack()
        {
            isAttacking = true;
            chargeAttackTimer = 0f;
            enemy.speed = attackSpeed;
            enemy.acceleration = acceleration * accelerationMultiplier;
            enemy.SetDestination(attackPosition);
            if (Vector3.Distance(transform.position, attackPosition) < 1f || Vector3.Distance(transform.position, player.transform.position) < 1f)
            {
                enemy.velocity = Vector3.zero;
                enemy.ResetPath();
            }
        }

        private void AttackPlayer()
        {
            recoveryTimer += Time.deltaTime;
            if (Vector3.Distance(transform.position, player.transform.position) < attackRadiuss && !damagedDealed)
            {
                Attack();
                e_Animator.SetTrigger("isAttacking");
                damagedDealed = true;
            }
        }

        private void ResetAttack()
        {
            enemy.speed = movementSpeed;
            enemy.acceleration = acceleration;
            recoveryTimer = 0f;
            isAttacking = false;
            isTargetSet = false;
            damagedDealed = false;
        }

        private void OnFire()
        {
            var emissionModule = fireParticles.emission;
            emissionModule.enabled = isOnFire;
            if (fireTimer == 0f || fireTimer <= fireTime)
            {
                fireTimer += Time.deltaTime;
            }
            if (fireTimer >= fireTime) isOnFire = false;
        }
       
    }
}