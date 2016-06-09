using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class listDisplay : MonoBehaviour {

    public GameObject planets;
    List<GameObject> planetStates;
    public Text list;
    private string list2;
   

	// Use this for initialization
	void Start ()
    {
        planetStates = new List<GameObject>();

        for (int counter = 0; counter < planets.transform.childCount; counter++)
        {
            planetStates.Add(planets.transform.GetChild(counter).gameObject);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        list2 = "";
        for (int counter = 0; counter < planetStates.Count; counter++)
        {
            if (planetStates[counter].activeSelf == true)
            {
                list2 += "<color=#00ff00ff>" + planetStates[counter].name + "</color>" + "\n\n";
            }
            else
            {
                list2 += "<color=#ff0000ff>" + planetStates[counter].name + "</color>" + "\n\n";
            }
        }

        list.text = list2;
	}
}
