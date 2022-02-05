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

    protected override void OnDispose()
    {
        UpdateManager.UnsubscribeFromUpdate(OnUpdate);
        base.OnDispose();

    }
    public MainMenuInputController()
    {
        UpdateManager.SubscribeToUpdate(OnUpdate);
    }
}

