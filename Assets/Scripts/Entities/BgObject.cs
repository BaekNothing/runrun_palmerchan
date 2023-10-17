using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgObject : MonoBehaviour, IBgObject
{
    [SerializeField] float _depth;
    [SerializeField] Collider2D _collider2D;
    [SerializeField] SpriteRenderer _spriteRenderer;

    public float Depth => _depth;
    public Collider2D Collider2D => _collider2D;
    public Transform ResetPoint { get; set; }

    public void Init(float depth, Transform resetPoint)
    {
        _depth = depth;
        ResetPoint = resetPoint;

        _spriteRenderer.sortingOrder = (int)(-depth * 100);
        _spriteRenderer.material.color = new Color(1, 1, 1, depth);
    }

    public void Move(Vector2 direction)
    {
        // 삼각함수를 이용해 depth에 따른 이동속도를 구한다.
        float speed = Mathf.Sin(_depth * Mathf.PI / 2) * PlayerData.Instance.Speed;
        gameObject.transform.position += new Vector3(direction.x * speed, direction.y * speed, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Finish")
        {
            ResetPosition();
            Debug.Log("Reset");
        }
        else
        {
            Debug.Log($"Collision with {other.gameObject.name}");
        }
    }

    public void ResetPosition()
    {
        gameObject.transform.position = ResetPoint.position;
    }
}
