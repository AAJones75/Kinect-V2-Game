using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SymbolManager : MonoBehaviour {

    public GameManager _manager;
    public GestureMeterBehavior _gestureMeter;

    public SymbolNode currentNode;
    public List<GameObject> abstractSymbols = new List<GameObject>();
    public List<string[]> concreteSymbols = new List<string[]>();
    public List<string> symbols = new List<string>();
    public string inputSymbol = "";
    public GameObject symbolInterface;
    public List<SymbolNodeBehavior> buttons;

    // Use this for initialization
    void Start () {

        Transform interfaceTransform = symbolInterface.transform.GetChild(1);

        //Get the symbol node behavior component of all buttons that represent nodes
        for(int l = 0; l < interfaceTransform.childCount; l++)
        {
            //Debug.Log(interfaceTransform.GetChild(l).tag);
            buttons.Add(interfaceTransform.GetChild(l).GetComponent<SymbolNodeBehavior>());
        }

        //Debug.Log("Num Buttons Found: " + buttons.Count);

        //Get all the symbol game objects, should be the children of this manager object
        for(int i = 0; i < transform.childCount; i++)
        {
            abstractSymbols.Add(transform.GetChild(i).gameObject);
        }

        //Get node ordering for each of the symbol game objects
        foreach(var symbol in abstractSymbols)
        {
            concreteSymbols.Add(symbol.GetComponent<Symbol>().symbol);
        }

        //Create symbols for comparision of user input
        GenerateComparisonSymbols();
	}
	
	// Update is called once per frame
	void Update () {
        if (!_manager.isPaused)
        {
            //Generate symbol from user input
            inputSymbol = GenerateInputSymbol().ToString();
            //Debug.Log(inputSymbol);
            //Check to see if the user input is equal to some symbol
            CompareSymbolSets(); 
        }
	}

    private void CompareSymbolSets()
    {
        //Compare input to the list of know symbols
        foreach(var symbol in symbols)
        {
            //Debug.Log(inputSymbol);
            //If a button wasn't selected this will be null
            if (inputSymbol != null)
            {
                //Debug.Log("Input Symbol: " + inputSymbol);
                //Debug.Log("Comparison Symbol: " + symbol);

                //Check to see if the user entered a correct symbol
                if (inputSymbol.Equals(symbol))
                {
                    //Debug.Log("The user entered a correct combination.");
                    foreach(var abstraction in abstractSymbols)
                    {
                        //Construct the comparison string of a symbol from the associated gameobject
                        Symbol _symbol = abstraction.GetComponent<Symbol>();
                        string[] symbolArray = _symbol.symbol;
                        string symbolPath = symbolArray[0];
                        
                        for(int i = 1; i < symbolArray.Length; i++)
                        {
                            symbolPath += "-->" + symbolArray[i];
                        }
                        
                        //Determine which symbol game object to be used after user entered the correct symbol
                        if(symbol.Equals(symbolPath) && _gestureMeter.isMeterAvailable)
                        {
                            //Debug.Log("Successfully constructed symbol from gameobject inputs");
                            //Debug.Log("Path: " + symbolPath);
                            //Debug.Log("Actual Symbol: " + symbol);
                            _symbol.item.SetActive(true);
                           _gestureMeter.DecrementMeter(_symbol.itemCost);
                            symbolInterface.SetActive(false);
                            currentNode = null;
                            inputSymbol = "";
                            
                            foreach(var button in buttons)
                            {
                                button.Reset();
                            }
                        }
                    }
                } 
            }
        }

    }

    private void GenerateComparisonSymbols()
    {
        foreach(var symbol in concreteSymbols)
        {
            SymbolNode root = new SymbolNode();
            root.Id = symbol[0];
            SymbolNode current = root;

            for(int i = 1; i < symbol.Length; i++)
            {
                SymbolNode next = new SymbolNode();
                next.Id = symbol[i];
                current.Next = next;
                current = next;
            }

            symbols.Add(root.ToString());
            //Debug.Log("Symbol: " + root);
        }
    }

    private SymbolNode GenerateInputSymbol()
    {
        SymbolNode lastNode = currentNode;
        SymbolNode rootNode = new SymbolNode();

        //Traverse backwards, start from current node until
        //the root node is found
        while(lastNode != null)
        {
            if (lastNode.Prev == null)
            {
                rootNode = lastNode;
            }

            lastNode = lastNode.Prev;
        }

        //Debug.Log(rootNode);
        return rootNode;  
    }
}
