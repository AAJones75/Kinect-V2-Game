using UnityEngine;
using Microsoft.Kinect.VisualGestureBuilder;
using UnityEngine.UI;

public class SymbolOneDetector : GestureDetector {

    //Decouple from this class
    //Create individual objects or something to represent this data
    //Observed Gestures
    const string TOP_LEFT = "Symbol01_TopLeft";
    const string BOTTOM_MID = "Symbol01_BottomMid";
    const string TOP_RIGHT = "Symbol01_TopRight";
    const string SYMBOL_PROGRESS = "Symbol01Progress";

    bool topLeftDetected;
    bool topRightDetected;
    bool bottomMidDetected;

    //Item Generator for the appropriate gesture
    public ItemGenerator generator;

    //Confidence threshold for discrete gestures
    //Fairly weak confidence, I'll provide more learning later :)
    public float discreteConfidenceThreshold = 0.5f;

    public Text progressDisplay; 

    void Start()
    {
        base.Start();
    }

    public override void doAction(VisualGestureBuilderFrame frame)
    {
        Debug.Log("Hi I am the symbol one detector doing my own custom stuff with the gesture");
        Debug.Log("Hi I am the tracking id of the frame passed by symbol one detector's parent class " + frame.TrackingId);

        //Observe all discrete gestures retrieved from frame
        foreach (var result in frame.DiscreteGestureResults)
        {
            Gesture gesture = result.Key;
            DiscreteGestureResult gestureResult = result.Value;
            float confidence = gestureResult.Confidence;

            if(gesture.Name == TOP_LEFT && confidence > discreteConfidenceThreshold)
            {
                Debug.Log("TopLeft found with confidence: " + confidence);
            }

            if (gesture.Name == TOP_RIGHT && confidence > discreteConfidenceThreshold)
            {
                Debug.Log("TopRight found with confidence: " + confidence);
            }

            if (gesture.Name == BOTTOM_MID && confidence > discreteConfidenceThreshold)
            {
                Debug.Log("BottomMid found with confidence: " + confidence);
            }
        }

        //Observe all continous gestures retrieve from frame
        foreach (var result in frame.ContinuousGestureResults)
        {
            Gesture gesture = result.Key;
            ContinuousGestureResult gestureResult = result.Value;
            float progress = gestureResult.Progress;
            progressDisplay.text = "Progress of Symbol01: " + progress;
        }
    }
}
