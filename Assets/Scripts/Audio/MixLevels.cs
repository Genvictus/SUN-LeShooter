using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour
{
	public AudioMixer masterMixer;

	private static string keySfx = "sfx";
	private static string keyBgm = "bgm";
	public static float sfx => PlayerPrefs.HasKey(keySfx) ? PlayerPrefs.GetFloat(keySfx) : -10;
	public static float bgm => PlayerPrefs.HasKey(keyBgm) ? PlayerPrefs.GetFloat(keyBgm) : -10;

	public void SetSfxLvl(float sfxLvl)
	{
		masterMixer.SetFloat("sfxVol", sfxLvl);
		PlayerPrefs.SetFloat(keySfx, sfxLvl);
	}

	public void SetMusicLvl(float bgmLvl)
	{
		masterMixer.SetFloat("musicVol", bgmLvl);
		PlayerPrefs.SetFloat(keyBgm, bgmLvl);
	}
}
