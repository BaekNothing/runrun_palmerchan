using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System;

[Serializable]
public class SpineController
{
    [SerializeField] SkeletonAnimation _skeletonAnimation;
    public SpineController(GameObject parentObject)
    {
        _skeletonAnimation = parentObject.GetComponent<SkeletonAnimation>();
    }

    public void SetAnimation(string animationName, bool loop)
    {
        _skeletonAnimation.loop = loop;
        SetTimeScale(GameData.Instance.PlayerAnimationTimeScaleMin);
        _skeletonAnimation.AnimationName = animationName;
    }

    public void SetUnScaledTime(bool isUnScaledTime)
    {
        _skeletonAnimation.UnscaledTime = isUnScaledTime;
    }

    public void SetTimeScale(float timeScale)
    {
        _skeletonAnimation.timeScale = timeScale;
    }
}
