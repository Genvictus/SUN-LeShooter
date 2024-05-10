using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour
{

	public AudioMixerSnapshot paused;
	public AudioMixerSnapshot unpaused;

	Canvas canvas;

	void Start()
	{
		canvas = GetComponent<Canvas>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			TogglePauseMenu();
		}
	}

	public void SetGameOverPause(bool pauseValue = true)
	{
		Debug.Log("Set GameOver Pause to " + pauseValue.ToString());
		EventManager.TriggerEvent("GameOverPause", pauseValue);
	}

	public void SetPause(bool pauseValue = true)
	{
		Debug.Log("Set Pause to " + pauseValue.ToString());
		EventManager.TriggerEvent("Pause", pauseValue);


		// if setting paused to false, unfreeze time 
		if (!pauseValue)
		{
			Time.timeScale = 1;
			Lowpass();
		}
	}

	public void TogglePauseMenu()
	{
		// toggles pause based on canvas enabled or not
		canvas.enabled = !canvas.enabled;
		GetComponent<CanvasGroup>().interactable = !GetComponent<CanvasGroup>().interactable;

		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		Lowpass();
		SetPause(canvas.enabled);
	}

	void Lowpass()
	{
		if (Time.timeScale == 0)
		{
			paused.TransitionTo(.01f);
		}

		else

		{
			unpaused.TransitionTo(.01f);
		}
	}

	public void Quit()
	{
		SetPause(false);
		SetGameOverPause(true);

		SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
	}
}
