using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace GreyWolf
{
    public class SceneManagement : MonoBehaviour
    {
        [SerializeField] VideoPlayer intro;
        [SerializeField] float addLength = 1f;
        [SerializeField] int sceneIndex;

        private void Start()
        {
            intro = GetComponent<VideoPlayer>();
            StartCoroutine(WaitForIntro());
        }

        IEnumerator WaitForIntro()
        {
            float videoLength = (float)intro.length + addLength;
            yield return new WaitForSeconds(videoLength);
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
