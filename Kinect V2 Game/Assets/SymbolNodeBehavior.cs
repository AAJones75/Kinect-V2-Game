using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SymbolNodeBehavior : MonoBehaviour {

    Button button;
    ColorBlock buttonColors;
    Color normalColor;
    Color pressedColor;

    public SymbolNode representation;
    public SymbolManager manager;

    void Awake()
    {
        //Get button and associated colors
        button = gameObject.GetComponent<Button>();
        buttonColors = button.colors;
        normalColor = buttonColors.normalColor;
        pressedColor = buttonColors.pressedColor;

        //Add click event listener to button
        button.onClick.AddListener(ClickHandler);
        //Setup node representation of button
        representation = new SymbolNode();
        representation.Id = gameObject.name;
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void ClickHandler()
    {        
        buttonColors.normalColor = pressedColor;
        button.colors = buttonColors;

        //Check to see if current node is not null
        //If it is the root of the symbol is not yet determined
        if(manager.currentNode != null)
        {
            //Check to see if pressing node again
            //Flush this out later, not actually an accurate check
            if (manager.currentNode.Id != representation.Id)
            {
                representation.Prev = manager.currentNode;
                manager.currentNode.Next = representation;
            }
        }

        manager.currentNode = representation;
    }

    public void Reset()
    {
        buttonColors.normalColor = normalColor;
        button.colors = buttonColors;
        representation = new SymbolNode();
        representation.Id = gameObject.name;
    }
}
