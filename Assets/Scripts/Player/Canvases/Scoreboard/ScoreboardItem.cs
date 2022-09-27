using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
//using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace ShooterPun2D.pt2
{
	public class ScoreboardItem : MonoBehaviour/*PunCallbacks*/
	{
		[SerializeField] private TMP_Text _playerNameText;
		[SerializeField] private TMP_Text _killsText;
		[SerializeField] private TMP_Text _deathsText;

		//private Player _player;
		
		public void Initialize(Player player)
		{
			//_player = player;

			_playerNameText.text = player.NickName;
			//UpdateStats();
		}

		// private void UpdateStats() 
		// {
		// 	if (_player.CustomProperties.TryGetValue("kills", out object kills))
		// 	{
		// 		_killsText.text = kills.ToString();
		// 	}
		// }

		// public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) 
		// {
		// 	if (targetPlayer == _player)
		// 	{
		// 		if (changedProps.ContainsKey("kills")) 
		// 		{
		// 			UpdateStats();
		// 		}
		// 	}
		// }
	}
}

