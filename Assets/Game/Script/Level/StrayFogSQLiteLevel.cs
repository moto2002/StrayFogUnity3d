﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
/// <summary>
/// FMS关卡
/// </summary>
[AddComponentMenu("Game/StrayFogSQLiteLevel")]
public class StrayFogSQLiteLevel : AbsLevel
{
    /// <summary>
    /// Awake
    /// </summary>
    void Awake()
    {
        StrayFogGamePools.gameManager.Initialization(() =>
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            List<XLS_Report_Table_Report> reports = StrayFogSQLiteEntityHelper.Select<XLS_Report_Table_Report>();
            watch.Stop();
            UnityEngine.Debug.LogFormat("SQLite Data=>{0} , Time=>{1}", reports.Count, watch.Elapsed);

            XLS_Report_Table_Report insertReport = new XLS_Report_Table_Report(Guid.NewGuid().ToString().GetHashCode());
            insertReport.Set_stringCol(Guid.NewGuid().ToString());
            StrayFogSQLiteEntityHelper.Insert(insertReport);

            watch.Reset();
            watch.Start();
            reports = StrayFogSQLiteEntityHelper.Select<XLS_Report_Table_Report>();
            watch.Stop();
            UnityEngine.Debug.LogFormat("SQLite Data=>{0} , Time=>{1}", reports.Count, watch.Elapsed);
        });
    }
}
