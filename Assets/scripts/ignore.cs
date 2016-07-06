using UnityEngine;
using System.Collections;

public class ignore : MonoBehaviour {
    public GameObject balls;
    public GameObject walls;
    public GameObject child;
    private Collider collisionDetector;
    private MeshRenderer rend;
    


	// Use this for initialization
	void Start ()
    {
        collisionDetector = GetComponent<Collider>();
        rend = child.GetComponent<MeshRenderer>();
        ignoreChildren(balls);
        ignoreChildren(walls);
	}


    void ignoreChildren(GameObject parent)
    {
        //ignores all the children within a parent
        for (int counter = 0; counter < parent.transform.childCount; counter++)
        {
            Physics.IgnoreCollision(collisionDetector, parent.transform.GetChild(counter).gameObject.GetComponent<Collider>());
        }
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnCollisionEnter(Collision other)
    {
        rend.enabled = false;
        collisionDetector.enabled = false;
    }
}
