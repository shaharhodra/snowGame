using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onPlayerJump : MonoBehaviour
{
    GameObject Player;
    Transform newpos;

    // Start is called before the first frame update
    void Start()
    {
        newpos = GameObject.Find("riverpos").GetComponentInChildren<Transform>();
        Player = GameObject.Find("PlayerArmature");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnTriggerEnter(Collider other)
	{
		if (other==Player)
		{
            Player.SetActive(false);
            Player.transform.position = newpos.position;
            Player.SetActive(true);
        }
	}
}
