using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GreyWolf
{
    public class UIUpgradeManager : MonoBehaviour
    {


        ServiceLocator _service;

        private void Start()
        {
            _service = FindObjectOfType<ServiceLocator>(); 
        }
        private void Update()
        {
            SyncUpgradeAccesability();
        }

        private void SyncUpgradeAccesability()
        {
            //TODO This was old way, but I want to try with Scriptable Objects, so its easier to add later more upgrades
        }

    }
}
