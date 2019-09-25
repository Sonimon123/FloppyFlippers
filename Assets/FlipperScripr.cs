using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperScripr : MonoBehaviour
{

    Rigidbody rb;
    float w = 0.9f;
    Vector3 flip;


    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        flip = new Vector3(0, 1000, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 pos = new Vector3(transform.position.x + 0.8f, transform.position.y + 0.8f, transform.position.z + 0.8f);
            rb.AddForceAtPosition(flip, pos);
        }
    }
}
