using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class BodySourceManager : MonoBehaviour 
{
    private KinectSensor _Sensor;
    private BodyFrameReader _Reader;
    private Body[] _Data = null;
    private ulong _trackingId = 0;

    public SymbolOneDetector gestureDetector;
    

    public Body[] GetData()
    {
        return _Data;
    }

    void Start () 
    {
        _Sensor = KinectSensor.GetDefault();

        if (_Sensor != null)
        {
            _Reader = _Sensor.BodyFrameSource.OpenReader();
            
            if (!_Sensor.IsOpen)
            {
                _Sensor.Open();
            }
        }   
    }
    
    void Update () 
    {
        if (_Reader != null)
        {
            var frame = _Reader.AcquireLatestFrame();
            if (frame != null)
            {
                if (_Data == null)
                {
                    _Data = new Body[_Sensor.BodyFrameSource.BodyCount];
                }

                _trackingId = 0;
                frame.GetAndRefreshBodyData(_Data);
                
                frame.Dispose();
                frame = null;

                foreach(var body in _Data)
                {
                    //Look for trackable bodies
                    if(body != null && body.IsTracked)
                    {
                        _trackingId = body.TrackingId;

                        //The gesture detector that it can turn on its reader
                        //and start getting frames since there is a body that
                        //is being tracked
                        if(gestureDetector != null)
                        {
                            gestureDetector.SetTrackingId(_trackingId);
                        }

                        //We break since we care about the first available body
                        break;
                    }
                }
            }
        }    
    }
    
    void OnApplicationQuit()
    {
        if (_Reader != null)
        {
            _Reader.Dispose();
            _Reader = null;
        }
        
        if (_Sensor != null)
        {
            if (_Sensor.IsOpen)
            {
                _Sensor.Close();
            }
            
            _Sensor = null;
        }
    }
}
