using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAbilities : MonoBehaviour
{
    public GameManager _manager;

    //Related shooting variables
    public bool canShoot;
    public List<GameObject> shots;

    //Related wall climbing variables
    public bool canClimb;
    public float climbingTimer;

    //Related dashing variables
    public bool canDash;
    public int dashCount;

    public PlatformerMotor2D _motor;

    // Use this for initialization
    void Start()
    {
        _motor = gameObject.GetComponent<PlatformerMotor2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_manager.isPaused)
        {
            Shoot();
            StartTimer();
            Climb();
            Dash(); 
        }
    }

    void Shoot()
    {
        if (canShoot && Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            if (shots.Count >= 1)
            {
                //Arbitrarily take a shot and fire it
                //Remove from the list once done
                foreach (GameObject shot in shots)
                {
                    //Debug.Log("Setting up bullet");
                    shot.SetActive(true);
                    shots.Remove(shot);
                    break;
                }
            }
            else
            {
                canShoot = false;
            }
        }
    }

    void Climb()
    {
        if (climbingTimer >= 6f)
        {
            _motor.enableWallSticks = false;
            EndTimer();
        }
    }

    void Dash()
    {
        if (dashCount > 0 && Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            _motor.Dash();
            dashCount--;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_manager.isPaused)
        {
            if (collision.gameObject.tag.Equals("ShootItem"))
            {
                collision.gameObject.SetActive(false);
                canShoot = true;
            }

            if (collision.gameObject.tag.Equals("WallClimbingItem"))
            {
                collision.gameObject.SetActive(false);
                _motor.enableWallSticks = true;
                canClimb = true;
            }

            if (collision.gameObject.tag.Equals("DashItem"))
            {
                //Debug.Log("Collision: Player Abilities");
                collision.gameObject.SetActive(false);
                _motor.enableDashes = true;
                dashCount = collision.gameObject.GetComponent<DashItemBehavior>().dashCount;
                canDash = true;
            } 
        }
    }

    void StartTimer()
    {
        if (canClimb)
        {
            RunTimer();
        }
    }

    void RunTimer()
    {
        climbingTimer += Time.deltaTime;
    }

    void EndTimer()
    {
        canClimb = false;
        climbingTimer = 0f;
    }

    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("Hitting env!");
    //    if(collision.gameObject.tag.Equals("Grippable") && canClimb)
    //    {
    //        _motor.enableWallSticks = true;
    //    }
    //    else
    //    {
    //        Debug.Log("Touching non grippable wall");
    //        _motor.enableWallSticks = false;
    //    }
    //}
}
