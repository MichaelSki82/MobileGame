using System;
using System.Collections.Generic;
using UI;

public interface IInventoryView:IView
{
    //event EventHandler<IItem> Selected;
    //event EventHandler<IItem> Deselected;

    void Display(IReadOnlyList<IItem> items);
}
