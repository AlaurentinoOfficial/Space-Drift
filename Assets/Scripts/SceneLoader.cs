using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	public void LoadScene(string scene) {
		if(scene.ToLower() == "exit")
			Application.Quit();
		else
			SceneManager.LoadScene(scene);
	}
}
