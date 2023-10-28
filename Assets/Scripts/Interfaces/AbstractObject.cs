using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractObject : MonoBehaviour
{
    [SerializeField] protected float _depth;
    [SerializeField] protected Collider2D _collider2D;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected Transform _resetPoint;
    public abstract void Init(float depth, Transform resetPoint);
    public abstract void Move(Vector2 direction);
}
