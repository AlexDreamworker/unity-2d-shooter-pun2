using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using System;
using ExitGames.Client.Photon;

namespace ShooterPun2D.pt2
{
	public class NetworkManager : MonoBehaviourPunCallbacks
	{
		[SerializeField] private GameObject _playerPrefab;

		private void Start()
		{
			Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-2f, 2f));
			PhotonNetwork.Instantiate(_playerPrefab.name, randomPosition, Quaternion.identity);

			//Регистрация кастомного типа данных с сериализацией / десериализацией
			PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);
		}

		public void Leave() 
		{
			// Вызов для выхода из комнаты
			PhotonNetwork.LeaveRoom();
		}

		public override void OnLeftRoom() 
		{
			// Когда мы покидаем комнату
			SceneManager.LoadScene(0);
		}

		public override void OnPlayerEnteredRoom(Player newPlayer) 
		{ 
			Debug.LogFormat("Player {0} entered room", newPlayer.NickName);
		}

		public override void OnPlayerLeftRoom(Player otherPlayer) 
		{
			Debug.LogFormat("Player {0} entered room", otherPlayer.NickName);
		}	

		//?----CUSTOM---TYPE---SERIALIZE-/-DESERIALIZE------------------------------------------------------
		public static object DeserializeVector2Int(byte[] data) 
		{
			Vector2Int result = new Vector2Int();

			result.x = BitConverter.ToInt32(data, 0);
			result.y = BitConverter.ToInt32(data, 4);
			
			return result;
		}	

		public static byte[] SerializeVector2Int(object obj) 
		{
			Vector2Int vector = (Vector2Int)obj;
			byte[] result = new byte[8];

			BitConverter.GetBytes(vector.x).CopyTo(result, 0);
			BitConverter.GetBytes(vector.y).CopyTo(result, 4);

			return result;
		}
		//?----CUSTOM---TYPE---SERIALIZE-/-DESERIALIZE------------------------------------------------------
	}
}

