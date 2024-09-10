using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apple : MonoBehaviour
{
	[SerializeField] Animation anim;
	[SerializeField]AudioSource bite;
	StarterAssets.ThirdPersonController tpc;
	GameObject Player;
	private void Start()
	{
		Player = GameObject.Find("PlayerArmature");
		tpc = Player.GetComponent<StarterAssets.ThirdPersonController>();
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))

		{
			tpc._animator.SetBool("grab", true);
			bite.Play();
			Invoke("destroyaple", .5f);
		}
	}
	void destroyaple()
	{
		tpc._animator.SetBool("grab", false);

		Destroy(gameObject);

	}
}
