using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamMatchmakingServerListResponse : ISteamMatchmakingServerListResponse {
	//singleton members
	public static SteamMatchmakingServerListResponse Instance {
		get {
			if (_self != null) {
				return _self;
			}
			return _self = new SteamMatchmakingServerListResponse();
		}
	}
	static SteamMatchmakingServerListResponse _self = null;

	//internal members
	static HServerListRequest serverListRequest = HServerListRequest.Invalid;

	//constructor/destructor
	SteamMatchmakingServerListResponse() : base(ServerResponded, ServerFailedToRespond, RefreshComplete) {
		//
	}

	~SteamMatchmakingServerListResponse() {
		ReleaseRequest();
	}

	//public members
	public static void SearchForServers() {
		Debug.Log("Searching for servers");

		MatchMakingKeyValuePair_t[] filters = new MatchMakingKeyValuePair_t[2];

		filters[0].m_szKey = "secure";
		filters[0].m_szValue = "1";

		filters[1].m_szKey = "gamedir";
		filters[1].m_szValue = "decadedice";

		serverListRequest = SteamMatchmakingServers.RequestInternetServerList(SteamUtils.GetAppID(), filters, 2, Instance);

		if (serverListRequest == HServerListRequest.Invalid) {
			Debug.Log("Request is invalid");
		}
	}

	static void ReleaseRequest() {
		SteamMatchmakingServers.ReleaseRequest(serverListRequest);
		serverListRequest = HServerListRequest.Invalid;
	}

	public static void ListServers() {
		int serverCount = SteamMatchmakingServers.GetServerCount(serverListRequest);
		Debug.Log("Servers found: " + serverCount);

		for(int i = 0; i < serverCount; i++) {
			gameserveritem_t gs = SteamMatchmakingServers.GetServerDetails(serverListRequest, i);
			Debug.Log("Server name: " + gs.GetServerName());
		}
	}

	//callbacks
	static void ServerResponded(HServerListRequest hRequest, int iServer) {
		Debug.Log("ServerResponded called");

//		ListServers(); //TODO: store the server data
	}

	static void ServerFailedToRespond(HServerListRequest hRequest, int iServer) {
		Debug.Log("ServerFailedToRespond called");
	}

	static void RefreshComplete(HServerListRequest hRequest, EMatchMakingServerResponse response) {
		Debug.Log("RefreshComplete called");

		ReleaseRequest();
	}
}