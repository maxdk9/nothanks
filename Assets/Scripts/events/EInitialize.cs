using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class EInitialize : EngineEvent
{
    public int? Seed;

    public EInitialize()
    {
        QUndoable = false;
    }

    public override void Do(Timeline c)
    {
        //Debug.Log("Einitialize::Do");
        initializeSeeds();
        initializeModel();

        initializeGUI();

        c.addEvent(new EStartRound());
    }

    public void initializeSeeds()
    {
        if (!Seed.HasValue)
            Seed = Mathf.Abs(System.DateTime.UtcNow.Ticks.GetHashCode());
        //Debug.Log("Using seed: " + Seed);
        UnityEngine.Random.InitState(Seed.Value);
        //UniExtensions.Rnd.rnd = new System.Random( Seed.Value );
    }

    public void initializeModel()
    {
        Player startPlayer = PlayerList.Players.GetRandom();
        PlayerList.setOrderToClockwiseWithStartAt(startPlayer);


        Game.theGame.CurrentGameState = Game.GameState.PLAY;
        Game.theGame.CurrentPlayer = startPlayer;
        List<int> allCards = Enumerable.Range(3, 33).ToList();
        allCards.Shuffle();
        
        Game.theGame.GiftDeck = allCards.Take(24).ToList();
        Game.theGame.NumCenterChips = 0;

        foreach (Player player in PlayerList.Players)
        {
            if (PlayerList.Players.Count == 7)
            {
                player.NumChips = 7;
            }
            else
            {


                if (PlayerList.Players.Count == 6)
                {
                    player.NumChips = 9;
                }
                else
                {
                    player.NumChips = 11;
                }
            }


        }
        
    }

    public void initializeGUI()
    {
        foreach (PlayerGUI playerGUI in GameGUI.theGameGUI.PlayerPads)
        {
            if (PlayerList.Players.Any(p => p.Position == playerGUI.Position))
            {
                playerGUI.init();
            }
           else
                playerGUI.gameObject.SetActive(false);
        }

        GameGUI.theGameGUI.PlayerPads =
            GameGUI.theGameGUI.PlayerPads.Where(p => p.gameObject.activeSelf).ToList();

        //GameGUI.theGameGUI.Scoreboard.buildScoreboard();
    }

    public override float Act(bool qUndo)
    {
        AudioPlayer.PlayClip(AudioPlayer.AudioClipEnum.SHUFFLE);
        GameGUI.theGameGUI.draw();
        return 0;
    }
}