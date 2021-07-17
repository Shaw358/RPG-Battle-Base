using UnityEngine;
using System.Collections.Generic;
using BattleEnums;

public class BattleManager : MonoSingleton<BattleManager>
{
    //===================Variables===================\\
    [Header("Player")]
    public Party playerParty; //{ get; private set; }
    [SerializeField] public List<GameObject> playerPartyPos = new List<GameObject>();
    [SerializeField] public List<CharacterBattleInstanceCollection> playerCollection = new List<CharacterBattleInstanceCollection>();

    [Header("Enemy")]
    public Party enemyParty; //{ get; private set; }
    [SerializeField] public List<GameObject> enemyPartyPos = new List<GameObject>();
    [SerializeField] public List<CharacterBattleInstanceCollection> enemyCollection = new List<CharacterBattleInstanceCollection>();

    public int currentCharacterIndex { get; private set; }
    private BaseState currentState;
    public Character currentCharacter { get; private set; }
    public Character selectedCharacter;

    bool playerTurn;

    [SerializeField] CharacterBattleInformationUpdater informationUIUpdater;
    [SerializeField] public DamageTextHandler damageFeedback;
    public ActionEffectManager effectManager { get; private set; }

    [SerializeField] BattleMenu mainBattleMenu;

    //===================Mono Life Cycle===================\\
    private void Awake()
    {
        playerParty = GameTracker.instance.playerParty;

        #region Godawful testing, I hate this
        Stats tempStatPlayer = new Stats(1, 1, 1, 1, 1);
        
        enemyParty.AddCharacter(new Character(Factions._FACTIONS.BRASS, 100, 100));
        enemyParty.GetCharacter(0).Increasehealth(100);
        enemyParty.GetCharacter(0)._char_party = enemyParty;
        enemyParty.GetCharacter(0).basestats = tempStatPlayer;
        enemyParty.GetCharacter(0).StatsReset();
        #endregion

        currentCharacter = playerParty.GetCharacter(0) as Character;
        currentState = new IdleState();
    }

    private void OnEnable()
    {
        playerParty = GameTracker.instance.playerParty;
    }

    public void InitializeBattle(Party newPlayerParty, Party newEnemyParty)
    {
        //NOTE: This one's going to be a doozy
        playerParty = GameTracker.instance.playerParty;
        enemyParty = newEnemyParty;
    }

    public void UpdateCurrenState(BaseState newState)
    {
        if (playerParty.GetCharacters().Contains(selectedCharacter))
        {
            informationUIUpdater.UpdateCharacterInfo(GetIndexOfCurrentPlayer(), (int)selectedCharacter._hp, (int)selectedCharacter._mp);
        }

        currentState.Finish();

        currentState = newState;
        EndTurn();

        currentState.Start();
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    private void Start()
    {
        //for testing, give me a break
    }

    //===================Animation===================\\
    public void SetAnimationVariables(BattleActionEnum action)
    {
        switch (action)
        {
            case BattleActionEnum.Charging:
                break;
            case BattleActionEnum.PhysicalAttack:
                break;
            case BattleActionEnum.MagicAttack:
                break;
            case BattleActionEnum.UseItem:
                //playerCollection[0].battleMove.SetMovementState
                break;
            case BattleActionEnum.Walking:
                playerCollection[0].battleMove.SetTarget(enemyCollection[0].gameObject);
                break;
            case BattleActionEnum.Jump:
                break;
            case BattleActionEnum.WalkingBack:
                playerCollection[0].battleMove.SetTarget(playerPartyPos[GetIndexOfCurrentPlayer()]);
                break;
        }
    }

    public void EndTurn()
    {

    }

    //===================Utils===================\\
    public void ApplyDamage(Attack attack)
    {
        currentCharacter = playerParty.GetCharacter(0) as Character;
        selectedCharacter = enemyParty.GetCharacter(0) as Character;
        damageFeedback.DisplayDamage(DamageCalculator.Calculate(currentCharacter, attack, selectedCharacter).ToString(), enemyCollection[0].gameObject);
        
        if(playerParty.GetCharacters().Contains(selectedCharacter))
        {
            informationUIUpdater.UpdateCharacterInfo(GetIndexOfCurrentPlayer(), (int)selectedCharacter._hp, (int)selectedCharacter._mp);
        }
    }

    public CharacterBattleInstanceCollection GetCurrentPlayerCharacterInstanceCollection()
    {
        return playerCollection[GetIndexOfCurrentPlayer()];
    }

    public int GetIndexOfCurrentPlayer()
    {
        return playerParty.GetCharacters().FindIndex(x => x._name == currentCharacter._name);
    }

    public int GetIndexOfSelectedCharacter()
    {
        int characterIndex = playerParty.GetCharacters().FindIndex(x => x._name == currentCharacter._name);

        if (characterIndex != -1)
        {
            return characterIndex;
        }
        else
        {
            return enemyParty.GetCharacters().FindIndex(x => x._name == currentCharacter._name);
        }
    }

    public Transform GetBasePosOfCurrentCharacter()
    {
        int characterIndex = playerParty.GetCharacters().FindIndex(x => x._name == currentCharacter._name);

        if (characterIndex != -1)
        {
            return playerPartyPos[characterIndex].transform;
        }
        else
        {
            return enemyPartyPos[characterIndex].transform;
        }
    }

    public GameObject SelectedCharacterObject()
    {
        GameObject target = null;
        int characterIndex = 0;

        characterIndex = playerParty.GetCharacters().FindIndex(x => x._name == currentCharacter._name);
        if (characterIndex == -1)
        {
            characterIndex = enemyParty.GetCharacters().FindIndex(x => x._name == currentCharacter._name);
            target = enemyCollection[characterIndex].gameObject;
        }
        else
        {
            target = playerCollection[characterIndex].gameObject;
        }
        return target;
    }

    public void ReturnToBattleMenu()
    {
        mainBattleMenu.GoToMainBattleMenu();
    }
}