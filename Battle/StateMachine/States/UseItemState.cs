using BattleEnums;
using UnityEngine;

public class UseItemState : BaseState
{
    private int currentAnim;
    CharacterBattleInstanceCollection characterInstancesCollection;
    Item item;

    public UseItemState(CharacterBattleInstanceCollection newCollection, Item newItem)
    {
        characterInstancesCollection = newCollection;
        item = newItem;
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
        currentAnim++;
        switch (currentAnim)
        {
            case 0:
                characterInstancesCollection.battleMove.SetMovementState(BattleActionEnum.Jump);
                time = 1;
                break;
            case 1:
                item.UseItem(BattleManager.instance.selectedCharacter);
                for (int i = 0; i < 1; i++) //TODO: If multiple characters are affected, do the magic multiple times
                {
                    //NOTE: Item class needs a rework with a subclass for things like Equipable or Consumable, till that is done I can't properly display the amount of effect the consumable had
                    BattleManager.instance.damageFeedback.DisplayDamage("0", null);
                }
                BattleManager.instance.UpdateCurrenState(new IdleState());
                break;
            case 2:
                break;
        }
    }

    public override void DoActionEffect()
    {
        //TODO: return how much it affected the character
        BattleManager.instance.effectManager.DisplayEffect(BattleManager.instance.SelectedCharacterObject().transform, item.effectType);
    }
}
