﻿using System;
using UnityEngine;
/// <summary>
/// 窗口视图
/// </summary>
public abstract class AbsUIWindowView : AbsMonoBehaviour
{
    #region config 窗口配置
    /// <summary>
    /// 窗口配置
    /// </summary>
    public XLS_Config_Table_UIWindowSetting config { get; private set; }
    #endregion

    #region SetConfig 设置设定
    /// <summary>
    /// 设置设定
    /// </summary>
    /// <param name="_cfg">设定</param>
    public void SetConfig(XLS_Config_Table_UIWindowSetting _cfg)
    {
        config = _cfg;
    }
    #endregion

    #region ToggleActive 切换激活状态
    /// <summary>
    /// 是否是第一次显示
    /// </summary>
    bool mIsFirstDisplay = true;
    /// <summary>
    /// 切换激活状态
    /// </summary>
    /// <param name="_isActive">是否激活</param>
    public void ToggleActive(bool _isActive)
    {
        if (gameObject.activeSelf != _isActive)
        {
            if (_isActive)
            {
                if (mIsFirstDisplay)
                {
                    mIsFirstDisplay = false;
                    OnInitialize();
                }
                OnBeforeDisplay();
                OnToggleActive(_isActive);
                OnAfterDisplay();
            }
            else
            {
                OnBeforeHidden();
                OnToggleActive(_isActive);
                OnAfterHidden();
            }
        }
    }

    /// <summary>
    /// 切换激活状态
    /// </summary>
    /// <param name="_isActive">是否激活</param>
    void OnToggleActive(bool _isActive)
    {
        rectTransform.gameObject.SetActive(_isActive);    
    }

    #region protected virtual methods
    /// <summary>
    /// 窗口初始化
    /// </summary>
    protected virtual void OnInitialize() { }
    /// <summary>
    /// 显示窗口之前
    /// </summary>
    protected virtual void OnBeforeDisplay() { }
    /// <summary>
    /// 显示窗口之后
    /// </summary>
    protected virtual void OnAfterDisplay() { }
    /// <summary>
    /// 隐藏窗口之前
    /// </summary>
    protected virtual void OnBeforeHidden() { }
    /// <summary>
    /// 隐藏窗口之后
    /// </summary>
    protected virtual void OnAfterHidden() { }
    #endregion
    #endregion

    #region CloseWindow
    /// <summary>
    /// 关闭窗口事件
    /// </summary>
    public event Action<AbsUIWindowView> OnCloseWindow;
    /// <summary>
    /// 关闭窗口
    /// </summary>
    public void CloseWindow()
    {
        OnCloseWindow?.Invoke(this);
    }
    #endregion
}