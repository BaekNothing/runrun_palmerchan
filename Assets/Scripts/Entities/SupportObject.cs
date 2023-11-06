using System.Collections;
using System.Collections.Generic;
using System.Security;
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

    public override void Init(float depth)
    {
        _depth = depth;
        this.gameObject.SetActive(false);
        Reset();
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
            Reset();
            gameObject.SetActive(false);
        }
    }

    public override void SetPositionOffset()
    {
        var delayOffset = UnityEngine.Random.Range(
            ObjectData.Instance.SupportRandomRange.x, ObjectData.Instance.SupportRandomRange.y);
        gameObject.transform.position += new Vector3(delayOffset, 0, 0);
    }

    public override void ResetAction()
    {
        base.ResetAction();

        _supportType = ObjectData.Instance.CheckDashObject() ? SupportType.Hyper : SupportType.Common;
        _spriteRenderer.color = _supportType == SupportType.Hyper ? Color.blue : Color.yellow;
    }
}
