using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GreyWolf
{
    public class UIController : MonoBehaviour
    {
        
        ServiceLocator _service;

        private void Start()
        {
            _service = FindObjectOfType<ServiceLocator>();
        }

        public void SetInactive(GameObject setInactive)
        {
            setInactive.gameObject.SetActive(false);
        }
        public void SetActive(GameObject setActive)
        {
            setActive.gameObject.SetActive(true);
        }
        public void TogglePause()
        {
            _service.pauseManager.TogglePause();
        }

    }
}
