using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class PTakeCard : PlayerEvent {

	private int _numChips = 0;
	public PTakeCard() : base(Game.theGame.CurrentPlayer)
	{
		
	}
	
	
	
	public override void Do(Timeline timeline)
	{
		
		_player.NumChips += Game.theGame.NumCenterChips;
		Game.theGame.NumCenterChips = 0;
		
		_numChips = Game.theGame.NumCenterChips;
		_player.TakeGift(Game.theGame.GiftDeck.Pop(0));
 
		if (Game.theGame.GiftDeck.Count == 0)
			timeline.addEvent(new EEndGame());
	}


	public override float Act(bool qUndo = false)
	{
		
		// Center card
		GameObject movingCard = 
			GameGUI.cloneOnCanvas(GameGUI.theGameGUI.OfferedGift.gameObject);
		movingCard.transform.DOMove(_gui.CardContainer.position, 1f).
			OnComplete(() =>
			{
				GameObject.Destroy(movingCard);
				_gui.draw();
			});
		movingCard.transform.DORotate(_gui.transform.rotation.eulerAngles, 1f);
 
		
		// Center chips
		if (_numChips > 0)
		{
			GameObject movingChip = GameGUI.cloneOnCanvas(GameGUI.theGameGUI.OfferedChips);
			movingChip.transform.DOMove(_gui.Chips.transform.position, 1f).
				OnComplete(() => GameObject.Destroy(movingChip));
			movingChip.transform.DORotate(_gui.transform.rotation.eulerAngles, 1f);
		}
 
		GameGUI.theGameGUI.drawCenter();
		// New center card
		GameGUI.theGameGUI.OfferedGift.transform.DOMove(
				GameGUI.theGameGUI.DeckSizeText.transform.position, 1f).
			From().
			SetDelay(0.5f);
		return 0;
	}
}
