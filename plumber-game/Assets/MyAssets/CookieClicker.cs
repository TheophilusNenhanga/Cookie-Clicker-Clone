using System;
using TMPro;
using UnityEngine;

namespace MyAssets {
	public class CookieClicker : MonoBehaviour {
		private int _cookieCount;
		private double _cookieRate;
		private double _lastCookieTime;

		[SerializeField] private double timeForCookie;

		[SerializeField] private TMP_Text cookieText;
		[SerializeField] private TMP_Text cookieRateText;

		private void Start() {
			_cookieCount = 0;
			_cookieRate = 0;
			_lastCookieTime = 0;
		}

		void Update() {
			if (Input.GetMouseButtonDown(0)) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

				if (hit.collider) {
					IncreaseCookies(1);
				}
			}

			if (_cookieCount != 0 && _cookieCount % 25 == 0) {
				UpdateCookieRate(this._cookieRate + 0.1);
			}

			timeForCookie = 1 / _cookieRate;

			if (_lastCookieTime == 0) {
				// the first cookie
				_lastCookieTime = Time.timeAsDouble;
			}

			double cookieTime = Time.timeAsDouble;
			if (cookieTime - _lastCookieTime >= timeForCookie) {
				IncreaseCookies(1);
				_lastCookieTime = Time.timeAsDouble;
			}
		}

		private void IncreaseCookies(int increment) {
			_cookieCount += increment;
			cookieText.text = _cookieCount == 1 ? $"{_cookieCount} cookie" : $"{_cookieCount} cookies";
		}

		private void UpdateCookieRate(double newRate) {
			this._cookieRate = newRate;
			cookieRateText.text = $"{Math.Round(_cookieRate, 3)} cookies per second";
		}
	}
}