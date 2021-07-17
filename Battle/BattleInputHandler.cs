using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class BattleInputHandler : MonoBehaviour
{
    [SerializeField] GameObject targetForBroadcast;

    public void OnExecute()
    {
        targetForBroadcast.BroadcastMessage("Execute", 0, SendMessageOptions.DontRequireReceiver);
    }

    public void OnBack()
    {
        targetForBroadcast.BroadcastMessage("Back", null, SendMessageOptions.DontRequireReceiver);
    }

    public void OnUp()
    {
        targetForBroadcast.BroadcastMessage("Up", null, SendMessageOptions.DontRequireReceiver);
    }

    public void OnDown()
    {
        targetForBroadcast.BroadcastMessage("Down", null, SendMessageOptions.DontRequireReceiver);
    }
}