using UnityEngine;
/// <summary>
/// OnGUI
/// </summary>
[AddComponentMenu("StrayFog/Game/SimulateMonoBehaviour/SimulateMonoBehaviour_OnGUI")]
public sealed class SimulateMonoBehaviour_OnGUI : AbsSimulateMonoBehaviourMethod
{
	/// <summary>
    /// 方法分类
    /// </summary>
    public override int methodClassify { get { return 1087004320; } }

	/// <summary>
    /// OnGUI
    /// </summary>
    void OnGUI()
    {
        simulateMonoBehaviour.OnGUI();
    }
}