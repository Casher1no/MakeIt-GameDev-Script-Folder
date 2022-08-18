using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreyWolf
{
    public class UpgradeManager : MonoBehaviour
    {
        // This script contains all upgrades and save file management functions 
        private bool u_Fire = false;
        private bool u_TripleArrow = false;

        public bool U_Fire { get => u_Fire; }
        public bool U_TripleArrow { get => u_TripleArrow; }
    }
}
