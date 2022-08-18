using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetections : MonoBehaviour
{

    [SerializeField]private float destroyTime = 6f;

    private void Update()
    {
        destroyTime -= Time.deltaTime;

        if(destroyTime < 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }

}
