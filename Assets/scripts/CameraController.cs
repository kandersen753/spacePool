using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject camera;
    public GameObject player;
    private rollBall cue;
    private int movechoice;
    private float rotationSpeed = 80.0f;
    private Vector3 center;
    private float dist;



    void Start()
    {
        cue = player.GetComponent<rollBall>();
        camera.gameObject.SetActive(false);
        center = new Vector3(0.0f, 10.0f, 0.0f);
    }

    void LateUpdate()
    {
        movechoice = cue.getMoveChoice();
        if (movechoice == 2 || movechoice == 3)
        {
            camera.gameObject.SetActive(true);
            dist = Vector3.Distance(center, camera.transform.position);

            //checks for up and down input
            if (Input.GetAxis("Vertical") != 0)
            {
                camera.transform.RotateAround( center, camera.transform.right, (rotationSpeed * Input.GetAxis("Vertical")) * Time.deltaTime);
               // desiredPosition = (transform.position - center.position).normalized * radius + center.position;
            }

            //checks for left and right input
            if (Input.GetAxis("Horizontal") != 0)
            {
                camera.transform.RotateAround(center, camera.transform.up, -(rotationSpeed * Input.GetAxis("Horizontal")) * Time.deltaTime);
                //desiredPosition = (transform.position - center.position).normalized * radius + center.position;
            }

            //checks for zoom input
            if (Input.GetAxis("Fire1") != 0)
            {
                //keeps camera from getting ontop of focus point
                if (dist > 1.0f)
                {
                    camera.transform.position = Vector3.MoveTowards(camera.transform.position, center, (Input.GetAxis("Fire1") * 5) * Time.deltaTime);
                }

                //allows camera to back up from close point but not zoom in
                else if (dist < 1.0f && Input.GetAxis("Fire1") < 0)
                {
                    camera.transform.position = Vector3.MoveTowards(camera.transform.position, center, (Input.GetAxis("Fire1") * 5) * Time.deltaTime);
                }
            }
        }
        else
        {
            camera.gameObject.SetActive(false);
        }
    }
}