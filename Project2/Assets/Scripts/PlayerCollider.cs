using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour
{
	public int hitEnemy = 0;


	void Update()
	{
		if (hitEnemy >= 3)
		{
			// Maybe not do anything and just display how many people you've hit??
			Debug.Log("You've hit at least three enemies!");
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "SpearPoint")
		{
			Debug.Log("You hit them!");
			hitEnemy = hitEnemy + 1;
			if (hitEnemy == 3)
				GameManager.instance.GameOver();
		}
	}
}
