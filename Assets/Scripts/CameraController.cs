using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playerTransform;
    public float leftBound = 0f;
    public float rightBound = 100f;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Min(Mathf.Max(playerTransform.position.x, leftBound), rightBound);
        transform.position = position;
    }
}
