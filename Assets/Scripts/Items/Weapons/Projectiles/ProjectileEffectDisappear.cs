using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEffectDisappear : MonoBehaviour {

	private readonly float lifeTime = 1.95f;

	#pragma warning disable 0649
	[SerializeField] private List<AudioClip> clips = new List<AudioClip>();
	#pragma warning restore 0649

	private AudioSource source;

	private void OnEnable()
	{
		source = GetComponent<AudioSource>();
		StartCoroutine(Disappear());
	}

    private IEnumerator Disappear()
	{
		for (int i = 0; i < clips.Count; i++)
        {
			source.PlayOneShot(clips[i]);
			source.priority -= 20;
        }
		
		yield return new WaitForSeconds(lifeTime);
		gameObject.SetActive(false);
	}
}
