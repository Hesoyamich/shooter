using UnityEngine;
using System.Collections;

public class DestroyEffect : MonoBehaviour {

	private void Start() {
		StartCoroutine(KillEffect());
	}

	IEnumerator KillEffect()
	{
		yield return new WaitForSeconds(2f);
		Destroy(gameObject);
	}
}
