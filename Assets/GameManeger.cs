using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManeger : MonoBehaviour
{
  
     GM gm;
	StarterAssets.ThirdPersonController tpc;
	GameObject Player;


	// Start is called before the first frame update
	void Start()
    {
		Player = GameObject.Find("PlayerArmature");
       tpc = Player.GetComponent<StarterAssets.ThirdPersonController>();
       gm= GameObject.FindObjectOfType<GM>();
    }

    // Update is called once per frame
    void Update()
    {
		
	}
	
	public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("appel"))
        {
            gm.apple();
 
           
        }
		if (other.gameObject.CompareTag("shack"))
		{
			gm.shack();
		
		}





	}


	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("shack"))
		{
			gm.GoOut();
			//tpc.iscold = true;

			//CancelInvoke("heatconter");
			//CancelInvoke("coldCounter");

		}

   }
		//public void heatconter()
		//   {

		//	if (tpc.cold>0)
		//	{
		//           tpc.cold--;


		//       }



		//}
		//   public void coldCounter()
		//{
		//       if (tpc.PlayerState<4)
		//       {
		//           tpc.PlayerState++;
		//       }
		//   }



	}
