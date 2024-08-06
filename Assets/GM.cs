using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
	StarterAssets.ThirdPersonController tpc;
	[SerializeField] GameObject Player;
	int appelCount = 0;
	public int coldCount = 0;
	private void Start()
	{
		tpc = Player.GetComponent<StarterAssets.ThirdPersonController>();

	}
	private void Update()
	{
		if (tpc.cold >= 10)
		{

			CancelInvoke("heatconter");
		}
		if (tpc.PlayerState == 1)
		{
			CancelInvoke("coldCounter");
		}
	}
	public void apple()
	{
		appelCount++;
		if (tpc.PlayerState <= 4 && appelCount == 2)
		{
			tpc.cold--;
			tpc.PlayerState++;
			appelCount = 0;
		}
	}
	public void shack()
	{
		tpc._animator.SetBool("warming",true);

		tpc.iscold = false;
		InvokeRepeating("heatconter", 0, 2);
		tpc.MoveSpeed = 0f;
		//stop moving bool
		//tpc.move = false;
		InvokeRepeating("coldCounter", 0, 2);
	}
	public void GoOut()
	{
		tpc.iscold = true;
		tpc._animator.SetBool("warming", false);
		CancelInvoke("heatconter");
		CancelInvoke("coldCounter");
	}
	public void heatconter()
	{

		if (tpc.cold > 0)
		{
			tpc.cold--;


		}



	}
	public void coldCounter()
	{
		if (tpc.PlayerState < 4)
		{
			tpc.PlayerState++;
		}
	}


}
