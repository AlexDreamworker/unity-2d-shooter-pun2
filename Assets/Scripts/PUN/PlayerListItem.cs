using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace ShooterPun2D
{
	public class PlayerListItem : MonoBehaviourPunCallbacks
	{
		[SerializeField] TMP_Text _text;
		private Player _player;

		public void SetUp(Player player) 
		{
			_player = player;
			_text.text = _player.NickName;
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
	}
}

