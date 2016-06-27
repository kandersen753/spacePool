using UnityEngine;
using System.Collections;

public class lightToucher : MonoBehaviour {
   /* public GameObject leftBall;
    public GameObject downBall;
    public GameObject rightBall;
    public GameObject upBall;*/

    private Light changeMe;

    // Use this for initialization
    void Start () {
        changeMe = gameObject.GetComponent<Light>();
    }
	
	// Update is called once per frame
    void Update ()
    {
        //changeMe.color = Color.green;
    }

	void OnTriggerEnter (Collider other)
    {
        Debug.Log("collision active");

        if (other.gameObject.CompareTag ("orb"))
        {
            changeMe.color = Color.red;
        }
	}

    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.CompareTag ("orb"))
        {
            changeMe.color = Color.green;
        }
    }
}
