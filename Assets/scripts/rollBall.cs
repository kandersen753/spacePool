using UnityEngine;
using System.Collections;

public class rollBall : MonoBehaviour
{

    //general stuff
    public GameObject ball;
    public int moveChoice = 0;


    //stuff for rotation
    public Transform center;
    public Vector3 axisUp = Vector3.up;
    public Vector3 axisRight = Vector3.right;
    private Vector3 desiredPosition;
    private float radius = 3.0f;
    public float radiusSpeed = 0.5f;
    public float rotationSpeed = 80.0f;

    //stuff for hitting
    private Transform target;
    public float speed;
    float step;


    void Start()
    {
        
    }

    void FixedUpdate()
    {
        //changes input options so user can hit ball
        if (Input.GetAxis("Jump") != 0.0f)
        {
            moveChoice = 1;
            target = center;
        }

        //when movechoice is 0 then the input options rotate 
        if (moveChoice == 0)
        {
            axisUp = new Vector3(0, Input.GetAxis("Vertical"), 0);
            axisRight = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

            if (axisUp == new Vector3(0, 1, 0) || axisUp == new Vector3(0, -1, 0))
            {
                transform.RotateAround(center.position, transform.right, (rotationSpeed * Input.GetAxis("Vertical")) * Time.deltaTime);
                desiredPosition = (transform.position - center.position).normalized * radius + center.position;
            }

            if (axisRight == new Vector3(1, 0, 0) || axisRight == new Vector3(-1, 0, 0))
            {
                transform.RotateAround(center.position, transform.forward, (rotationSpeed * Input.GetAxis("Horizontal")) * Time.deltaTime);
                desiredPosition = (transform.position - center.position).normalized * radius + center.position;
            }
        }

        else if (moveChoice == 1)
        {
            step = Input.GetAxis("Vertical") * Time.deltaTime*speed;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

    }
}
