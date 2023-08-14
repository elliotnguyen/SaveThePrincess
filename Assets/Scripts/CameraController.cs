using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    private Vector3 tempPos;

    public float ZoomChange;
    public float SmoothChange;
    public float MinSize, MaxSize;

    private Camera cam;
    // Start is called before the first frame update
    private void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        player = GameObject.FindWithTag("player").transform;
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            cam.orthographicSize -= ZoomChange * Time.deltaTime * SmoothChange;
        } else if (Input.mouseScrollDelta.y < 0)
        {
            cam.orthographicSize += ZoomChange * Time.deltaTime * SmoothChange;
        }

        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, MinSize, MaxSize);
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        tempPos = transform.position;
        tempPos.x = player.position.x;
        tempPos.y = player.position.y;

        transform.position = tempPos;
    }
}
