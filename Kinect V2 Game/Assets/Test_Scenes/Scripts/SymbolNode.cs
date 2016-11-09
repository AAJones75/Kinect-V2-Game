
using System.Collections;

public class SymbolNode {

    public string Id
    {
        get;
        set;
    }

    public SymbolNode Next
    {
        get;
        set;
    }

    public SymbolNode Prev
    {
        get;
        set;
    }

    public override string ToString()
    {
        //Show node and all following nodes
        string symbol = Id;
        SymbolNode nextNode = Next;

        while (nextNode != null)
        {
            symbol += "-->" + nextNode.Id;
            nextNode = nextNode.Next;
        }

        return symbol;
    }
}
