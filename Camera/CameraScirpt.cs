using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScirpt : MonoBehaviour
{
    Transform playerTarget;

    [SerializeField] Vector3 cameraOffset;


    private void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void LateUpdate()
    {
        transform.position = playerTarget.transform.position + cameraOffset;
    }

}
