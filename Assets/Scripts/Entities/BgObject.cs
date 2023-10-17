using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgObject : MonoBehaviour, IBgObject
{
    public float Depth { get; }
    public GameObject GameObject { get; }
    public Collider2D Collider2D { get; }

    public BgObject(GameObject gameObject, float depth)
    {
        GameObject = gameObject;
        Collider2D = gameObject.GetComponent<Collider2D>();
        Depth = depth;
    }

    public void Move(Vector2 direction)
    {
        // 삼각함수를 이용해 depth에 따른 이동속도를 구한다.
        float speed = Mathf.Sin(Depth * Mathf.PI / 2) * PlayerData.Instance.Speed;
        GameObject.transform.position += new Vector3(direction.x * speed, direction.y * speed, 0);
    }

    public void ResetPosition()
    {
        GameObject.transform.position = new Vector3(0, 0, Depth);
    }
}
