﻿#if UNITY_EDITOR
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 构建模拟Behaviour事件组件
/// </summary>
public class EditorWindowBuildSimulateBehaviourMethod : AbsEditorWindow
{
    /// <summary>
    /// MonoBehaviour滚动视图位置
    /// </summary>
    Vector2 mMonoBehaviourScrollViewPosition = Vector2.zero;

    /// <summary>
    /// UIBehaviour滚动视图位置
    /// </summary>
    Vector2 mUIBehaviourScrollViewPosition = Vector2.zero;

    /// <summary>
    /// 模拟MonoBehaviour方法
    /// </summary>
    List<EditorSimulateBehaviourMethod> mSimulateMonoBehaviourMethodMaping = null;

    /// <summary>
    /// 模拟MonoBehaviour方法
    /// </summary>
    List<EditorSimulateBehaviourMethod> mSimulateUIBehaviourMethodMaping = null;

    /// <summary>
    /// OnFocus
    /// </summary>
    void OnFocus()
    {
        mSimulateMonoBehaviourMethodMaping = EditorStrayFogExecute.CollectSimulateMonoBehaviourMethods();
        mSimulateUIBehaviourMethodMaping = EditorStrayFogExecute.CollectSimulateUIBehaviourMethods();
    }

    /// <summary>
    /// OnGUI
    /// </summary>
    void OnGUI()
    {
        DrawBrower();
        DrawAssetNodes();
    }

    #region DrawBrower
    /// <summary>
    /// DrawBrower
    /// </summary>
    void DrawBrower()
    {
        
    }
    #endregion

    #region DrawAssetNodes
    /// <summary>
    /// DrawAssetNodes
    /// </summary>
    private void DrawAssetNodes()
    {
        EditorGUILayout.BeginHorizontal();
        mMonoBehaviourScrollViewPosition = OnViewMethod("Mapper to MonoBehaviour", mMonoBehaviourScrollViewPosition, mSimulateMonoBehaviourMethodMaping);
        mUIBehaviourScrollViewPosition = OnViewMethod("Mapper to UIBehaviour", mUIBehaviourScrollViewPosition, mSimulateUIBehaviourMethodMaping);
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Build SimulateBehaviour"))
        {
            EditorStrayFogExecute.ExecuteBuildSimulateBehaviour();
        }
    }
    #endregion

    #region 显示方法视图
    /// <summary>
    /// 显示方法视图
    /// </summary>
    /// <param name="_title">标题</param>
    /// <param name="_scrollViewPosition">滚动视图位置</param>
    /// <param name="_methods">方法组</param>
    /// <returns>滚动视图位置</returns>
    Vector2 OnViewMethod(string _title,Vector2 _scrollViewPosition, List<EditorSimulateBehaviourMethod> _methods)
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField(_title);
        if (_methods != null)
        {
            _scrollViewPosition = EditorGUILayout.BeginScrollView(_scrollViewPosition);
            int index = 0;
            int count = _methods.Count;
            foreach (EditorSimulateBehaviourMethod key in _methods)
            {
                index++;
                if (key.isBuildSimulate)
                {
                    EditorGUILayout.LabelField(string.Format("{0}.【{1}】",
                    index.PadLeft(count), key.methodInfo.Name));
                }
                else
                {
                    EditorGUILayout.LabelField(string.Format("{0}.【{1}】【Not Build Simulate】",
                    index.PadLeft(count), key.methodInfo.Name));
                }
            }
            EditorGUILayout.EndScrollView();
        }
        EditorGUILayout.EndVertical();
        return _scrollViewPosition;
    }
    #endregion
}
#endif