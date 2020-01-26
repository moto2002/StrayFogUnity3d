﻿using UnityEngine;
/// <summary>
/// 模拟MonoBehaviour方法抽象
/// </summary>
public abstract class AbsSimulateMonoBehaviourMethod : MonoBehaviour,ISimulateBehaviourMethod
{
    /// <summary>
    /// 方法分类
    /// </summary>
    public abstract int methodClassify { get; }
}