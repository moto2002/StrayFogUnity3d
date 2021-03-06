using UnityEngine;
/// <summary>
/// OnApplicationFocus
/// </summary>
[AddComponentMenu("StrayFog/Game/SimulateMonoBehaviour/SimulateMonoBehaviour_OnApplicationFocus")]
public sealed class SimulateMonoBehaviour_OnApplicationFocus : AbsSimulateMonoBehaviourMethod
{
	/// <summary>
    /// 方法分类
    /// </summary>
    public override int methodClassify { get { return -1366296050; } }

	/// <summary>
    /// OnApplicationFocus
    /// </summary>
    void OnApplicationFocus(System.Boolean _focus)
    {
        simulateMonoBehaviour.OnApplicationFocus(_focus);
    }
}