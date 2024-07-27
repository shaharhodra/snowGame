using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManeger : MonoBehaviour
{
    
    StarterAssets.ThirdPersonController tpc;
    [SerializeField] GameObject Player;
    int appelCount=0;
    public int coldCount=0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        tpc = Player.GetComponent<StarterAssets.ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
		if (tpc.cold >= 10)
		{

			CancelInvoke("heatconter");
		}
		if (tpc.PlayerState==1)
		{
            CancelInvoke("coldCounter");
		}
	}
	
	public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("appel"))
        {

            appelCount++;
			if (tpc.PlayerState<=4&& appelCount==2)
			{
                tpc.cold--;
                tpc.PlayerState++;
                appelCount = 0;
            }
           
        }
        if (other.gameObject.CompareTag("shack"))
        {
            tpc.iscold = false;
            InvokeRepeating("heatconter", 0, 2);

            InvokeRepeating("coldCounter", 0, 2);
        }





    }

	private void OnTriggerExit(Collider other)
	{
        if (other.gameObject.CompareTag("shack"))
        {
            tpc.iscold = true;

            CancelInvoke("heatconter");
            CancelInvoke("coldCounter");
         
        }

    }
	public void heatconter()
    {
        
		if (tpc.cold>0)
		{
            tpc.cold--;
             
			
        }
       
       
		
	}
    public void coldCounter()
	{
        if (tpc.PlayerState<4)
        {
            tpc.PlayerState++;
        }
    }
 
    

}
