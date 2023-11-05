using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(AbstractCutIn), true)]
public class AbstractCutInEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Position Apply"))
        {
            var cutIn = target as AbstractCutIn;
            cutIn.SetPosition();
        }
        base.OnInspectorGUI();
    }
}

#endif

public abstract class AbstractCutIn : MonoBehaviour
{
    public Vector2 StartPosition = Vector2.zero;
    public AnimationCurve Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    [SerializeField] Vector2 Direction = Vector2.zero;
    [SerializeField] protected float Duration = 0f;
    [SerializeField] protected float Wait = 0f;

    protected float Timer = 0f;

    public enum AppearType
    {
        Ease_In,
        Ease_Out,
        Linear
    }

    public void SetPosition()
    {
        transform.localPosition = StartPosition;
    }

    void OnEnable()
    {
        Init();
        CutIn();
    }

    void Init()
    {
        Timer = 0f;
    }

    public async void CutIn()
    {
        PrevAction();

        Timer = 0f;
        while (Timer < Duration)
        {
            Timer += Time.deltaTime;
            Move(CalcMove(StartPosition, Direction, Timer, Duration));
            await Task.Yield();
        }
        Move(StartPosition + Direction);

        int waitTime = (int)(Wait * 1000);
        WaitAction(waitTime);
        await Task.Delay(waitTime);

        Timer = 0f;
        while (Timer < Duration)
        {
            Timer += Time.deltaTime;
            Move(CalcMove(StartPosition, Direction, Duration - Timer, Duration));
            await Task.Yield();
        }
        Move(StartPosition);

        EndAction();
        gameObject.SetActive(false);
    }

    Vector2 CalcMove(Vector2 startPosition, Vector2 direction, float timer, float duration)
    {
        var t = CalcLerpT(timer / duration);
        return startPosition + direction * t;
    }

    float CalcLerpT(float t)
    {
        return Curve.Evaluate(t);
    }

    void Move(Vector2 endPosition)
    {
        if (this && this.gameObject && transform)
            transform.localPosition = endPosition;
    }

    public virtual void PrevAction()
    {

    }

    public virtual void WaitAction(float wait)
    {

    }

    public virtual void EndAction()
    {

    }
}
