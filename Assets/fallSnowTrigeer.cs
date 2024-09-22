using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallSnowTrigeer : MonoBehaviour
{
GameObject box;
	
	private void Start()
	{
		box = GameObject.Find("fallSnow");
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			box.GetComponent<Rigidbody>().isKinematic = false;
		}
	}
}
