using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreyWolf
{
    public class SpikeTrapManager : MonoBehaviour
    {
        [SerializeField] BoxCollider triggerArea;


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 size = new Vector3(triggerArea.size.x, triggerArea.size.y, triggerArea.size.z);

            Vector3 transform = triggerArea.transform.position;
            Vector3 pos = new Vector3(transform.x, transform.y + triggerArea.center.y, transform.z);
            Gizmos.DrawWireCube(pos, size);
        }
    }
}
