using System.Collections;
using UnityEngine;

public class ProjectileEffectDisappear : MonoBehaviour {

	private readonly float lifeTime = 1.2f;

    private void OnEnable() => StartCoroutine(Disappear());

    private IEnumerator Disappear()
	{
		yield return new WaitForSeconds(lifeTime);
		gameObject.SetActive(false);
	}
}
