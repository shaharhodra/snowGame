using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallSnowTrigeer : MonoBehaviour
{
	[SerializeField] GameObject box;
	
	private void Start()
	{
		
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			box.GetComponent<Rigidbody>().isKinematic = false;
		}
	}
}
