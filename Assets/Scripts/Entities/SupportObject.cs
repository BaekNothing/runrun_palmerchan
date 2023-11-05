using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportObject : AbstractObject
{
    [SerializeField] SupportType _supportType = 0;
    public SupportType GetSupportType() => _supportType;

    public enum SupportType
    {
        Common,
        Hyper
    }

    public override void Init(float depth, Transform resetPoint)
    {
        _depth = depth;
        _resetPoint = resetPoint;
        ResetPosition();
    }

    public override void Move(Vector2 direction)
    {
        if (!gameObject.activeSelf) return;
        // 삼각함수를 이용해 depth에 따른 이동속도를 구한다.

        float speed = Mathf.Sin(_depth * Mathf.PI / 2) * GameData.Instance.Speed;
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
        gameObject.transform.position = _resetPoint.position + new Vector3(delayOffset, 0, _depth);
        _supportType = (SupportType)UnityEngine.Random.Range(0, 2);

        _spriteRenderer.color = _supportType == SupportType.Common ? Color.yellow : Color.blue;
    }
}
