using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class ScoreboardItem : MonoBehaviour
	{
		[SerializeField] private TMP_Text _playerNameText;
		[SerializeField] private TMP_Text _killsText;
		[SerializeField] private TMP_Text _deathsText;

		public void Initialize(Player player)
		{
			_playerNameText.text = player.NickName;
		}
	}
}

