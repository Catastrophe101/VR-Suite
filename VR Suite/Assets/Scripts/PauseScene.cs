using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class PauseScene : MonoBehaviour {

	public bool _paused = false;

	public void PauseGame()
	{
		SceneManager.LoadScene ("text", LoadSceneMode.Additive);
	}


	public void ResumeGame()
	{
		SceneManager.UnloadScene ("text");
	}
	

}
