using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using TMPro;

public class LobbySceneManager : MonoBehaviour {
	//inspector members
	[SerializeField]
	GameObject lobbyPanelPrefab;

	[SerializeField]
	Transform ListContentTransform;

	//internal members
	Callback<LobbyCreated_t> lobbyCreatedCallback;
	Callback<LobbyMatchList_t> lobbyListCallback;
	Callback<LobbyDataUpdate_t> lobbyInfoCallback;
	Callback<LobbyEnter_t> lobbyEnterCallback;

	ulong currentLobbyId = 0;

	void OnEnable() {
		lobbyCreatedCallback = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
		lobbyListCallback = Callback<LobbyMatchList_t>.Create(OnGetLobbiesList);
		lobbyInfoCallback = Callback<LobbyDataUpdate_t>.Create(OnGetLobbyInfo);
		lobbyEnterCallback = Callback<LobbyEnter_t>.Create(OnLobbyEntered);

		//kick it off
		SearchForLobbies();
	}

	void Update() {
		//monitor the results
		//
	}

	//public members
	public void SearchForLobbies() {
		//Instantiate(lobbyPanelPrefab, ListContentTransform);

		SteamMatchmaking.RequestLobbyList();
	}

	public void CreateLobby() {
		SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeInvisible, 8); //TODO: switch to public
	}

	public void JoinLobby(int childIndex) {
		Debug.Log($"Trying to join {SteamMatchmaking.GetLobbyData(SteamMatchmaking.GetLobbyByIndex(childIndex), "name")}");
		SteamMatchmaking.LeaveLobby((CSteamID)currentLobbyId);
		SteamMatchmaking.JoinLobby(SteamMatchmaking.GetLobbyByIndex(childIndex));
	}

	//callbacks
	void OnLobbyCreated(LobbyCreated_t result) {
		//creates the new lobby and names it
		if (result.m_eResult == EResult.k_EResultOK) {
			Debug.Log("Lobby created!");
		} else {
			Debug.Log("Failed to create a lobby");
		}

		string personaName = SteamFriends.GetPersonaName();
		SteamMatchmaking.SetLobbyData((CSteamID)result.m_ulSteamIDLobby, "name", $"{personaName}'s game");
	}

	void OnGetLobbiesList(LobbyMatchList_t result) {
		//deletes the existing list of lobbies and requests info on the new lobbies
		Debug.Log("Found " + result.m_nLobbiesMatching + " lobbies!");

		//delete existing children
		foreach (Transform child in ListContentTransform) {
			Destroy(child.gameObject);
		}

		//request new info
		for(int i = 0; i < result.m_nLobbiesMatching; i++) {
			SteamMatchmaking.RequestLobbyData(SteamMatchmaking.GetLobbyByIndex(i));
		}
	}

	void OnGetLobbyInfo(LobbyDataUpdate_t result) {
		Debug.Log("Info received" + result.m_ulSteamIDLobby);

		//save the lobby data, populate the display
		GameObject go = Instantiate(lobbyPanelPrefab, ListContentTransform);

		//populate the contents of the game object (TODO: have a lobby-specific script ready)
		go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = SteamMatchmaking.GetLobbyData((CSteamID)result.m_ulSteamIDLobby, "name");
//		go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = SteamMatchmaking.GetLobbyData((CSteamID)result.m_ulSteamIDLobby, "population");
	}

	void OnLobbyEntered(LobbyEnter_t result) {
		if (result.m_EChatRoomEnterResponse == 1) {
			currentLobbyId = result.m_ulSteamIDLobby;

			Debug.Log("Current lobby: " + currentLobbyId);

			Debug.Log("Joined a lobby!");

			//TODO: switch scenes to the chat system
		} else {
			Debug.Log("Failed to join a lobby");
		}
	}
}