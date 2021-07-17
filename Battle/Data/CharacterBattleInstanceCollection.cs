using UnityEngine;

public class CharacterBattleInstanceCollection : MonoBehaviour
{
    public double pressTurns;
    public BattleAnimationHandler battleAnimationInstance { get; private set; } 
    public BattleMoveAnimations battleMove { get; private set; }

    private void Awake()
    {
        battleAnimationInstance = GetComponent<BattleAnimationHandler>();
        battleMove = GetComponent<BattleMoveAnimations>();
    }
}
