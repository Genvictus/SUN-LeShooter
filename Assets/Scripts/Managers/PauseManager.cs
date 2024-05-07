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
			canvas.enabled = !canvas.enabled;
			Pause();
		}
	}

	public void Pause()
	{
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		Lowpass();
        EventManager.TriggerEvent("Pause", canvas.enabled);
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
		SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
	}
}
