using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shackarea : MonoBehaviour
{
    StarterAssets.ThirdPersonController tpc;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerArmature");
        tpc = player.GetComponent<StarterAssets.ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	private void OnTriggerStay(Collider other)
	{
        if (other.CompareTag("Player"))
        {
            tpc.PlayerState = 4;
           // tpc.cold = 5;
            tpc.reduse = false;
        }
    }
	private void OnTriggerExit(Collider other)
	{
        if (other.CompareTag("Player"))
        {
            tpc.reduse = true;
        }
    }

}
