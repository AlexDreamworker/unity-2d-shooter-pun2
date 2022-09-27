using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class RoomListItem : MonoBehaviour
	{
		[SerializeField] private TMP_Text _text;

		private RoomInfo _info;

		public RoomInfo RoomInfo => _info;
		
		public void SetUp(RoomInfo info) 
		{
			_info = info;
			_text.text = info.Name;
		}

		public void OnClick() 
		{
			Launcher.Instance.JoinRoom(_info);
		}
	}
}

