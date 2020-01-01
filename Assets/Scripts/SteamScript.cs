using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

class SteamScript : MonoBehaviour {
	void Start() {
		if (SteamManager.Initialized) {
			string name = SteamFriends.GetPersonaName();
			Debug.Log(name);
		}
	}
}