using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTrigger : MonoBehaviour
{
   GameObject text;
    public bool colected;
    // Start is called before the first frame update
    void Start()
    {
        colected = true;
        text = GameObject.Find("go back to cabin");
        text.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
    }
	private void OnTriggerEnter(Collider other)
	{
		if (colected)
		{
            text.SetActive(true);

        }

    }
    private void OnTriggerExit(Collider other)
	{
        text.SetActive(false);

    }
}
