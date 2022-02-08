using JoostenProductions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuInputController:BaseController
{
    public event Action<Vector3> MousePosition;
    
    public void OnUpdate()
    {

        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePosition?.Invoke(position);
       
    }
    public MainMenuInputController()
    {
        UpdateManager.SubscribeToUpdate(OnUpdate);
    }

    protected override void OnDispose()
    {
        UpdateManager.UnsubscribeFromUpdate(OnUpdate);
        base.OnDispose();

    }
}

