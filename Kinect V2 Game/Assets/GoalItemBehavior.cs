using UnityEngine;
using System.Collections;

public class GoalItemBehavior : LoadScene {

    public GameManager _manager;

	// Use this for initialization
	protected override void Start () {
      
	}

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_manager.isPaused)
        {
            base.OnCollisionEnter2D(collision); 
        }
    }
}
