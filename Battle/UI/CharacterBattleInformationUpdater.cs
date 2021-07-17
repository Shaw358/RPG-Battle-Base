using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterBattleInformationUpdater : MonoBehaviour
{
    [HideInInspector]
    public List<int> maxHp = new List<int>();
    [HideInInspector]
    public List<int> maxMp = new List<int>();

    public List<TextMeshProUGUI> characterNames = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> hp = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> mp = new List<TextMeshProUGUI>();

    public void SetInitialValues(List<string> newNames, List<int> newMaxHp, List<int> newHp, List<int> newMaxMp, List<int> newMp)
    {
        for (int i = 0; i < newNames.Count; i++)
        {
            characterNames[i].text = newNames[i];
        }
        for (int i = 0; i < newMaxHp.Count; i++)
        {
            maxHp[i] = newMaxHp[i];
        }
        for (int i = 0; i < newHp.Count; i++)
        {
            hp[i].text = newHp[i].ToString();
        }
        for (int i = 0; i < maxMp.Count; i++)
        {
            maxMp[i] = newMaxMp[i];
        }
        for (int i = 0; i < newMp.Count; i++)
        {
            mp[i].text = newMp[i].ToString();
        }
    }

    public void UpdateCharacterInfo(int index, int newHp, int newMp)
    {
        hp[index].text = "HP: " + maxHp[index] + "/" + newHp.ToString();
        mp[index].text = "MP: " + maxMp[index] + "/" + newMp.ToString();
    }
}