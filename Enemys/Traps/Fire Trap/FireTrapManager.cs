using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreyWolf
{
    [ExecuteInEditMode]
    public class FireTrapManager : MonoBehaviour
    {
        [SerializeField] BoxCollider EffectedArea;
        [SerializeField][Range(1,20)] float range = 5f;
        [SerializeField]List<ParticleSystem> flames;

        [SerializeField] float restTime = 2f;
        [SerializeField] float fireTime = 4f;

        float restTimer = 0f;
        float fireTimer = 0f;

        bool isActive = false;

        private void Start()
        {
            for (int i = 0; i < flames.Count; i++)
            {
                var flameRange = flames[i].GetComponent<ParticleSystem>();
                var particleMainSettings = flameRange.main;
                particleMainSettings.startLifetime = range/20;
                particleMainSettings.loop = true;
            }   
        }

        private void Update()
        {
            EffectedArea.size = new Vector3(1, 5, range);
            EffectedArea.center = new Vector3(0, 0, range / 2);

            ActivatingFire();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 direction = transform.forward;
            Gizmos.DrawRay(transform.position, direction * range);
        }

        private void ActivatingFire()
        {
            ActivationTimers();
            ActivateFlameEmission();
        }

        private void ActivateFlameEmission()
        {
            foreach (ParticleSystem flame in flames)
            {
                var emissionModule = flame.GetComponent<ParticleSystem>().emission;
                emissionModule.enabled = isActive;
            }
        }

        private void ActivationTimers()
        {
            if (!isActive)
            {
                restTimer += Time.deltaTime;
                if (restTimer >= restTime)
                {
                    restTimer = 0;
                    isActive = true;
                }
            }
            else
            {
                fireTimer += Time.deltaTime;
                if (fireTimer >= fireTime)
                {
                    fireTimer = 0;
                    isActive = false;
                }
            }
            EffectedArea.gameObject.SetActive(isActive);
        }
    }
}
