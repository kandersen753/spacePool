using UnityEngine;
using System.Collections;

public class rollBall : MonoBehaviour
{

    //general variables
    public GameObject ball;
    private int moveChoice = 0;
	private MeshRenderer rend;
	private Collider collisionDetector;
    private Color alphaChanger;
    private int turn;

    //control use variable
    public int controlScheme;

    //ignore collision variables
    public GameObject Walls;
    public GameObject Balls;

    //varialbes for rotation
    public Transform center;
    private float rotationSpeed = 80.0f;

    //variables for hitting
    private float speed;
	private Rigidbody rb;


    void Start()
    {
        //gets components for respected variables
		rb = GetComponent<Rigidbody> ();
		rend = GetComponent<MeshRenderer> ();
		collisionDetector = GetComponent<Collider> ();
        alphaChanger = ball.GetComponent<Renderer>().material.color;

        //allows cue to ignore the mesh collider of balls and walls
        ignoreChildren(Walls);
        ignoreChildren(Balls);

        ball.transform.position = new Vector3(Random.Range(-80.0f, 80.0f), Random.Range(20.0f, 180.0f), Random.Range(-80.0f, 0.0f));

        //moves the cue to the starting posistion
        transform.position = new Vector3((ball.transform.position.x), (ball.transform.position.y), (ball.transform.position.z) - 30);
        transform.localRotation = Quaternion.identity;
        turn = 0;
    }
    
    void ignoreChildren(GameObject parent)
    {
        //ignores all the children within a parent
        for (int counter = 0; counter<parent.transform.childCount; counter++)
        {
            Physics.IgnoreCollision(collisionDetector, parent.transform.GetChild(counter).gameObject.GetComponent<Collider>());
        }
    }

    void FixedUpdate()
    {


        //when movechoice is 0 then the input options rotate 
		if (moveChoice == 0) 
		{
            //reactivate main camera
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);


            alphaChanger.a = .50f;
            ball.GetComponent<Renderer>().material.SetColor("_Color", alphaChanger);




            //ball.GetComponent<Renderer>().material.SetColor("_Color", alphaChanger); 

            if (controlScheme == 0)
            {
                //checks for up and down input
                if (Input.GetAxis("Vertical") != 0)
                {
                    transform.RotateAround(center.position, transform.right, (rotationSpeed * Input.GetAxis("Vertical")) * Time.deltaTime);
                }

                //checks for left and right input
                if (Input.GetAxis("Horizontal") != 0)
                {
                    transform.RotateAround(center.position, transform.forward, (rotationSpeed * Input.GetAxis("Horizontal")) * Time.deltaTime);
                }

                //changes input options so user can hit ball
                if (Input.GetAxis("Jump") != 0.0f)
                {
                    moveChoice = 1;
                    rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

                }

                if (Input.GetAxis("Fire1") != 0)
                {
                    //changes to movechoice so when enter is pressed again, camera will be back on stick
                    moveChoice = 3;

                    //disables main camera
                    transform.GetChild(0).gameObject.SetActive(false);
                }
            }

            else if (controlScheme == 1)
            {

            }
		} 

        //when movechoice is one stick moves back and fourth
		else if (moveChoice == 1) 
		{
            //changes transparency of cueball
            alphaChanger.a = 1.0f;
            ball.GetComponent<Renderer>().material.SetColor("_Color", alphaChanger);

            //disables projection
            transform.GetChild(1).gameObject.SetActive(false);

            if (controlScheme == 0)
            {
                if (Input.GetAxis("Fire2") != 0)
                {
                    moveChoice = 0;
                    //freeze then unfreeze constraints on the stick
                    rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                    rb.constraints = RigidbodyConstraints.None;

                    //reset stick position
                    transform.position = new Vector3((ball.transform.position.x), (ball.transform.position.y), (ball.transform.position.z) - 30);
                    transform.localRotation = Quaternion.identity;
                }

                //moves stick towards and away from ball
                Vector3 movement = new Vector3(0.0f, Input.GetAxis("Vertical"), 0.0f);
                rb.AddRelativeForce(movement * 100.0f);
            }

            else if (controlScheme == 1)
            {

            }
		} 

        //when movechoice is two balls are in motion
		else if (moveChoice == 2) 
		{
            //ensures projection is disabled
            transform.GetChild(1).gameObject.SetActive(false);


            if (controlScheme == 0)
            {
                //when enter is pressed reset cue position and make it reappear
                if (Input.GetAxis("Submit") != 0)
                {
                    rend.enabled = true;
                    collisionDetector.enabled = true;
                    transform.position = new Vector3((ball.transform.position.x), (ball.transform.position.y), (ball.transform.position.z) - 30);
                    transform.localRotation = Quaternion.identity;
                    moveChoice = 0;
                    turn++;
                }
            }

            else if (controlScheme == 1)
            {

            }

		}

        //moves outside camera for overallview
        else if (moveChoice == 3)
        {
            if (controlScheme == 0)
            {
                if (Input.GetAxis("Fire2") != 0)
                {
                    moveChoice = 0;
                }

                if (Input.GetAxis("Submit") != 0)
                {
                    transform.position = new Vector3((ball.transform.position.x), (ball.transform.position.y), (ball.transform.position.z) - 30);
                    transform.localRotation = Quaternion.identity;
                    moveChoice = 0;
                }
            }

            else if (controlScheme == 1)
            {

            }


        }

    }

    //cue collides only with balsl
	void OnCollisionEnter (Collision other)
		{
            //ensures collision with balls
            if (other.gameObject == ball)
            {
                //freeze then unfreeze constraints on the stick
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                rb.constraints = RigidbodyConstraints.None;

                //disable mesh renderer and collider
                rend.enabled = false;
                collisionDetector.enabled = false;

                //disable main camera
                transform.GetChild(0).gameObject.SetActive(false);
                
                moveChoice = 2;
            }
		}

    //returns the current move choice
    public int getMoveChoice()
    {
        return moveChoice;
    }

    //returns the current players turn
    public int getTurn()
    {
        return turn;
    }

    //returns the selected controller scheme
    public int getControlScheme()
    {
        return controlScheme;
    }

}
