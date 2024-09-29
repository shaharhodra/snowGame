using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class camshackcolaider : MonoBehaviour
{
    CinemachineFreeLook cam;
    StarterAssets.ThirdPersonController tpc;
    GameObject Player;
     
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("PlayerArmature");
        tpc = Player.GetComponent<StarterAssets.ThirdPersonController>();

        cam = GameObject.Find("shackcamera").GetComponent<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	private void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag("Player"))
        {
            tpc.move = false;
            cam.Priority = 11;
            Invoke("back", 3);
        }

    }
	//private void OnTriggerExit(Collider other)
	//{
 //       if (other.CompareTag("Player"))
 //       {
 //           cam.Priority = 1;

 //       }
 //   }
    void back()
	{
        cam.Priority = 1;
        tpc.move = true;

    }
}
