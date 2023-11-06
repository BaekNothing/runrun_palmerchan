using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgObject : AbstractObject
{
    public override void Init(float depth)
    {
        _depth = depth;

        _spriteRenderer.sortingOrder = (int)(-depth);
        _spriteRenderer.material.color = new Color(1, 1, 1, depth);
        transform.localScale = new Vector3(0.8f + (1 - depth) * 3, 0.8f + (1 - depth) * 3, 1);

        Reset();
    }

    public override void Move(Vector2 direction)
    {
        // 삼각함수를 이용해 depth에 따른 이동속도를 구한다.

        float speed = Mathf.Sin(_depth * Mathf.PI / 2) * GameData.Instance.Speed;

        gameObject.transform.position += new Vector3(direction.x * speed, direction.y * speed, 0) * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Finish")
        {
            Reset();
            Utility.Log("ResetPosition", Utility.LogLevel.Verbose);
        }
    }

    public override void SetPositionOffset()
    {
        var delayOffset = UnityEngine.Random.Range(
            ObjectData.Instance.BgObjectRandomRange.x, ObjectData.Instance.BgObjectRandomRange.y);
        gameObject.transform.position += new Vector3(delayOffset, 0, 0);
    }
}
