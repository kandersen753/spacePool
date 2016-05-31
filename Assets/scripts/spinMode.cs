using UnityEngine;
using System.Collections;

public class spinMode : MonoBehaviour {

    public float spinSpeed;
    private Vector3 spinner;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        spinner = new Vector3(0.0f, spinSpeed, 0.0f);
        transform.Rotate(-spinner * Time.deltaTime);
	}

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag ("killPlanet"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
