using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBgObject
{
    public float Depth { get; }
    public Collider2D Collider2D { get; }
    public Transform ResetPoint { get; set; }
    public void Move(Vector2 direction);
}
