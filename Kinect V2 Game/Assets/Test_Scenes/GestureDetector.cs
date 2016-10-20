using UnityEngine;
using System.Collections;
using Windows.Kinect;
using Microsoft.Kinect.VisualGestureBuilder;
using System;
using System.Collections.Generic;

public abstract class GestureDetector : MonoBehaviour {

    private KinectSensor _sensor;
    private VisualGestureBuilderFrameSource vgbFrameSource;
    private VisualGestureBuilderFrameReader vgbFrameReader;

    public string database = "Symbol01";

    public void SetTrackingId(ulong id)
    {
        vgbFrameReader.IsPaused = false;
        vgbFrameSource.TrackingId = id;
        vgbFrameReader.FrameArrived += vgbFrameReader_FrameArrived;
    }

	// Use this for initialization
	protected void Start () {
        //Get and open kinect sensor
        _sensor = KinectSensor.GetDefault();
        _sensor.Open();

        //Create vgb frame source for sensor
        vgbFrameSource =  VisualGestureBuilderFrameSource.Create(_sensor, 0);

        //Grab the vg database from specified path
        string databasePath = @"C:\Users\USER\Desktop\Symbol01\GestureDatabases\" + database + ".gbd";
        VisualGestureBuilderDatabase vgDatabase = VisualGestureBuilderDatabase.Create(databasePath);

        //Check to see if database was properly initialized
        if(vgDatabase != null)
        {
            //Add all gestures found in the database to the source frame
            foreach(Gesture gesture in vgDatabase.AvailableGestures)
            { 
                vgbFrameSource.AddGesture(gesture);
            }
        }

        //Get the reader for the gesture frame source
        //and pause reader until there is a trackable body
        vgbFrameReader = vgbFrameSource.OpenReader();
        vgbFrameReader.IsPaused = true;
    }

    private void vgbFrameReader_FrameArrived(object sender, VisualGestureBuilderFrameArrivedEventArgs e)
    {
        //Get the vgb frame and be sure to dispose of it properly
        using (var frame = e.FrameReference.AcquireFrame())
        {
            if(frame != null)
            {
                ////Observe all discrete gestures retrieved from frame
                //foreach (var result in frame.DiscreteGestureResults)
                //{
                //    Gesture gesture = result.Key;
                //    DiscreteGestureResult gestureResult = result.Value;
                //    float confidence = gestureResult.Confidence;
                //    Debug.Log("Gesture: " + gesture.Name);
                //    Debug.Log("Confidence Level: " + confidence);
                //}

                ////Observe all continous gestures retrieve from frame
                //foreach (var result in frame.ContinuousGestureResults)
                //{
                //    Gesture gesture = result.Key;
                //    ContinuousGestureResult gestureResult = result.Value;
                //    float progress = gestureResult.Progress;
                //    Debug.Log("Gesture: " + gesture.Name);
                //    Debug.Log("Progress Level: " + progress);
                //}

                doAction(frame);
            }
        }
    }

    public abstract void doAction(VisualGestureBuilderFrame frame);
}
