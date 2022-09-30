using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class Scoreboard : MonoBehaviourPunCallbacks
	{
		[SerializeField] private Transform _container;
		[SerializeField] private GameObject _scoreboardItemPrefab;

		Dictionary<Photon.Realtime.Player, ScoreboardItem> _scoreboardItems = new Dictionary<Photon.Realtime.Player, ScoreboardItem>();

		private void Start()
		{
			foreach (var player in PhotonNetwork.PlayerList)
			{
				AddScoreboardItem(player);
			}
		}

		public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer) 
		{
			AddScoreboardItem(newPlayer);
		}

		public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer) 
		{
			RemoveScoreboardItem(otherPlayer);
		}

		private void AddScoreboardItem(Photon.Realtime.Player player) 
		{
			var itemObject = Instantiate(_scoreboardItemPrefab, _container);
			var item = itemObject.GetComponent<ScoreboardItem>();
			item.Initialize(player);
			_scoreboardItems[player] = item;
		}

		private void RemoveScoreboardItem(Photon.Realtime.Player player) 
		{
			Destroy(_scoreboardItems[player].gameObject);
			_scoreboardItems.Remove(player);
		}
	}
}

