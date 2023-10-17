using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBgObject
{
    public float Depth { get; }
    public GameObject GameObject { get; }
    public Collider2D Collider2D { get; }
    public void Move(Vector2 direction);
    public void ResetPosition();
}
