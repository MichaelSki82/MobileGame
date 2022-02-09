using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRendererView : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trailRendererPrefab;

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

}
