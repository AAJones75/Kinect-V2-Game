using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class HandManager : AbstractBodyManager {

    private Body[] bodyData;

    // Use this for initialization
    override protected void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	override protected void Update () {
        base.Update();
        bodyData = GetData();
        //With the body data, use it in conjunction with the kinect ui module
        if (bodyData != null )
        {
            foreach (var body in bodyData)
            {
                //Debug.Log("Got a body!");
                //Let the input module know if it can process data from kinect
                KinectInputModule.instance.isBodyAvailable = body.IsTracked;

                //Check the first available body
                if (body.IsTracked)
                {
                    KinectInputModule.instance.TrackBody(body);
                    break;
                }
            }
        }
	}

    override protected void OnApplicationQuit()
    {
        base.OnApplicationQuit();
    }
}
