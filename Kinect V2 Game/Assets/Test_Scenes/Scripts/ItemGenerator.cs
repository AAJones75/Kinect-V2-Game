using UnityEngine;
using System.Collections;
using Windows.Kinect;
using System;


public class ItemGenerator : MonoBehaviour {

    public GameObject wallClimbItem;
    public GameObject shootItem;
    public GameObject dashItem;

    public void spawnItem(string gestureInput)
    {
        switch (gestureInput)
        {
            case "LEFT":
                Instantiate(wallClimbItem);
                break;

            case "RIGHT":
                Instantiate(shootItem);
                break;

            case "MID":
                Instantiate(dashItem);
                break;

            default:
                break;
        }
    }
}
