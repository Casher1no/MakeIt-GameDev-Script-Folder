using UnityEngine;

namespace GreyWolf
{
    public class DoorAccessor : MonoBehaviour, IDoorAccessor
    {
        [SerializeField] Animator doorAnimator;


        public void OpenDoor()
        {
            doorAnimator.SetBool("isEnemyLeft", true);
        }
        public void CloseDoor()
        {
            doorAnimator.SetBool("isEnemyLeft", false);
        }

    }
}
