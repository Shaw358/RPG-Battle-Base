using UnityEngine;

/// <summary>
/// This is purely for getting animation clip lengts... for now
/// </summary>
/// 
public class BattleAnimationHandler : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void SetAnimation(string animationName)
    {
        animator.Play(animationName);
    }

    public float GetAnimationLengthInSeconds()
    {
        #region Fallback?
        /*RuntimeAnimatorController ac = animator.runtimeAnimatorController;    //Get Animator controller
        for (int i = 0; i < ac.animationClips.Length; i++)                 //For all animations
        {
            if (ac.animationClips[i].name == animator.GetCurrentAnimatorClipInfo(0)[0].clip.name)        //If it has the same name as your clip
            {
                return ac.animationClips[i].length;
            }
        }
        Debug.LogError("No clip found!");
        return 0;*/

        //AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
        #endregion

        AnimatorClipInfo[] myAnimatorClip = animator.GetCurrentAnimatorClipInfo(0);
        if(myAnimatorClip[0].clip.length <= 0)
        {
            Debug.LogError("Clip too short or not present!");
            return 0;
            //Application.Quit();
        }
        return myAnimatorClip[0].clip.length;
    }

    public float GetAnimationLengthLeftInSeconds()
    {
        AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorClipInfo[] myAnimatorClip = animator.GetCurrentAnimatorClipInfo(0);
        return myAnimatorClip[0].clip.length * animationState.normalizedTime;
    }
}