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

		private int _frags;
		private Photon.Realtime.Player _player;
		
		public void Initialize(Photon.Realtime.Player player)
		{
			_player = player;
			_playerNameText.text = player.NickName;
			UpdateFrags();
		}

		private void UpdateFrags() 
		{
			if (_player.CustomProperties.TryGetValue("Frags", out object frags))
			{
				if ((int)frags > _frags)
					_frags = (int)frags;

				_fragsText.text = _frags.ToString();
			}
		}

		public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable changedProps) 
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
