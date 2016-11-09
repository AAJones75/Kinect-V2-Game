using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GestureMeterBehavior : MonoBehaviour {

    public int startingMeter = 4;
    public int currentMeter;
    public Slider gestureMeter;
    public bool isMeterAvailable;

	// Use this for initialization
	void Start () {
        currentMeter = startingMeter;
        isMeterAvailable = true;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Current Meter: " + currentMeter);
	}

    public void DecrementMeter(int amount)
    {

        if(currentMeter - amount < 0)
        {
            isMeterAvailable = false;
            return;
        }

        currentMeter -= amount;
        gestureMeter.value = currentMeter;

        if(currentMeter == 0)
        {
            isMeterAvailable = false;
        }
        else
        {
            isMeterAvailable = true;
        }
    }
}
