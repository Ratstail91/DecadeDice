using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbySceneManager : MonoBehaviour { //I need a stiff drink
	//inspector members
	[SerializeField]
	GameObject lobbyPanelPrefab;

	[SerializeField]
	Transform ListContentTransform;

	//internal members
	//

	//lifetime members
	void OnEnable() {
		//kick it off
		SteamMatchmakingServerListResponse.SearchForServers();
	}
}