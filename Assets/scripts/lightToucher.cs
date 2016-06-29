using UnityEngine;
using System.Collections;

public class lightToucher : MonoBehaviour {
    public GameObject leftBall;
    public GameObject downBall;
    public GameObject rightBall;
    public GameObject upBall;
    public GameObject reset;
    public GameObject forward;
    public GameObject backward;
    public SteamVR_TrackedObject trackedObj;
    private bool isTouched = false;
    public GameObject cueBall;
    public Transform center;
    private float rotationSpeed = 20.0f;


    public GameObject rig;

    private Light changeMe;

    // Use this for initialization
    void Start ()
    {
        rig.transform.position = new Vector3((cueBall.transform.position.x), (cueBall.transform.position.y), (cueBall.transform.position.z) - 15);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);

        isTouched = device.GetTouch(SteamVR_Controller.ButtonMask.Trigger);
    }

	void OnTriggerEnter (Collider other)
    {
        Debug.Log("collision active");

        if (other.gameObject.CompareTag ("orb"))
        {
            changeMe = other.gameObject.GetComponent<Light>();
            changeMe.color = Color.red;
        }
	}

    void OnTriggerStay(Collider other)
    {
        if (isTouched)
        { 
            if (other.gameObject == leftBall)
            {
                rig.transform.RotateAround(center.position, transform.up, (rotationSpeed * Time.deltaTime));
            }
            else if (other.gameObject == downBall)
            {
                rig.transform.RotateAround(center.position, transform.right, -(rotationSpeed * Time.deltaTime));
            }
            else if (other.gameObject == rightBall)
            {
                rig.transform.RotateAround(center.position, transform.up, -(rotationSpeed * Time.deltaTime));
            }
            else if (other.gameObject == upBall)
            {
                rig.transform.RotateAround(center.position, transform.right, (rotationSpeed * Time.deltaTime));  
            }
            else if (other.gameObject == reset)
            {
                rig.transform.position = new Vector3((cueBall.transform.position.x), (cueBall.transform.position.y), (cueBall.transform.position.z) - 15);
                rig.transform.localRotation = Quaternion.identity;
            }
            else if (other.gameObject == forward)
            {
                rig.transform.Translate(new Vector3(0.0f, 0.0f, .05f));
            }
            else if (other.gameObject == backward)
            {
                rig.transform.Translate(new Vector3(0.0f, 0.0f, -.05f));
            }
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.CompareTag ("orb"))
        {
            changeMe = other.gameObject.GetComponent<Light>();
            changeMe.color = Color.green;
        }
    }
}
