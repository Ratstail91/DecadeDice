using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using TMPro;

public class LobbySceneManager : MonoBehaviour, ISteamMatchmakingServerListResponse { //I need a stiff drink
	//inspector members
	[SerializeField]
	GameObject lobbyPanelPrefab;

	[SerializeField]
	Transform ListContentTransform;

	//internal members
	//

	//lifetime members
	void OnEnable() {
		//

		//kick it off
		SearchForServers();
	}

	void OnDisable() {
		CleanUp();
	}

	void Update() {
		//monitor the results
		//
	}

	//public members
	public void SearchForServers() {
		//Instantiate(lobbyPanelPrefab, ListContentTransform);

		Debug.Log("Searching for servers");

//		MatchMakingKeyValuePair_t[] filters = new MatchMakingKeyValuePair_t[1];
//		filters[0].m_szKey = "secure";

		HServerListRequest serverListRequest = SteamMatchmakingServers.RequestLANServerList(SteamUtils.GetAppID(), this); //"this"... motherf*cker

		if (serverListRequest == HServerListRequest.Invalid) {
			Debug.Log("Request is invalid");
		}

		Debug.Log(serverListRequest);

		int serverCount = SteamMatchmakingServers.GetServerCount(serverListRequest);
		Debug.Log("Servers found: " + serverCount);

		for(int i = 0; i < serverCount; i++) {
			gameserveritem_t gs = SteamMatchmakingServers.GetServerDetails(serverListRequest, i);
			Debug.Log("Server name: " + gs.GetServerName());
		}

		SteamMatchmakingServers.ReleaseRequest(serverListRequest);
	}

	public void CleanUp() {
		//TODO: release the request
	}

	//callbacks
	void OnServerResponse(HServerListRequest hRequest, int iServer) {
		Debug.Log("OnServerResponse called");
	}

	void OnServerFailedToRespond(HServerListRequest hRequest, int iServer) {
		Debug.Log("OnServerFailedToRespond called");
	}

	void OnRefreshComplete(HServerListRequest hRequest, EMatchMakingServerResponse response) {
		Debug.Log("OnRefreshComplete called");
	}
}