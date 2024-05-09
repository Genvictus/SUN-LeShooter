using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour
{
	public AudioMixer masterMixer;

	public static float sfx;
	public static float bgm;

	public void SetSfxLvl(float sfxLvl)
	{
		masterMixer.SetFloat("sfxVol", sfxLvl);
		sfx = sfxLvl;
	}

	public void SetMusicLvl(float musicLvl)
	{
		masterMixer.SetFloat("musicVol", musicLvl);
		bgm = musicLvl;
	}
}
