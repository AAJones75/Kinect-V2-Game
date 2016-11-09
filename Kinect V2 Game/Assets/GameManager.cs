using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject symbolInterface;
    public bool isPaused;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if (isPaused)
            {
                isPaused = false;
            }
            else
            {
                isPaused = true;
            } 
        }

        if (!isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                symbolInterface.SetActive(true);
            } 
        }
	}
}
