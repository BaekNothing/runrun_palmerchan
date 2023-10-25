using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportObject : MonoBehaviour, IBgObject
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
        ResetPosition();
    }

    public void Move(Vector2 direction)
    {
        if (!gameObject.activeSelf) return;
        // 삼각함수를 이용해 depth에 따른 이동속도를 구한다.

        float speed = Mathf.Sin(_depth * Mathf.PI / 2) * PlayerData.Instance.Speed;
        gameObject.transform.position += new Vector3(direction.x * speed, direction.y * speed, 0) * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Finish")
        {
            ResetPosition();
            gameObject.SetActive(false);
        }
    }

    public void ResetPosition()
    {
        var delayOffset = UnityEngine.Random.Range(0.0f, 120f);
        gameObject.transform.position = ResetPoint.position + new Vector3(delayOffset, 0, _depth);
    }
}
