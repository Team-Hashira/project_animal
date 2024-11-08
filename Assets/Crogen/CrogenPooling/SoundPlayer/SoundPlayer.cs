using System;
using System.Collections;
using UnityEngine;
using Crogen.CrogenPooling;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class SoundPlayer : MonoBehaviour, IPoolingObject
{
	[field:SerializeField] public AudioSource AudioSource { get; private set; }
	
	public string OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	public void SetAudioResource(AudioResource audioResource, bool loop = false)
	{
		AudioSource.resource = audioResource;
		AudioSource.Play();
		if(loop == false)
			StartCoroutine(CoroutineOnPlay());
	}

	public void OnPop()
	{
	}

	public void OnPush()
	{
		StopAllCoroutines();
	}

	private IEnumerator CoroutineOnPlay()
	{
		yield return new WaitWhile(()=>AudioSource.isPlaying);
		this.Push();
	}
}
