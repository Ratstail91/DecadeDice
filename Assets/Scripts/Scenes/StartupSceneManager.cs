using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupSceneManager : MonoBehaviour {
	public void EnterLobbyButton() {
		SceneManager.LoadScene("LobbyScene");
	}

	public void OptionsButton() {
		Debug.Log("Options");
	}

	public void	QuitButton() {
		Application.Quit(); //NOTE: doesn't work in editor mode
		Debug.Log("Quit");
	}
}