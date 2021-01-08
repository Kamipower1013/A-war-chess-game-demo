using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public enum CardType
{
    PlayerPath,
    PlayeraddAttack_decrease,
    player_RefrashHP,
    player_AddDefenceValue,
    enemyAttack_decrease,
    enemyDefence_Decrease,
    enemy_PathDecrease,
    
}

public class CardManager : MonoBehaviour
{
    public static CardManager instance
    {

        get;
        private set;
    }

    public findTheWayPlayer findTheWayPlayerScript;
    public PlayerCharactor PlayerCharactorScript;
    public AlltheCard AlltheCardList;
    public GameObject CardPerfab1;
    public GameObject CardPerfab2;
    public GameObject CardPerfab3;
    public Card temp;
    public bool TheCardRise;
    public Card card1;
    public Card card2;
    public Card card3;
    public Text Card1Text;
    public Text Card2Text;
    public Text Card3Text;

    public int RoundCount;

    public Card TheChoicenCard;
    public bool HadchoicenCard;
    public int thechoicen_CardID;

    public bool isRoundStart;


    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
        TheCardRise = false;
        RoundCount = battleManager.instance.battleRoundCount;
        Init_CurrentCardlist();
        HadchoicenCard = false;
        thechoicen_CardID = -1;
        isRoundStart = true;

        Refresh_Status_PenelAbout_All();
        AlltheEnemy.instance.Refrash_All_Enemy_Staus();
    }



    //public List<Card> AllMyCardList = new List<Card>();

    public List<Card> currentCurrentList = new List<Card>();
    //public GameObject CardListpanel;
    // Update is called once per frame

    public void Init_CurrentCardlist()
    {
        for (int i = 0; i < AlltheCardList.AllCardList.Count; i++)
        {
            if (AlltheCardList.AllCardList[i] != null)
            {
                Debug.Log("add了Card初始化");
                currentCurrentList.Add(AlltheCardList.AllCardList[i]);
            }
        }

    }

    public void Sorted_Give_Card()
    {

        SortCard(currentCurrentList);
        card1 = currentCurrentList[0];
        card2 = currentCurrentList[1];
        card3 = currentCurrentList[2];
        Card1Text.text = card1.discribe;
        Card2Text.text = card2.discribe;
        Card3Text.text = card3.discribe;

    }

    public void SortCard(List<Card> List)//洗牌算法
    {
        for (int i = 0; i < List.Count; i++)
        {
            int k = Random.Range(i, List.Count);
            temp = List[i];
            List[i] = List[k];
            List[k] = temp;
        }

    }

    public void Choice_Perfab1()
    {
        if (card1 != null)
        {
            thechoicen_CardID = card1.CardID;
            Choice_the_Card();
        }
    }
    public void Choice_Perfab2()
    {
        if (card2 != null)
        {
            thechoicen_CardID = card2.CardID;
            Choice_the_Card();
        }
    }

    public void Choice_Perfab3()
    {
        if (card3 != null)
        {
            thechoicen_CardID = card3.CardID;
            Choice_the_Card();
        }
    }

    public void Choice_the_Card()
    {
        HadchoicenCard = true;

        for (int i = 0; i < currentCurrentList.Count; i++)
        {
            if (thechoicen_CardID == currentCurrentList[i].CardID)
            {
                TheChoicenCard = currentCurrentList[i];
            }
        }

        TheCard_effect();
        Refresh_Status_PenelAbout_All();
        AlltheEnemy.instance.Refrash_All_Enemy_Staus();
    }



    public void TheCard_effect()
    {
        if (TheChoicenCard.thisCardType == CardType.PlayerPath)
        {
            Card_Change_Player_MovePath(TheChoicenCard, ref findTheWayPlayerScript.TheBuff_addPathStep, ref PlayerCharactorScript.buff_Attackvalue,ref PlayerCharactorScript.buff_DefenceValue);
        }
        else if (TheChoicenCard.thisCardType == CardType.enemyAttack_decrease)
        {
            Card_Decrease_Enemy_Attack(TheChoicenCard);
        }
        else if(TheChoicenCard.thisCardType == CardType.enemyDefence_Decrease)
        {
            Card_Enemy_Defence_Decrease(TheChoicenCard);
        }
        else if (TheChoicenCard.thisCardType == CardType.player_RefrashHP)
        {
            Card_Add_HP(TheChoicenCard);
        }
        else if (TheChoicenCard.thisCardType == CardType.player_AddDefenceValue)
        {
            Card_Player_AddDefence(TheChoicenCard, ref PlayerCharactorScript.buff_DefenceValue);
        }
        else if (TheChoicenCard.thisCardType == CardType.enemy_PathDecrease)
        {
            Card_Enemy_StepDelay(TheChoicenCard);
        }
         //Player_panelManager.instance.openPlayer_Panel();
      
    }



    public void Card_Enemy_StepDelay(Card SelectCard)
    {
        AlltheEnemy.instance.Change_Allenemy_Step(SelectCard.enemy_Path);

    }

    public void Reset_Card_Enemy_StepDelay(Card SelectCard)
    {
        AlltheEnemy.instance.Change_Allenemy_Step(0);
    }
    

    public void Card_Enemy_Defence_Decrease(Card SelcetCard)
    {
        AlltheEnemy.instance.change_Allenemy_Defence(SelcetCard.Add_defenceValue);

    }
    public void Reset_Card_Enemy_Defence_Decrease(Card SelcetCard)
    {
        AlltheEnemy.instance.change_Allenemy_Defence(0);

    }

    public void Card_Decrease_Enemy_Attack(Card SelectCard)
    {
        AlltheEnemy.instance.Change_Allenemy_attack_Value(SelectCard.Add_attackValue);
    }

    public void Reset_Card_Decrease_Enemy_Attack(Card SelectCard)
    {
        AlltheEnemy.instance.Change_Allenemy_attack_Value(0);
    }

    public void Card_Player_AddDefence(Card SelectCard, ref int Buff_defence_Value)
    {
        Buff_defence_Value += SelectCard.Add_defenceValue;
    }


    public void Reset_Card_Player_AddDefence(Card SlectCard,ref int buff_defence_Value)
    {
        buff_defence_Value = 0;
    }

   

    public void Card_Change_Player_MovePath(Card selectCard, ref int Buff_pathNum, ref int Buff_attack_Value,ref int Buff_Defence)
    {

        Buff_pathNum += selectCard.player_Path;
        Buff_attack_Value += selectCard.Add_attackValue;
        Buff_Defence += selectCard.Add_defenceValue;
    }
    public void Reset_Card_Change_Player_MovePath(Card selectCard, ref int Buff_pathNum, ref int Buff_attack_Value, ref int Buff_Defence)
    {
        Buff_pathNum = 0;
        Buff_attack_Value = 0;
        Buff_Defence = 0;
    }


    public void Card_Add_HP(Card selectCard)
    {
        int HP = PlayerCharactorScript.HP + selectCard.add_HP;
        PlayerCharactorScript.HPbuff.Play();
        if (HP > 100)
        {
            PlayerCharactorScript.HP = 100;
            PlayerCharactorScript.Get_hurt_Reflash_playerStatus();
           
        }
        else
        {
            PlayerCharactorScript.HP = HP;
            PlayerCharactorScript.Get_hurt_Reflash_playerStatus();
        }
    }


    public void Refresh_Status_PenelAbout_All()
    {
        PlayerCharactorScript.Refrash_The_Current_PlayerData();

    }

    public void RoundinGiveCard()
    {
        Sorted_Give_Card();

        Debug.Log(TheCardRise);
        Rise_The_Card();
        TheCardRise = true;

    }




    public void Reset_AllCardEffect()
    {

        if (TheChoicenCard.thisCardType == CardType.PlayerPath)
        {
            Reset_Card_Change_Player_MovePath(TheChoicenCard, ref findTheWayPlayerScript.TheBuff_addPathStep, ref PlayerCharactorScript.buff_Attackvalue,ref PlayerCharactorScript.buff_DefenceValue);
        }
        else if (TheChoicenCard.thisCardType == CardType.enemyAttack_decrease)
        {
            Reset_Card_Decrease_Enemy_Attack(TheChoicenCard);
        }
        else if(TheChoicenCard.thisCardType == CardType.enemyDefence_Decrease)
        {
            Reset_Card_Enemy_Defence_Decrease(TheChoicenCard);
        }
        else if (TheChoicenCard.thisCardType == CardType.player_RefrashHP)
        {

        }
        else if(TheChoicenCard.thisCardType == CardType.player_AddDefenceValue)
        {
            Reset_Card_Player_AddDefence(TheChoicenCard, ref PlayerCharactorScript.buff_DefenceValue);
        }
        else if (TheChoicenCard.thisCardType == CardType.enemy_PathDecrease)
        {
            Reset_Card_Enemy_StepDelay(TheChoicenCard);
        }


        thechoicen_CardID = -1;
    }

    public void Update()
    {

        if (isRoundStart == true && battleManager.instance.current_Allbattle_state == Allbattle_state.Player_state && battleManager.instance.current_state == Battle_State.StartRoundstay && TheCardRise == false)
        {
            Debug.Log("在卡牌开始环节进入1次");
            if (HadchoicenCard == true)
            {
                Reset_AllCardEffect();
                Refresh_Status_PenelAbout_All();
                AlltheEnemy.instance.Refrash_All_Enemy_Staus();
                HadchoicenCard = false;
            }
            isRoundStart = false;
            RoundinGiveCard();

        }
        
        else if (HadchoicenCard == false && battleManager.instance.current_Allbattle_state == Allbattle_state.Player_state && battleManager.instance.current_state != Battle_State.StartRoundstay && TheCardRise == true)
        {
            down_The_Card();
            TheCardRise = false;
        }
        else if(HadchoicenCard == false&& battleManager.instance.current_Allbattle_state != Allbattle_state.Player_state&& TheCardRise == true)
        {
            down_The_Card();
            TheCardRise = false;
        }
        else if (HadchoicenCard == true && battleManager.instance.current_Allbattle_state == Allbattle_state.Player_state && battleManager.instance.current_state == Battle_State.StartRoundstay && TheCardRise == true)
        {
            down_The_Card();
            TheCardRise = false;
        }
    }


    public void Rise_The_Card()
    {
        DOTween.PlayForward("CardRise");
    }

    public void down_The_Card()
    {

        DOTween.PlayBackwards("CardRise");

    }
}
