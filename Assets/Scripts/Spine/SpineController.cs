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
        _skeletonAnimation.AnimationName = animationName;
        _skeletonAnimation.loop = loop;
    }
}
