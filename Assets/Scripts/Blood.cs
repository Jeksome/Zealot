using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour {

	private float lifeTime = 1.0f;

	private IEnumerator Start()
	{
		yield return new WaitForSeconds(lifeTime);
		this.gameObject.SetActive(false);
	}
}
