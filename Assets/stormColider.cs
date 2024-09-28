using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stormColider : MonoBehaviour
{
    StarterAssets.ThirdPersonController tpc;
    GameObject Player;
    AudioSource stormSound;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("PlayerArmature");
        tpc = Player.GetComponent<StarterAssets.ThirdPersonController>();
        stormSound = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
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
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
            stormSound.Play();
            // Start the invokes again
            InvokeRepeating("heatconter", 0, 7);
            InvokeRepeating("coldCounter", 0,16);
        }
	}
	private void OnTriggerExit(Collider other)
	{
        stormSound.Stop();
	}

	public void heatconter()
    {
        if (tpc.cold >= 0)
        {
            tpc.cold++; // Decrease cold value over time
        }
    }

    public void coldCounter()
    {
        if (tpc.PlayerState <= 4)
        {
            tpc.PlayerState--; // Increase player state as cold builds up
        }
    }
}

