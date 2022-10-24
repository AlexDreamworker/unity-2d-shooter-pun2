using Photon.Pun;
using TMPro;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PlayerListItem : MonoBehaviourPunCallbacks
	{
		[SerializeField] private TMP_Text _text;
		[SerializeField] private GameObject[] _itemSkins;
		private GameObject _currentSkin;
		private Photon.Realtime.Player _player;

		public void SetUp(Photon.Realtime.Player player) 
		{
			_player = player;
			_text.text = _player.NickName;
			UpdateItemSkin(player);
		}

		public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer) 
		{
			if (_player == otherPlayer)
				Destroy(gameObject);
		}

		public override void OnLeftRoom() 
		{
			Destroy(gameObject);
		}

		public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps) 
		{
			if (changedProps.ContainsKey("playerModel") && _player == targetPlayer) 
				UpdateItemSkin(targetPlayer);
		}

		void UpdateItemSkin(Photon.Realtime.Player player) 
		{
			if (player.CustomProperties.ContainsKey("playerModel"))
				_currentSkin = _itemSkins[(int)player.CustomProperties["playerModel"]];
				_currentSkin.SetActive(true);
		}
	}
}

