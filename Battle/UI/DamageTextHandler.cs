using TMPro;
using UnityEngine;

public class DamageTextHandler : MonoBehaviour
{
    [SerializeField] DamageText[] textmesh;

    public void DisplayDamage(string damage, GameObject newTarget)
    {
        textmesh[0].gameObject.SetActive(true);
        textmesh[0].DoMove(damage, newTarget);
    }
}