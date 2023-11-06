using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractObject : MonoBehaviour
{
    [SerializeField] protected float _depth;
    [SerializeField] protected Collider2D _collider2D;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    public abstract void Init(float depth);
    public abstract void Move(Vector2 direction);
    public void Reset()
    {
        ResetPosition();
        ResetAction();
    }
    void ResetPosition()
    {
        gameObject.transform.position = ObjectData.Instance.ResetPosition + new Vector3(0, 0, _depth);
        SetPositionOffset();
    }
    public abstract void SetPositionOffset();
    public virtual void ResetAction() { }
}
