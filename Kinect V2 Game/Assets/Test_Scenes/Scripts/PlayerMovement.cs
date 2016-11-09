using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public GameManager _manager;

    public PlatformerMotor2D _motor;
    public int speed;

	// Use this for initialization
	void Start () {
        //Get the motor of the player
        _motor = GetComponent<PlatformerMotor2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!_manager.isPaused)
        {
            _motor.frozen = false;
            _motor.enabled = true;
            // Input based off of an xbox one controller
            //Check for the pressing of the button 
            //and if character is jumping already

            if (Input.GetKey(KeyCode.Joystick1Button0) && !_motor.IsInAir())
            {
                //Make the player jump duh?
                _motor.Jump();
            }

            //Move player based off left joystick input
            _motor.normalizedXMovement = Input.GetAxis("Axis 1") * speed; 
        }
        else
        {
            _motor.frozen = true;
            _motor.enabled = false;
        }

	}
}
