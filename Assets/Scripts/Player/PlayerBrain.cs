using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PlayerBrain : MonoBehaviour
	{
		private void Start()
		{
			FindObjectOfType<NetworkManager>().AddPlayer(this);
		}
	}
}

