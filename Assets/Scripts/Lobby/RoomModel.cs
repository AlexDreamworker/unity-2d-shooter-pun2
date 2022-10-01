using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShooterPun2D
{
	public class RoomModel : MonoBehaviour
	{
		[SerializeField] private GameObject[] _rooms;
		private GameObject _currentRoom;

		private int _currentRoomIndex;

		public int CurrentRoomIndex => _currentRoomIndex;

		private void Start()
		{
			_currentRoomIndex = 0;
			//_currentRoomIndex = PlayerPrefs.GetInt("PlayerModelIndex", 0);
			UpdateRoom();
		}

		public void NextRoom() //* CALL
		{
			if (_currentRoomIndex < _rooms.Length - 1)
				_currentRoomIndex++;
			else 
				_currentRoomIndex = 0;

			UpdateRoom();
		}

		public void PreviousRoom() //* CALL
		{
			if (_currentRoomIndex > 0)
				_currentRoomIndex--;
			else 
				_currentRoomIndex = _rooms.Length - 1;
			
			UpdateRoom();
		}

		private void UpdateRoom() 
		{
			foreach (var room in _rooms)
			{
				room.SetActive(false);
			}

			_currentRoom = _rooms[_currentRoomIndex];
			_currentRoom.SetActive(true);
			
			var temp = _currentRoomIndex + 1;
			PlayerPrefs.SetInt("RoomModelIndex", temp);
		}
	}
}

