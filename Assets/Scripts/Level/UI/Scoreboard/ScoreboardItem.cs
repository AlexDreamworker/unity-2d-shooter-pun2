using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace ShooterPun2D.pt2
{
	public class ScoreboardItem : MonoBehaviourPunCallbacks
	{
		[SerializeField] private TMP_Text _playerNameText;
		[SerializeField] private TMP_Text _fragsText;

		private Player _player;
		private int _fragsCount;

		public int FragsCount => _fragsCount;
		
		public void Initialize(Player player)
		{
			_player = player;
			_playerNameText.text = player.NickName;
			UpdateFrags();
		}

		void UpdateFrags() 
		{
			if (_player.CustomProperties.TryGetValue("Frags", out object fragsProperty))
			{
				if ((int)fragsProperty > _fragsCount)
					_fragsCount = (int)fragsProperty;

				_fragsText.text = _fragsCount.ToString();
			}
		}

		public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) 
		{
			if (targetPlayer == _player) 
			{
				if (changedProps.ContainsKey("Frags"))
				{
					UpdateFrags();
				}
			}
		}		
	}
}

