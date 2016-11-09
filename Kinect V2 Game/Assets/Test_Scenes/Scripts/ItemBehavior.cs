using UnityEngine;
using System.Collections;
using Windows.Kinect;
using UnityEngine.UI;

public class ItemBehavior : AbstractBodyManager {

    public GameManager _manager;

    private Body[] bodyData;
    public bool isTrackingItem;
    public bool trackingEnabled;
    public bool isToBeTracked;

    //Stages user goes through to release item
    public bool stageOne;//Open hand
    public bool stageTwo;//Close hand
    public bool stageThree;//Open hand again

    public GameObject placementWarning;
    public bool isItemPlaceable;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        trackingEnabled = true;
        isToBeTracked = true;
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        if (!_manager.isPaused)
        {
            if (isToBeTracked)
            {
                bodyData = GetData();
                //With the body data, use it in conjunction with the kinect ui module
                if (bodyData != null)
                {
                    foreach (var body in bodyData)
                    {
                        //Check the first available body
                        if (body.IsTracked)
                        {                     
                            UpdatePosition(body);
                            checkPlacementGesture(body);
                            isItemPlaceable = true;
                            break;
                        }
                    }
                }
            } 
        }
    }

    private void UpdatePosition(Body body)
    {
        Windows.Kinect.Joint hand = body.Joints[JointType.HandRight];
        if (hand.TrackingState == TrackingState.Tracked && trackingEnabled)
        {
            transform.position = new Vector3(hand.Position.X * 10, hand.Position.Y * 10, 0f);
            isTrackingItem = true;
        }
    }

    private void checkPlacementGesture(Body body)
    {
        HandState state = body.HandRightState;

        if(isTrackingItem)
        {
            if(state.Equals(HandState.Open))
            {
                stageOne = true;
            }

            if(stageOne && state.Equals(HandState.Closed))
            {
                stageTwo = true;
            }

            if(stageTwo && state.Equals(HandState.Open))
            {
                stageThree = true;
            }
        }

        if(stageThree && isItemPlaceable)
        {
            trackingEnabled = false;
        }
    }

    override protected void OnApplicationQuit()
    {
        base.OnApplicationQuit();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gameObject.tag == "Level")
        //{
        //    PlacementWarningBehavior _placementWarning = placementWarning.GetComponent<PlacementWarningBehavior>();
        //    placementWarning.SetActive(true);
        //    _placementWarning.isTiming = true;
        //    isItemPlaceable = false;
        //    _placementWarning.isItemPlaceable = isItemPlaceable;
        //}
    }
}
