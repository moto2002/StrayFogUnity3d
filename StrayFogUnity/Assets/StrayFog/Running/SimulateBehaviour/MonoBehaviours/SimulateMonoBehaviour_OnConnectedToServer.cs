using UnityEngine;
/// <summary>
/// OnConnectedToServer
/// </summary>
[AddComponentMenu("StrayFog/Game/SimulateMonoBehaviour/SimulateMonoBehaviour_OnConnectedToServer")]
public sealed class SimulateMonoBehaviour_OnConnectedToServer : AbsSimulateMonoBehaviourMethod
{
	/// <summary>
    /// 方法分类
    /// </summary>
    public override int methodClassify { get { return -583367778; } }

	/// <summary>
    /// OnConnectedToServer
    /// </summary>
    void OnConnectedToServer()
    {
        simulateMonoBehaviour.OnConnectedToServer();
    }
}