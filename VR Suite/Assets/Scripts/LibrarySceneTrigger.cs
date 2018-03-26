using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LibrarySceneTrigger : MonoBehaviour {
public void LoadByIndex(int SI)
{
	SceneManager.LoadScene(SI);
}
}