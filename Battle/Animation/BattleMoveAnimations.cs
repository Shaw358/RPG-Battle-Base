using BattleEnums;
using System;
using System.Collections;
using UnityEngine;

public class BattleMoveAnimations : MonoBehaviour
{
    #region Lerping Variables
    Vector3 tempTarget;
    [SerializeField] float lerpSpeed;
    #endregion

    private BattleActionEnum currentMovement;
    CharacterBattleInstanceCollection collection;
    public Action onAnimationFinished;

    public void SetMovementState(BattleActionEnum newState)
    {
        switch (currentMovement)
        {
            case BattleActionEnum.Walking:
                break;
            case BattleActionEnum.Jump:
                break;
            case BattleActionEnum.WalkingBack:
                break;
            case BattleActionEnum.UseItem:
                BattleManager.instance.GetCurrentPlayerCharacterInstanceCollection().battleAnimationInstance.SetAnimation("UseItem");
                break;
            case BattleActionEnum.NONE:
                break;
        }

        currentMovement = newState;
    }

    public void SetTarget(GameObject newTarget)
    {
        tempTarget = new Vector2(newTarget.transform.position.x - 1.2f, newTarget.transform.position.y);
    }

    private void OnEnable()
    {
        onAnimationFinished += DebugStuff;
    }

    private void OnDisable()
    {
        onAnimationFinished -= DebugStuff;
    }

    private void Awake()
    {
        collection = GetComponent<CharacterBattleInstanceCollection>();
    }

    private void LateUpdate()
    {
        switch (currentMovement)
        {
            case BattleActionEnum.Walking:
                Lerp();
                break;
            case BattleActionEnum.Jump:
                collection.battleAnimationInstance.SetAnimation("JumpAnimation");
                StartCoroutine(Jump());
                break;
            case BattleActionEnum.NONE:
                break;
            case BattleActionEnum.UseItem:
                if (BattleManager.instance.GetCurrentPlayerCharacterInstanceCollection().battleAnimationInstance.GetAnimationLengthLeftInSeconds() <= 0)
                {
                    onAnimationFinished?.Invoke();
                }
                break;
        }
    }

    public void ClearCurrentMovent()
    {
        currentMovement = BattleActionEnum.NONE;
    }

    private void Lerp()
    {
        if(Vector2.Distance(transform.position, tempTarget) > 0.1)
        {
            transform.position = Vector2.Lerp(transform.position, tempTarget, lerpSpeed * Time.deltaTime);
        }
        else
        {
            Debug.Log(Vector3.Distance(transform.position, tempTarget));
            onAnimationFinished?.Invoke();
        }
    }

    public IEnumerator Jump()
    {
        yield return new WaitForSeconds(collection.battleAnimationInstance.GetAnimationLengthInSeconds());
        onAnimationFinished?.Invoke();
    }
    
    public void DebugStuff()
    {
        Debug.Log("AnimationOver");
    }
}