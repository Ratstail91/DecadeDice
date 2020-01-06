using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPanel : MonoBehaviour {
	public void JoinButton() {
		GameObject go = GameObject.Find("LobbySceneManager");

		if (go == null) {
			throw new NullReferenceException("Failed to find LobbySceneManager");
		}

//		go.GetComponent<LobbySceneManager>().JoinLobby(transform.GetSiblingIndex());
	}
}