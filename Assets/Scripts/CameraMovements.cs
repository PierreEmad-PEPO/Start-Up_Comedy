using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovements : MonoBehaviour
{
    public float maxDeeps = 10.5f;
    public float minDeeps = 5.5f;
    Camera _camera;
    Vector3 startPosition;
    Vector3 input = Vector3.zero;
    float scrollInput;
    Vector3 lastPosition;
    float speed = 5f;
    int xLimet;
    int zLimet;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        xLimet = (int)(startPosition.x - 18);
        _camera = GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");
        scrollInput = Input.GetAxis("Mouse ScrollWheel");
        lastPosition = transform.position;
        transform.position += input * speed * Time.deltaTime;
        zLimet = (int)(startPosition.z -  (((GameManager.GroundCount) / 3 ) * 10));
        if (transform.position.x < xLimet || transform.position.x > startPosition.x)
        {
            Vector3 po = transform.position;
            po.x = lastPosition.x;
            transform.position = po;
        }

        if (transform.position.z < zLimet || transform.position.z > startPosition.z)
        {
            Vector3 po = transform.position;
            po.z = lastPosition.z;
            transform.position = po;
        }

        if (scrollInput != 0)
        {
            _camera.orthographicSize += scrollInput;
            if (_camera.orthographicSize > maxDeeps)
                _camera.orthographicSize = maxDeeps;
            if (_camera.orthographicSize < minDeeps)
                _camera.orthographicSize = minDeeps;
        }

    }
}
