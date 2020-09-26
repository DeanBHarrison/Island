using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumManager : MonoBehaviour
{
 
}
public enum ScenesSelect { Town, Woods, Cave }

public enum Coins
{
    RedCoin, BlueCoin, YellowCoin, GreenCoin, GoldCoin, GoldCoinGain,
    SmallRedCoin, SmallBlueCoin, SmallYellowCoin, SmallGreenCoin, BigRedCoin, BigBlueCoin, BigYellowCoin, BigGreenCoin
}

public enum BattleState { START, PLAYERTURN, ENEMYTURN, COMBAT, WON, LOST }

public enum PetSelect { Chick, Cat, Wolf, Lizard }

public enum ButtonSelect { Inn, Shop, NPC, Quest }

public enum QuestGoalType { Kill, Gather}
