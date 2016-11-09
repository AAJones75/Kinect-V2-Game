using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour
{
    public GameManager _manager;

    Vector3 currentPosition;
    public float speed;

    //Related timer variables
    public float timer = 0f;
    public bool isTiming;

    // Use this for initialization
    void Start()
    {
        currentPosition = transform.position;
        isTiming = true;
        //Debug.Log("I'm alive!");
    }

    // Update is called once per frame
    void Update()
    {
        if (!_manager.isPaused)
        {
            StartTimer();

            if (timer >= 3f)
            {
                EndTimer();
                gameObject.SetActive(false);
            }
            else
            {
                //Debug.Log("I'm traveling!");
                Move();
            } 
        }

    }

    void Move()
    {
        transform.position = currentPosition + new Vector3(speed * timer, 0, 0);
    }

    void StartTimer()
    {
        if (isTiming)
        {
            RunTimer();
        }
    }

    void RunTimer()
    {
        timer += Time.deltaTime;
    }

    void EndTimer()
    {
        isTiming = false;
        timer = 0f;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_manager.isPaused)
        {
            if (collision.gameObject.tag.Equals("Breakable"))
            {
                collision.gameObject.SetActive(false);
            } 
        }
    }
}
