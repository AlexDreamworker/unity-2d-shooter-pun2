using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
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
		List<GameObject> _itemsGO = new List<GameObject>(); //todo: rename

		private void Start()
		{
			foreach (var player in PhotonNetwork.PlayerList)
			{
				AddScoreboardItem(player);
			}
		}

		private void Update()
		{
			var sortedItemsGO = _itemsGO.OrderByDescending(i => i.GetComponent<ScoreboardItem>().FragsCount).ToArray();

			for (var i = 0; i < sortedItemsGO.Length; i++)
			{
				var child = _container.transform.GetChild(i);
				var ix = sortedItemsGO[i].transform.GetSiblingIndex(); //todo: rename

				child.SetSiblingIndex(ix);
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

		private void AddScoreboardItem(Player player) 
		{
			var itemObject = Instantiate(_scoreboardItemPrefab, _container);
			var item = itemObject.GetComponent<ScoreboardItem>();
			item.Initialize(player);
			_scoreboardItems[player] = item;

			_itemsGO.Add(itemObject);// !!!
		}

		private void RemoveScoreboardItem(Player player) 
		{
			var itemGO = _scoreboardItems[player].gameObject;// !!!
			_itemsGO.Remove(itemGO);// !!!

			Destroy(_scoreboardItems[player].gameObject);
			_scoreboardItems.Remove(player);
		}
	}
}

