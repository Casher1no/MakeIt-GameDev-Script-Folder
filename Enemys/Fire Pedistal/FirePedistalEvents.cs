using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreyWolf
{
    public class FirePedistalEvents : MonoBehaviour
    {
        [SerializeField] ParticleSystem shootingParticle;

        public void Shoot()
        {
            shootingParticle.Play();
        }
    }
}
