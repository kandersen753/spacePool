﻿using UnityEngine;
using System.Collections;

public class vrstick : MonoBehaviour
{

    private Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(rb.velocity);
	}
}
