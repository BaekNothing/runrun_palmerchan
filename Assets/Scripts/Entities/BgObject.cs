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
    public GameObject GameObject => gameObject;
    public Transform ResetPoint { get; set; }

    public void Init(float depth, Transform resetPoint)
    {
        _depth = depth;
        ResetPoint = resetPoint;

        _spriteRenderer.sortingOrder = (int)(-depth * 100);
        _spriteRenderer.material.color = new Color(1, 1, 1, depth);
        transform.localScale = new Vector3(0.8f + (1 - depth) * 3, 0.8f + (1 - depth) * 3, 1);

        ResetPosition();
    }

    public void Move(Vector2 direction)
    {
        // 삼각함수를 이용해 depth에 따른 이동속도를 구한다.

        float speed = Mathf.Sin(_depth * Mathf.PI / 2) * GameData.Instance.Speed;

        gameObject.transform.position += new Vector3(direction.x * speed, direction.y * speed, 0) * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Finish")
        {
            ResetPosition();
            Utility.Log("ResetPosition", Utility.LogLevel.Verbose);
        }
    }

    public void ResetPosition()
    {
        var delayOffset = UnityEngine.Random.Range(0.0f, 120f);
        gameObject.transform.position = ResetPoint.position + new Vector3(delayOffset, 0, _depth);
    }
}
