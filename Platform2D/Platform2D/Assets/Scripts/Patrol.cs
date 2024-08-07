using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    float xPointA;
    float xPointB;
    // Start is called before the first frame update
    void Start()
    {
        if (pointA)
        {
            xPointA = pointA.position.x;
            xPointB = pointB.position.x;
        }
        else
        {
            xPointA = transform.position.x;
            xPointB = pointB.position.x;
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        
        if (transform.position.x > xPointB)
        {
            transform.Rotate(0, 180, 0);
            transform.position = new Vector3(xPointB, transform.position.y, 0);
        }
        else if (transform.position.x < xPointA)
        {
            transform.Rotate(0, 180, 0);
            transform.position = new Vector3(xPointA, transform.position.y, 0);
        }

        //max = 3.85

    }
}
