using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreyWolf
{
    public class ActivatePortal : MonoBehaviour
    {
        public LevelManager levelManager;
        [SerializeField] GameObject portalDoor;

        private void Start()
        {
            levelManager = GameObject.FindObjectOfType<LevelManager>();

        }
        private void Update()
        {
            if (levelManager.GetEnemyCount() == 0)
            {
                portalDoor.SetActive(true);
            }
        }
    }
}