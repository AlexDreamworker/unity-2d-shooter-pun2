using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class Scoreboard : MonoBehaviourPunCallbacks
	{
		[SerializeField] private Transform _container;
		[SerializeField] private GameObject _scoreboardItemPrefab;

		Dictionary<Player, ScoreboardItem> _scoreboardItems = new Dictionary<Player, ScoreboardItem>();
		List<GameObject> _itemsGameObjects = new List<GameObject>();

		void Start()
		{
			foreach (var player in PhotonNetwork.PlayerList)
				AddScoreboardItem(player);
		}

		void Update()
		{
			SortingScoreboardByFrags();
		}

		void SortingScoreboardByFrags() 
		{
			var sortedItems = _itemsGameObjects
				.OrderByDescending(i => i.GetComponent<ScoreboardItem>().FragsCount)
				.ToArray();

			for (var i = 0; i < sortedItems.Length; i++)
			{
				var child = _container.transform.GetChild(i);
				var sortedIndex = sortedItems[i].transform.GetSiblingIndex();

				child.SetSiblingIndex(sortedIndex);
			}
		}

		public override void OnPlayerEnteredRoom(Player newPlayer) 
		{
			AddScoreboardItem(newPlayer);
		}

		public override void OnPlayerLeftRoom(Player otherPlayer) 
		{
			RemoveScoreboardItem(otherPlayer);
		}

		void AddScoreboardItem(Player player) 
		{
			var itemObject = Instantiate(_scoreboardItemPrefab, _container);
			var item = itemObject.GetComponent<ScoreboardItem>();
			item.Initialize(player);
			_scoreboardItems[player] = item;

			_itemsGameObjects.Add(itemObject);
		}

		void RemoveScoreboardItem(Player player) 
		{
			var itemGO = _scoreboardItems[player].gameObject;
			_itemsGameObjects.Remove(itemGO);

			Destroy(_scoreboardItems[player].gameObject);
			_scoreboardItems.Remove(player);
		}
	}
}

