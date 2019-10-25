﻿using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 引导参考对象UI窗口控件
/// </summary>
public class UserGuideReferObject_Refer2DType_UIWindowControl_Command : AbsGuideSubCommand_ReferObject
{
    /// <summary>
    /// 窗口名称
    /// </summary>
    public string windowName { get; private set; }
    /// <summary>
    /// 控件名称
    /// </summary>
    public string controlName { get; private set; }
    /// <summary>
    /// Graphic遮罩名称
    /// </summary>
    public string graphicMask { get; private set; }
    /// <summary>
    /// 索引
    /// </summary>
    public int index { get; private set; }
    /// <summary>
    /// 遮罩控件
    /// </summary>
    AbsUIGuideGraphic mGraphicMask = null;
    /// <summary>
    /// 遮罩控件ActiveSelf
    /// </summary>
    bool mGraphicMaskActiveSelf = false;
    /// <summary>
    /// 解析配置
    /// </summary>
    /// <param name="_config">配置</param>    
    /// <param name="_index">数据索引</param>
    /// <param name="_resolveStatus">解析状态</param>
    /// <param name="_status">引导状态</param>
    /// <returns>命令集</returns>
    protected override List<AbsGuideResolveMatch> OnResolveConfig(XLS_Config_Table_UserGuideReferObject _config,int _index, enGuideStatus _resolveStatus, enGuideStatus _status)
    {
        base.OnResolveConfig(_config, _index, _resolveStatus, _status);
        index = _index;
        string[] values = OnSegmentationGroup(_config.refer2DValue);
        windowName = values[0];
        controlName = graphicMask = values[1];
        if (values.Length >= 3)
        {
            graphicMask = values[2];
        }
        switch (_status)
        {
            case enGuideStatus.WaitTrigger:
                mGraphicMaskActiveSelf = Convert.ToBoolean(byte.Parse(guideConfig.triggerConditionValue[_index]));
                break;
        }
        return null;
    }

    /// <summary>
    /// 解析参考对象
    /// </summary>
    /// <returns>参考对象</returns>
    protected override enUserGuideReferObject_ReferType OnResolveReferObject()
    {
        return enUserGuideReferObject_ReferType.Refer2D;
    }

    /// <summary>
    /// 是否满足条件
    /// </summary>
    /// <param name="_parameters">参数</param>
    /// <param name="_conditionResults">条件结果</param>
    /// <param name="_status">当前引导状态</param>
    /// <returns>true:满足条件,false:不满足条件</returns>
    protected override bool OnIsMatchCondition(enGuideStatus _status, List<bool> _conditionResults, params object[] _parameters)
    {
        bool result = base.OnIsMatchCondition(_status, _conditionResults, _parameters);
        if (_parameters != null && mGraphicMask == null)
        {
            foreach (object p in _parameters)
            {
                if (p is AbsUIWindowView)
                {
                    AbsUIWindowView w = (AbsUIWindowView)p;
                    if (w.config.name.Equals(windowName))
                    {
                        UIBehaviour behaviour = w.FindCtrlByNameIsSelfOrParent<UIBehaviour>(controlName);
                        mGraphicMask = new UIGuideGraphic((int)_status, w.FindCtrlByNameIsSelfOrParent<Graphic>(graphicMask), index);
                        result = mGraphicMask.graphic != null && mGraphicMask.graphic.gameObject.activeSelf == mGraphicMaskActiveSelf;                        
                        break;
                    }
                }
            }
        }
        return result;
    }

    /// <summary>
    /// 执行处理
    /// </summary>
    /// <param name="_status">当前引导状态</param>
    /// <param name="_parameters">参数</param>
    protected override void OnExcute(enGuideStatus _status, params object[] _parameters)
    {
        base.OnExcute(_status, _parameters);
        if (_parameters != null)
        {
            foreach (AbsUIWindowView w in _parameters)
            {
                if (w is AbsUIGuideWindowView)
                {
                    ((AbsUIGuideWindowView)w).AddTrigger(mGraphicMask);
                }
            }
        }
    }

    /// <summary>
    /// 回收
    /// </summary>
    protected override void OnRecycle()
    {
        mGraphicMask = null;
        base.OnRecycle();
    }
}
