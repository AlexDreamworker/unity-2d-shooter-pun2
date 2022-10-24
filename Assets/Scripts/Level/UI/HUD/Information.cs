using Photon.Pun;
using TMPro;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class Information : MonoBehaviour
	{
		[SerializeField] private TMP_Text _pingRateText;
		[SerializeField] private TMP_Text _fpsRateText;

		private float _fpsMeasurePeriod = 0.5f;
    	private int _fpsAccumulator = 0;
    	private float _fpsNextPeriod = 0;
    	private int _currentFps;

		void Start()
		{
			_fpsNextPeriod = Time.realtimeSinceStartup + _fpsMeasurePeriod;
		}

		void Update()
		{
			CalculatePingRate(); 
			CalculateFpsRate();
		}

		void CalculatePingRate() 
		{
			_pingRateText.text = "Ping: " + PhotonNetwork.GetPing();
		}

		void CalculateFpsRate() 
		{
			_fpsAccumulator++;

        	if (Time.realtimeSinceStartup > _fpsNextPeriod)
        	{
            	_currentFps = (int)(_fpsAccumulator / _fpsMeasurePeriod);
            	_fpsAccumulator = 0;
            	_fpsNextPeriod += _fpsMeasurePeriod;
        	}
			
			_fpsRateText.text = "Fps: " + _currentFps.ToString("000");
		}
	}
}

