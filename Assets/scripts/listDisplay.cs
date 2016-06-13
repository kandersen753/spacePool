﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class listDisplay : MonoBehaviour {

    //game objects and class pieces
    public GameObject planets;
    List<GameObject> planetStates;
    private rollBall cueBall;
    public GameObject cue;

    //text assets
    public Text list;
    public Text endDisplay;
    public Text scoreboard;
    public Text moveOption;

    //data trackers 
    private bool gameOver;
    private int moveChoice;
    private int score;
    private int move;

    //temporary variables
    private string list2;


    // Use this for initialization
    void Start ()
    {
        //assigns planetstates to a list
        planetStates = new List<GameObject>();

        //gets the data for each piece of the list
        for (int counter = 0; counter < planets.transform.childCount; counter++)
        {
            planetStates.Add(planets.transform.GetChild(counter).gameObject);
        }

        //gets class rollball which allows cueball access to class functions
        cueBall = cue.GetComponent<rollBall>();

        //set beginning data
        moveChoice = cueBall.getMoveChoice();
        move = cueBall.getMoveChoice();
        score = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //gets the current move from rollball
        move = cueBall.getMoveChoice();

        //makes list2 empty to assign strings
        list2 = "";

        //loops to check status of all planets, and changes the text color accordingly
        for (int counter = 0; counter < planetStates.Count; counter++)
        {
            if (planetStates[counter].activeSelf == true)
            {
                //if planet is active set text color to green
                list2 += "<color=#00ff00ff>" + planetStates[counter].name + "</color>" + "\n\n";
            }
            else
            {
                //inactive planet gets red
                list2 += "<color=#ff0000ff>" + planetStates[counter].name + "</color>" + "\n\n";
            }
        }

        //displays controls for different move options
        if (move == 0)
        {
            moveOption.text = "Left, Right, Up, Down moves stick position \npress spacebar to move to hitting mode";
        }
        else if (move == 1)
        {
            moveOption.text = "Down Arrow to pull stick back \nUp Arrow to push stick forwards";
        }
        else if (move == 2)
        {
            moveOption.text = "Wait until balls are stopped then press 'Enter' \nto center stick on ball";
        }

        //updates the planet list
        list.text = list2;

        //updates the score counter
        scoreboard.text = "Score: " + score;

        //checks if the game is over
        checkWin();

        
	}

    //checks if the game is over
    void checkWin()
    {
        //variable to ensure all data entries are inactive
        gameOver = true;

        for (int counter = 0; counter < planetStates.Count; counter++)
        {
            //if there is at least 1 active planet the game is not over
            if (planetStates[counter].activeSelf == true)
            {
                gameOver = false;
            }
        }

        //if the game is over display message
        if (gameOver == true)
        {
            endDisplay.text = "Game Over";
        }
    }


    //function for other scripts to call to change the current score
    public void changeScore(int value)
    {
        score = score + value;
    }
}