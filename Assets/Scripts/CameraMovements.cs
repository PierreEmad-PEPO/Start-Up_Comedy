using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovements : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 input = Vector3.zero;
    Vector3 lastPosition;
    float speed = 5f;
    int xLimet;
    int zLimet;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        xLimet = (int)(startPosition.x - 18);
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");
        lastPosition = transform.position;
        transform.position += input * speed * Time.deltaTime;
        zLimet = (int)(startPosition.z -  (((GameManager.GroundCount) / 3 ) * 6));
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

    }
}
