using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class camshackcolaider : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find(" shackcamera").GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	private void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag("Player"))
        {
            cam.Priority = 11;
            
        }

    }
	private void OnTriggerExit(Collider other)
	{
        if (other.CompareTag("Player"))
        {
            cam.Priority = 1;

        }
    }
}
