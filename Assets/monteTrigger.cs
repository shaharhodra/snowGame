using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monteTrigger : MonoBehaviour
{
	
	public GameObject[] snow;
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			for (int i = 0; i < snow.Length; i++)
			{
				snow[i].GetComponent<Rigidbody>().isKinematic = false;
			}
		
		}
	}
}
