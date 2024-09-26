using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anatherturn : MonoBehaviour
{
    camtregger triger;
    GameObject Player;
    bool newpus;
    StarterAssets.ThirdPersonController tpc;

    // Start is called before the first frame update
    void Start()
    {
        newpus = false;
        triger = GameObject.Find("cameratregger").GetComponent<camtregger>();
        Player = GameObject.Find("PlayerArmature");
        tpc = Player.GetComponent<StarterAssets.ThirdPersonController>();

    }

    // Update is called once per frame
    void Update()
    {
		if (newpus)
		{
            Player.transform.rotation = Quaternion.Euler(00, -120, 00);
            tpc.MoveSpeed = 2;
            tpc.JumpHeight = 0;
        }
    }
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
            triger.side = false;
            newpus = true;
           

        }

    }
	private void OnTriggerExit(Collider other)
	{
        newpus = false;
	}
}
