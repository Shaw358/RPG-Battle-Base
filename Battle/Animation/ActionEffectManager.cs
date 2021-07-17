using ActionEffects;
using UnityEngine;

public class ActionEffectManager : MonoBehaviour
{
    [SerializeField] Animator effectGameobject;
    [SerializeField] GameObject effectPrefabs;
    
    public void DisplayEffect(Transform position, ActionEffectEnum effect)
    {
        transform.position = position.position;

        switch (effect)
        {
            case ActionEffectEnum.Slash:
                effectGameobject.Play("Slash", 0);
                break;
            case ActionEffectEnum.Punch:
                effectGameobject.Play("Punch", 0);
                break;
            case ActionEffectEnum.Fireball:
                effectGameobject.Play("Fireball", 0);
                break;
        }
    }
}