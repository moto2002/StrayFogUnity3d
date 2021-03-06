﻿#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 宏定义符号
/// </summary>
public class EditorUtility_MacroDefineSymbol : AbsEditorSingle
{
    #region EditorGUILayout_DrawMacroDefineSymbol 绘制脚本宏定义符
    /// <summary>
    /// 绘制脚本宏定义符
    /// </summary>
    /// <param name="_macroDefineSymbol">宏定义符</param>
    public void EditorGUILayout_DrawMacroDefineSymbol(Dictionary<int, EditorMacroDefineSymbol> _macroDefineSymbol)
    {
        foreach (KeyValuePair<int, EditorMacroDefineSymbol> macro in _macroDefineSymbol)
        {
            EditorGUILayout.LabelField(string.Format("【{0}】{1}", macro.Value.type.Name, macro.Value.alias.alias, GUILayout.ExpandWidth(false)));
            foreach (EditorMacroDefineSymbol_Item define in macro.Value.defineMaping.Values)
            {
                EditorGUILayout.BeginHorizontal();
                define.isChecked = EditorGUILayout.ToggleLeft(
                    string.Format("{0}【{1}】", define.name, define.alias.alias), define.isChecked);
                if (GUILayout.Button(string.Format("Copy 【{0}】Define", define.alias.alias, GUILayout.ExpandWidth(false))))
                {
                    EditorStrayFogApplication.CopyToClipboard(define.name);
                    EditorUtility.DisplayDialog("Copy Define", "Copy Define " + define.name, "OK");
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorStrayFogUtility.guiLayout.DrawSeparator();
        }
    }
    #endregion

    #region EditorGUILayout_DrawMacroDefineSymbolShortcut 绘制脚本宏定义符快捷菜单
    /// <summary>
    /// 快捷分类映射
    /// </summary>
    readonly Dictionary<enEditorMacroScriptingDefineSymbolShortcutClassify, AliasTooltipAttribute> mShortcutClassifyMaping = typeof(enEditorMacroScriptingDefineSymbolShortcutClassify).EnumToAttribute<enEditorMacroScriptingDefineSymbolShortcutClassify,AliasTooltipAttribute>();
    /// <summary>
    /// 绘制脚本宏定义符快捷菜单
    /// </summary>
    /// <param name="_macroDefineSymbol">宏定义符</param>
    public void EditorGUILayout_DrawMacroDefineSymbolShortcut(Dictionary<int, EditorMacroDefineSymbol> _macroDefineSymbol)
    {
        EditorGUILayout.HelpBox("Macro Define Symbol Shortcut Menu", MessageType.Info);
        foreach (KeyValuePair<enEditorMacroScriptingDefineSymbolShortcutClassify, AliasTooltipAttribute> key in mShortcutClassifyMaping)
        {
            if (GUILayout.Button(key.Value.alias))
            {
                SaveMacroDefineScriptingDefineSymbolsForShortcut(key.Key, _macroDefineSymbol);
            }
        }

        EditorStrayFogUtility.guiLayout.DrawSeparator();
        EditorGUILayout.HelpBox("Macro Define Symbols", MessageType.Info);
        StringBuilder sbSymbol = new StringBuilder();
        foreach (KeyValuePair<int, EditorMacroDefineSymbol> macro in _macroDefineSymbol)
        {            
            foreach (EditorMacroDefineSymbol_Item define in macro.Value.defineMaping.Values)
            {
                if (define.isPlayerSettingsChecked)
                {
                    sbSymbol.AppendLine(define.alias.alias);
                }
            }
        }
        GUI.enabled = false;
        EditorGUILayout.TextArea(sbSymbol.ToString());
        GUI.enabled = true;
    }
    #endregion

    #region LoadMacroDefineScriptingDefineSymbols 加载宏定义符号
    /// <summary>
    /// 加载宏定义符号
    /// </summary>
    /// <returns>核心宏定义符号</returns>
    public Dictionary<int, EditorMacroDefineSymbol> LoadMacroDefineScriptingDefineSymbols()
    {
        Dictionary<int, EditorMacroDefineSymbol> result = new Dictionary<int, EditorMacroDefineSymbol>();
        string[] defines = EditorStrayFogApplication.GetScriptingDefineSymbolsForGroup();
        Type define = typeof(EditorMacroDefineScriptingDefineSymbols);
        Type[] types = define.GetNestedTypes();
        if (types != null && types.Length > 0)
        {
            foreach (Type t in types)
            {
                EditorMacroDefineSymbol symbol = new EditorMacroDefineSymbol(t);
                if (!result.ContainsKey(symbol.key))
                {
                    result.Add(symbol.key, symbol);
                }
                symbol.SetCheckedFromPlayerSettings(defines);
            }
        }
        return result;
    }
    #endregion

    #region SaveMacroDefineScriptingDefineSymbols 保存宏定义符号
    /// <summary>
    /// 保存宏定义符号
    /// </summary>
    /// <param name="_symbols">宏定义符号</param>
    public void SaveMacroDefineScriptingDefineSymbols(Dictionary<int, EditorMacroDefineSymbol> _symbols)
    {
        List<string> saveDefines = new List<string>();
        List<string> removeDefines = new List<string>();        
        foreach (EditorMacroDefineSymbol key in _symbols.Values)
        {
            foreach (EditorMacroDefineSymbol_Item d in key.defineMaping.Values)
            {
                if (d.isChecked)
                {
                    if (!saveDefines.Contains(d.name))
                    {
                        saveDefines.Add(d.name);
                    }
                }
                else
                {
                    if (!removeDefines.Contains(d.name))
                    {
                        removeDefines.Add(d.name);
                    }
                }
            }
        }
        EditorStrayFogApplication.RemoveScriptingDefineSymbol(removeDefines.ToArray());
        EditorStrayFogApplication.AddScriptingDefineSymbol(saveDefines.ToArray());
    }
    #endregion

    #region SaveMacroDefineScriptingDefineSymbolsForShortcut 保存指定快捷分类的宏定义符号
    /// <summary>
    /// 保存指定快捷分类的宏定义符号
    /// </summary>
    /// <param name="_shortcutClassify">快捷分类</param>
    /// <param name="_symbols">宏定义符号</param>
    public void SaveMacroDefineScriptingDefineSymbolsForShortcut(
        enEditorMacroScriptingDefineSymbolShortcutClassify _shortcutClassify,
        Dictionary<int, EditorMacroDefineSymbol> _symbols)
    {        
        foreach (EditorMacroDefineSymbol key in _symbols.Values)
        {
            foreach (EditorMacroDefineSymbol_Item d in key.defineMaping.Values)
            {
                d.isChecked = d.IsShortcut(_shortcutClassify);
            }
        }
        SaveMacroDefineScriptingDefineSymbols(_symbols);
    }
    #endregion
}
#endif
