using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PPayChip : PlayerEvent {
	
	
	public PPayChip():base(Game.theGame.CurrentPlayer){}
	
	
	
	public override void Do(Timeline timeline)
	{
		--_player.NumChips;
		++Game.theGame.NumCenterChips;
		Game.theGame.CurrentPlayer = PlayerList.nextPlayer(_player);


	}

	public override float Act(bool qUndo = false)
	{
		
		AudioPlayer.PlayClip(AudioPlayer.AudioClipEnum.CHIP);

		GameObject chipCopy = GameGUI.cloneOnCanvas(_gui.Chips);
		chipCopy.DestroyChildrenImmediate();
		
		
		
		chipCopy.transform.DOMove(
				GameGUI.theGameGUI.OfferedChips.transform.position, 1f).
			OnComplete(() =>
			{
				GameObject.Destroy(chipCopy);
				GameGUI.theGameGUI.drawCenter();
			});


		GameObject giftCopy = GameGUI.cloneOnCanvas(_gui.DialogGUI.GiftIcon);
		PlayerGUI nextPlayerGui = GameGUI.currentPlayerPad();
//		giftCopy.transform.DOMove(nextPlayerGui.DialogGUI.GiftIcon.transform.position, 1f).onComplete(()=>
//		{
//			GameObject.Destroy(giftCopy);
//			nextPlayerGui.draw();
//		});
		
		
		giftCopy.transform.DOMove(
				nextPlayerGui.DialogGUI.GiftIcon.transform.position, 1f).
			OnComplete(() =>
			{
				GameObject.Destroy(giftCopy);
				nextPlayerGui.draw();
			});
		
		
		giftCopy.transform.DORotate(nextPlayerGui.transform.rotation.eulerAngles, 1f);
 
		_gui.draw();
		
		return 0f;
	}
}
