using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class Player {
  public enum PlayerColors
  {
    RED, GREEN, BLUE, YELLOW, CYAN, PURPLE, ORANGE
  }

  public enum Statistic
  {
    NUM_TYPES
  }
  #region State
  #endregion

  #region Statistics
  public float ActingTime = 0f;
  public Dictionary<Statistic, int> Statistics = new Dictionary<Statistic, int>();
  #endregion

  public int Position;
  public int Place;

 public int NumChips = -1;
    public List<int> AcceptedGifts = null;

    public PlayerColors Color { get; set; }

    


  public Player() {

        AcceptedGifts = new List<int>();
        NumChips = 0;
    for (int i = 0; i < (int)Statistic.NUM_TYPES; ++i)
      Statistics[(Statistic)i] = 0;
  }

  public Color veryLightColor()
  {
    return GameGUI.VeryLightColors[(int)Color];
  }
  public Color lightColor()
  {
    return GameGUI.LightColors[(int)Color];
  }
  public Color solidColor()
  {
    return GameGUI.SolidColors[(int)Color];
  }

  public int totalScore()
  {
        int giftTotal = 0;
        int priorGift = -1;
        foreach( int card in this.AcceptedGifts){

            if (card != priorGift + 1)
            {
                giftTotal += card;
            }
            priorGift = card;

            
        }
        int res = NumChips - giftTotal;
    return res;
  }
  public float totalScoreWithTieBreakers()
  {
    return totalScore() + 0;
  }
}
