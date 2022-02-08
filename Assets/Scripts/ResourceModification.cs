
using System;
using System.Security.AccessControl;

[Serializable]
public class ResourceModification
{
    public int Count;
    public ResourceType ResourceType;
}

public enum ResourceType
{ 
    Gold,
    None

}
