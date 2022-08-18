using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreyWolf
{
    [CreateAssetMenu(menuName = "Upgrade/Action/TripleBullets")]
    public class TripleBullets : ScriptableAction
    {
        public new string name;

        public string description;

        public Sprite artwork;

        public override void ActivateUpgrade()
        {
            ServiceLocator _service = FindObjectOfType<ServiceLocator>();

            _service.playerController.SetArrowCount(3);
        }
    }
}