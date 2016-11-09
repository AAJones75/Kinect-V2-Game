using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Windows.Kinect;

public class KinectUICursor : AbstractKinectUICursor
{
    public Color normalColor = new Color(1f, 1f, 1f, 0.5f);
    public Color hoverColor = new Color(1f, 1f, 1f, 1f);
    public Color clickColor = new Color(1f, 1f, 1f, 1f);
    public Vector3 clickScale = new Vector3(.8f, .8f, .8f);

    private Vector3 _initScale;

    public SymbolNode symbolRoot;
    public SymbolNode currentNode;

    public bool isRootCaptured;

    public override void Start()
    {
        base.Start();
        _initScale = transform.localScale;
        _image.color = new Color(1f, 1f, 1f, 0f);
    }

    public override void ProcessData()
    {
        // update pos
        Vector3 newPos = _data.GetHandScreenPosition();
        transform.position = new Vector3(newPos.x, newPos.y, -1f);
        //transform.position = _data.GetHandScreenPosition();
        if (_data.IsPressing)
        {
            //Get the object that was pressed
            //GameObject pressedObject = _data.HoveringObject;
            //if (pressedObject != null)
            //{
            //    //Check to see if the object was a button used for creating symbols
            //    //and that whether or not this is the first button pressed
            //    if (!isRootCaptured && pressedObject.tag == "Pressable Button")
            //    {
            //        symbolRoot = pressedObject.GetComponent<SymbolNodeBehavior>().representation;
            //        currentNode = symbolRoot;
            //        isRootCaptured = true;
            //    }
            //    else
            //    {
            //        symbolRoot.Next = pressedObject.GetComponent<SymbolNodeBehavior>().representation;
            //    }
            //}
            //Debug.Log("Definitely pressing!");
            _image.color = clickColor;
            _image.transform.localScale = clickScale;

            //Show Symbol
            /*Debug.Log(symbolRoot)*/;
            return;
        }
        if (_data.IsHovering)
        {
            _image.color = hoverColor;
        }
        else
        {
            _image.color = normalColor;
        }
        _image.transform.localScale = _initScale;
    }
}
