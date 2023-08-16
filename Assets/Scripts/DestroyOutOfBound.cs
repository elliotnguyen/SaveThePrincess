using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBound : MonoBehaviour
{
    [SerializeField] float topBound;
    [SerializeField] float downBound;
    [SerializeField] float leftBound;
    [SerializeField] float rightBound;

    private void Update()
    {
        if (transform.position.x <= leftBound || transform.position.x >= rightBound)
        {
            Destroy(gameObject);
        }

        if (transform.position.y >= topBound || transform.position.y <= downBound)
        {
            Destroy(gameObject);
        }
    }
}
