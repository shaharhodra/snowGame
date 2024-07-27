using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clif : MonoBehaviour
{
	[SerializeField] GameObject player;
	
	[SerializeField]Transform newpos;

	private void Start()
	{
		
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			player.SetActive(false);
			player.transform.position = newpos.position;
			player.SetActive(true);
		}
	}
}
