using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgObject : AbstractObject
{
    public Sprite[] _sprites_Near = new Sprite[0];
    public Sprite[] _sprites_Far = new Sprite[0];

    public override void Init(float depth)
    {
        _depth = 1 - depth;

        _spriteRenderer.sortingOrder = (int)(-depth) + (int)Utility.ObjectDrawOrder.Background;
        _spriteRenderer.material.color = new Color(1, 1, 1, 1 - depth * 0.5f);
        SetSprite(depth);
        transform.localScale = new Vector3(2f - depth, 2f - depth, 1);

        Reset();
    }


    public void SetSprite(float depth)
    {
        if (depth < 0.5f)
        {
            if (_sprites_Near.Length > 0)
                _spriteRenderer.sprite = _sprites_Near[Random.Range(0, _sprites_Near.Length)];
            else
                _spriteRenderer.sprite = null;
        }
        else
        {
            if (_sprites_Far.Length > 0)
                _spriteRenderer.sprite = _sprites_Far[Random.Range(0, _sprites_Far.Length)];
            else
                _spriteRenderer.sprite = null;
        }
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
