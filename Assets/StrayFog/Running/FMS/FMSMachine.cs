﻿using UnityEngine;
/// <summary>
/// FMS状态机接口
/// </summary>
public class FMSMachine : AbsMonoBehaviour
{
    #region Animator 读写Animator
    /// <summary>
    /// 阿凡达
    /// </summary>
    public Animator animator { get { return mAnimator; } }
    /// <summary>
    /// 阿凡达
    /// </summary>
    Animator mAnimator = null;
    /// <summary>
    /// 设置阿凡达
    /// </summary>
    /// <param name="_animator">阿凡达</param>
    public void SetAnimator(Animator _animator)
    {
        mAnimator = _animator;
    }
    #endregion

    #region IsMachine 是否是指定状态机
    /// <summary>
    /// 是否是指定状态机
    /// </summary>
    /// <param name="_machine">状态机</param>
    /// <returns>true:是,false:否</returns>
    public bool IsMachine(enFMSMachine _machine)
    {
        return IsMachine((int)_machine);
    }
    /// <summary>
    /// 是否是指定状态机
    /// </summary>
    /// <param name="_machineNameHash">状态机NameHash值</param>
    /// <returns>true:是,false:否</returns>
    public bool IsMachine(int _machineNameHash)
    {
        return FMSMachineMaping.IsMachine(mAnimator, _machineNameHash);
    }
    #endregion

    #region IsState 是否是指定状态
    /// <summary>
    /// 是否是指定状态
    /// </summary>
    /// <param name="_state">状态</param>
    /// <returns>true:是,false:否</returns>
    public bool IsState(enFMSState _state)
    {
        return IsState((int)_state);
    }
    /// <summary>
    /// 是否是指定状态
    /// </summary>
    /// <param name="_state">状态NameHash值</param>
    /// <returns>true:是,false:否</returns>
    public bool IsState(int _stateNameHash)
    {
        return FMSMachineMaping.IsState(mAnimator, _stateNameHash);
    }
    #endregion

    #region CrossFade 淡入淡出   
    /// <summary>
    /// 淡入淡出
    /// </summary>
    /// <param name="_state">状态</param>
    public void CrossFade(enFMSState _state)
    {
        CrossFade(_state, Time.fixedDeltaTime);
    }
    /// <summary>
    /// 淡入淡出Attack1_1
    /// </summary>
    /// <param name="_state">状态</param>
    /// <param name="_transitionDuration">转换时间</param>
    public void CrossFade(enFMSState _state, float _transitionDuration)
    {
        CrossFade((int)_state, _transitionDuration);
    }
    /// <summary>
    /// 淡入淡出
    /// </summary>
    /// <param name="_stateNameHash">状态值</param>
    public void CrossFade(int _stateNameHash)
    {
        CrossFade(_stateNameHash, Time.fixedDeltaTime);
    }
    /// <summary>
    /// 淡入淡出Attack1_1
    /// </summary>
    /// <param name="_stateNameHash">状态NameHash值</param>
    /// <param name="_transitionDuration">转换时间</param>
    public void CrossFade(int _stateNameHash, float _transitionDuration)
    {
        mAnimator.CrossFade(_stateNameHash, Mathf.Clamp(_transitionDuration, 0, float.MaxValue));
    }
    #endregion

    #region SetInteger
    /// <summary>
    /// SetInteger
    /// </summary>
    /// <param name="_parameter">参数</param>
    /// <param name="_value">值</param>
    public void SetInteger(enFMSParameter _parameter, int _value)
    {
        mAnimator.SetInteger((int)_parameter, _value);
    }
    #endregion

    #region SetFloat
    /// <summary>
    /// SetFloat
    /// </summary>
    /// <param name="_parameter">参数</param>
    /// <param name="_value">值</param>
    public void SetFloat(enFMSParameter _parameter, float _value)
    {
        mAnimator.SetFloat((int)_parameter, _value);
    }
    #endregion

    #region SetBool
    /// <summary>
    /// SetBool
    /// </summary>
    /// <param name="_parameter">参数</param>
    /// <param name="_value">值</param>
    public void SetBool(enFMSParameter _parameter, bool _value)
    {
        mAnimator.SetBool((int)_parameter, _value);
    }
    #endregion

    #region SetTrigger
    /// <summary>
    /// SetTrigger
    /// </summary>
    /// <param name="_parameter">参数</param>
    /// <param name="_value">值</param>
    public void SetTrigger(enFMSParameter _parameter, bool _value)
    {
        if (_value)
        {
            mAnimator.SetTrigger((int)_parameter);
        }
        else
        {
            mAnimator.ResetTrigger((int)_parameter);
        }
    }
    #endregion
}