using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class river : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform newpos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
            player.SetActive(false);
            player.transform.position = newpos.position;
            player.SetActive(true);
        }
	}
}
