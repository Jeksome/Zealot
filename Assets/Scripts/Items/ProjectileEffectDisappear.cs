using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEffectDisappear : MonoBehaviour {

	private float lifeTime = 1.2f;

    private void OnEnable()
    {
		StartCoroutine(Disappear());
    }
    private IEnumerator Disappear()
	{
		yield return new WaitForSeconds(lifeTime);
		gameObject.SetActive(false);
	}
}
