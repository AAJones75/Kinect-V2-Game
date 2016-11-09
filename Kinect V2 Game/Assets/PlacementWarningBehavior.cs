using UnityEngine;
using System.Collections;

public class PlacementWarningBehavior : MonoBehaviour {

    public GameManager _manager;

    //Related timer variables
    public float timer = 0f;
    public bool isTiming;
    public bool isItemPlaceable;

    // Use this for initialization
    void Awake()
    {        
        isTiming = true;
    }

    // Update is called once per frame
    void Update () {
        if (!_manager.isPaused)
        {
            if (isItemPlaceable)
            {
                StartTimer();

                if (timer >= 1f)
                {
                    EndTimer();
                    gameObject.SetActive(false);
                }
            }

        }
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

}
