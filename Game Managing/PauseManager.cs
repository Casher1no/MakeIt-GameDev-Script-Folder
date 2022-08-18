using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GreyWolf
{
    public class PauseManager : MonoBehaviour
    {
        private bool isPaused = false;

        public bool IsPaused { get => isPaused; }

        private void Update()
        {
            if (IsPaused)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
        public void TogglePause() => isPaused = !isPaused;
    }
}
