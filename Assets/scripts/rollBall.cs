using UnityEngine;
using System.Collections;

public class rollBall : MonoBehaviour
{

    //general stuff
    public GameObject ball;
    public int moveChoice = 0;
	MeshRenderer rend;
	Collider collisionDetector;

    //ignore collision stuff
    public GameObject Walls;
    public GameObject Balls;

    //stuff for rotation
    public Transform center;
    public Vector3 axisUp = Vector3.up;
    public Vector3 axisRight = Vector3.right;
    private Vector3 desiredPosition;
    private float radius = 3.0f;
    public float radiusSpeed = 0.5f;
    public float rotationSpeed = 80.0f;

    //stuff for hitting
    public float speed;
    float step;
	private Rigidbody rb;


    void Start()
    {
		rb = GetComponent<Rigidbody> ();
		rend = GetComponent<MeshRenderer> ();
		collisionDetector = GetComponent<Collider> ();
        ignoreChildren(Walls);
        ignoreChildren(Balls);
    }
    
    void ignoreChildren(GameObject parent)
    {
        for (int counter =0; counter<parent.transform.childCount; counter++)
        {
            Physics.IgnoreCollision(collisionDetector, parent.transform.GetChild(counter).gameObject.GetComponent<Collider>());
        }
    }

    void FixedUpdate()
    {


        //when movechoice is 0 then the input options rotate 
		if (moveChoice == 0) 
		{


			if (Input.GetAxis("Vertical") != 0) 
			{
				transform.RotateAround (center.position, transform.right, (rotationSpeed * Input.GetAxis ("Vertical")) * Time.deltaTime);
				desiredPosition = (transform.position - center.position).normalized * radius + center.position;
			}

			if (Input.GetAxis("Horizontal") != 0)
			{
				transform.RotateAround (center.position, transform.forward, (rotationSpeed * Input.GetAxis ("Horizontal")) * Time.deltaTime);
				desiredPosition = (transform.position - center.position).normalized * radius + center.position;
			}

			//changes input options so user can hit ball
			if (Input.GetAxis("Jump") != 0.0f)
			{
				moveChoice = 1;
				rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

			}
		} 

		else if (moveChoice == 1) 
		{
			step = Input.GetAxis ("Vertical");
			Vector3 movement = new Vector3 (0.0f, step, 0.0f);
			rb.AddRelativeForce (movement * 10.0f);
		} 

		else if (moveChoice == 2) 
		{
			if (Input.GetAxis ("Submit") != 0) 
			{
				rend.enabled = true;
				collisionDetector.enabled = true;
				transform.position = new Vector3 ((ball.transform.position.x), (ball.transform.position.y), (ball.transform.position.z)-3);
				transform.localRotation = Quaternion.identity;
				moveChoice = 0;
			}
		}

    }

	void OnCollisionEnter (Collision other)
		{
            if (other.gameObject == ball)
            {
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                rb.constraints = RigidbodyConstraints.None;
                rend.enabled = false;
                collisionDetector.enabled = false;
                moveChoice = 2;
            }
		}

}
