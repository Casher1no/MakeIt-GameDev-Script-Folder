using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreyWolf
{
    public class TrapDamageManager : MonoBehaviour
    {
        [Header("Fire Trap")]
        [Tooltip("Default Values: Player = 1f | enemy 0.05f")]
        [SerializeField] float fireDamageToPlayer = 1f;
        [SerializeField] float fireDamageToEnemy = .05f;

        public float FireDamageToPlayer { get => fireDamageToPlayer; }
        public float FireDamageToEnemy { get => fireDamageToEnemy; }

        [Header("Spike Trap")]
        [Tooltip("Default Values: Player = 25f | enemy 10f")]
        [SerializeField] float spikeDamageToPlayer = 25f;
        [SerializeField] float spikeDamageToEnemy = 10f;
        [SerializeField] float spikeDelayTime = 1f;

        public float SpikeDamageToPlayer { get => spikeDamageToPlayer; }
        public float SpikeDamageToEnemy { get => spikeDamageToEnemy; }
        public float SpikeDelayTime { get => spikeDelayTime; }
    }
}
