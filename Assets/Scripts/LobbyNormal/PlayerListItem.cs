using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PlayerListItem : MonoBehaviourPunCallbacks
	{
		[SerializeField] private TMP_Text _text;
		[SerializeField] private GameObject[] _itemSkins;
		private GameObject _currentSkin;
		private Player _player;

		public void SetUp(Player player) 
		{
			_player = player;
			_text.text = _player.NickName;
			UpdateItemSkin(player);
		}

		public override void OnPlayerLeftRoom(Player otherPlayer) 
		{
			if (_player == otherPlayer)
				Destroy(gameObject);
		}

		public override void OnLeftRoom() 
		{
			Destroy(gameObject);
		}

		public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps) 
		{
			if (changedProps.ContainsKey("playerModel") && _player == targetPlayer) 
			{
				UpdateItemSkin(targetPlayer);
			}
		}

		private void UpdateItemSkin(Player player) 
		{
			if (player.CustomProperties.ContainsKey("playerModel"))
			{
				_currentSkin = _itemSkins[(int)player.CustomProperties["playerModel"]];
				_currentSkin.SetActive(true);
			}
		}
	}
}

