using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupSceneManager : MonoBehaviour {
	public void EnterLobbyButton() {
		Debug.Log("Enter lobby");
	}

	public void OptionsButton() {
		Debug.Log("Options");
	}

	public void	QuitButton() {
		Application.Quit(); //NOTE: doesn't work in editor mode
		Debug.Log("Quit");
	}
}