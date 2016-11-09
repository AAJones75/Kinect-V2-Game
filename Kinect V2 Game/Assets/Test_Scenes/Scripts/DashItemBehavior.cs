using UnityEngine;
using System.Collections;

public class DashItemBehavior : ItemBehavior {

    public int dashCount = 0;

    // Use this for initialization
    override protected void Start () {
        base.Start();
        isToBeTracked = false; 
	}
	
	// Update is called once per frame
	override protected void Update () {
        base.Update();   
	}

    override protected void OnApplicationQuit()
    {
        base.OnApplicationQuit();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision");
        base.OnCollisionEnter2D(collision);
    }


}
