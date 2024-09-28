using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colectItem : MonoBehaviour
{
    GameObject colect;
    UiTrigger uiTrigger;
    // Start is called before the first frame update
    void Start()
    {

        uiTrigger = GameObject.Find("ui").GetComponent<UiTrigger>();
        colect = GameObject.Find("colider");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
            uiTrigger.colected = false;
            Destroy(this.gameObject);

            colect.GetComponent<BoxCollider>().isTrigger = true;
		}
	}
}
