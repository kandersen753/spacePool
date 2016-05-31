using UnityEngine;
using System.Collections;

public class rollBall : MonoBehaviour
{

    public float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float moveZ = Input.GetAxis("Jump");

        Vector3 movement = new Vector3(moveHorizontal, moveZ, moveVertical);

        rb.AddForce(movement * speed);
    }
}
