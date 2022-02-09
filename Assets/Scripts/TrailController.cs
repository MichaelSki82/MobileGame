using JoostenProductions;
using UnityEngine;
using System;

public class TrailController:BaseController
{
    private TrailRendererView _trailView;

    public TrailController(TrailRendererView trailView)
    {
        _trailView = trailView;
    }

    public void SetPosition(Vector3 position)
    {
        position = new Vector3(position.x, position.y, 0);
        _trailView.SetPosition(position);
    }

    protected override void OnDispose()
    {
        //GameObject.Destroy(_trailView.gameObject);
        base.OnDispose();

    }

}
