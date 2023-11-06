using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObject : AbstractObject
{
    public override void Init(float depth)
    {
        _depth = depth;
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
        else if (other.gameObject.tag == "Player")
        {
            Utility.Log("Player Hit", Utility.LogLevel.Verbose);
        }
    }

    public override void SetPositionOffset()
    {
        var delayOffset = Random.Range(0.0f, 120f);
        gameObject.transform.position += new Vector3(delayOffset, 0, 0);
    }

}
