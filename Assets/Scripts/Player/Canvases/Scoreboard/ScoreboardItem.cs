using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
//using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace ShooterPun2D.pt2
{
	public class ScoreboardItem : MonoBehaviourPunCallbacks
	{
		[SerializeField] private TMP_Text _playerNameText;
		[SerializeField] private TMP_Text _killsText;
		[SerializeField] private TMP_Text _deathsText;

		//private int _kills;
		//private int _deaths;

		private Player _player;
		
		public void Initialize(Player player)
		{
			_player = player;
			_playerNameText.text = player.NickName;
			UpdateStats(_player);
		}

		public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps) 
		{
			if (targetPlayer == _player)
			{
				if (/*changedProps.ContainsKey("playerKills") ||*/ changedProps.ContainsKey("playerDeaths"))
					UpdateStats(targetPlayer);
			}
		}

		private void UpdateStats(Player player) 
		{
			//int kills = 0;
			//Sint deaths = 0;

			// if (player.CustomProperties.ContainsKey("playerKills"))
			// {
			// 	kills = (int)player.CustomProperties["playerKills"];
			// 	_killsText.text = kills.ToString();
			// 	Debug.Log("KILLS: " + kills);
			// 	//_killsText.text = player.CustomProperties["playerKills"].ToString();
			// 	//Debug.Log("KILL PROPS: " + player.CustomProperties["playerKills"].ToString());
			// }
			// else 
			// {
			// 	_killsText.text = kills.ToString();
			// 	Debug.Log("PlayerKills is NULL");
			// }

			// if (player.CustomProperties.ContainsKey("playerDeaths"))
			// {
			// 	//deaths = (int)player.CustomProperties["playerDeaths"];
			// 	_deathsText.text = player.CustomProperties["playerDeaths"].ToString();
			// 	//Debug.Log("DEATHS: " + deaths);
			// 	//_deathsText.text = player.Cus1tomProperties["playerDeaths"].ToString();
			// 	Debug.Log("DEATH PROPS: " + player.CustomProperties["playerDeaths"].ToString());
			// } 
			// else 
			// {
			// 	//_deathsText.text = deaths.ToString();
			// 	Debug.Log("PlayerDeaths is NULL");
			// }

			//!----------------------------------------
			//if (player.CustomProperties.TryGetValue("playerDeaths", out object deaths))
			//{
			//	_deathsText.text = deaths.ToString();
			//}
			//_deathsText.text = player.CustomProperties["playerDeaths"].ToString();
		}	
	}
}

