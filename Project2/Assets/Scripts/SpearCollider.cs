using UnityEngine;
using System.Collections;

public class SpearCollider : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		var hit = GetComponent<Collider>().gameObject;
		var health = hit.GetComponent<Health>();

		if ((other.gameObject.tag == "Player"))
		{
			Debug.Log("You got stabbed!");
			//health.TakeDamage(10);
		}
		if (health = null)
			GameManager.instance.GameOver();
	}
}

