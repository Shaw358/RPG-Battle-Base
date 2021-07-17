using BattleEnums;
using UnityEngine;

public class PhysicalAttackState : BaseState
{
    private int currentAnim;
    CharacterBattleInstanceCollection characterInstancesCollection;
    Attack attack;

    public PhysicalAttackState(CharacterBattleInstanceCollection newCollection, Attack newAttack)
    {
        characterInstancesCollection = newCollection;
        attack = newAttack;
    }

    public override void Start()
    {
        characterInstancesCollection.battleMove.onAnimationFinished += WaitForAnimationDone;

        BattleManager.instance.SetAnimationVariables(BattleActionEnum.Walking);
        characterInstancesCollection.battleMove.SetMovementState(BattleActionEnum.Walking);
    }

    public override void Update()
    {
        if (time == 1)
        {
            timer += Time.deltaTime;
            if (timer > .25f)
            {
                DoActionEffect();
            }
            time++;
        }
    }

    public override void Finish()
    {
        characterInstancesCollection.battleMove.onAnimationFinished -= WaitForAnimationDone;
    }

    public override void WaitForAnimationDone()
    {
        switch (currentAnim)
        {
            case 0:
                characterInstancesCollection.battleMove.SetMovementState(BattleActionEnum.Jump);
                break;
            case 1:
                time = 1;
                BattleManager.instance.ApplyDamage(attack);
                BattleManager.instance.SetAnimationVariables(BattleActionEnum.WalkingBack);
                characterInstancesCollection.battleMove.SetMovementState(BattleActionEnum.Walking);
                break;
            case 2:
                BattleManager.instance.UpdateCurrenState(new IdleState());
                return;
        }
        currentAnim++;
    }

    public override void DoActionEffect()
    {
        BattleManager.instance.effectManager.DisplayEffect(BattleManager.instance.SelectedCharacterObject().transform, attack.effectType);
    }
}