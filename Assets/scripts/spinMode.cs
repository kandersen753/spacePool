using UnityEngine;
using System.Collections;

public class spinMode : MonoBehaviour {

    public float spinSpeed;
    private Vector3 spinner;
    private Rigidbody rb;
    private int turn;
    public GameObject mainScript;
    private rollBall turnGetter;

    //variables for changing score in the main list
    public GameObject display;
    private listDisplay scoreChanger;

	// Use this for initialization
	void Start ()
    {
        //gets the canvas object
        display = GameObject.Find("Canvas");

        //allows scoreChanger access to the public functions in listDisplay
        scoreChanger = display.GetComponent<listDisplay>();
        turnGetter = mainScript.GetComponent<rollBall>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        spinner = new Vector3(0.0f, spinSpeed, 0.0f);
        transform.Rotate(-spinner * Time.deltaTime);
	}

    void OnTriggerEnter (Collider other)
    {
        turn = turnGetter.getTurn()%2;

        //if the blackhole hits a planet, disable the planet and increment the score
        if (other.gameObject.CompareTag ("killPlanet"))
        {
            other.gameObject.SetActive(false);

            //increments score
            scoreChanger.addScore(turn);
        }

        //if it hits the cueball, disable the ball and freeze it, then reset its postion and decrement score
        else if (other.gameObject.CompareTag ("resetCue"))
        {
            rb = other.GetComponent<Rigidbody>();

            //freezes all constraints in the rigidbody for the cueball
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | 
                            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            //removes all active constraints on cue ball
            rb.constraints = RigidbodyConstraints.None;

            //resets objects position
            other.gameObject.transform.position = new Vector3(0.0f, 10.0f, 0.0f);

            //decrements score
            scoreChanger.minusScore(turn);
        }
    }

}
