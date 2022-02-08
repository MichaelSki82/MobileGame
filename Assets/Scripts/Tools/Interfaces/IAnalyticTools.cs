using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnalyticTools 
{
    void SendMessage(string alias, IDictionary<string, object> eventData = null);

}
