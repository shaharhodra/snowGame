using UnityEngine;

public class KeyPressCounter : MonoBehaviour
{
    GameObject fire;
    [SerializeField] int presscont;
    public float conter = 1.0f;
    GameObject steam;
    GameObject Steame;
    private float timer = 0f;
   [SerializeField] private float decrementInterval = 5f; // 5 seconds interval
    GameObject Player;
    StarterAssets.ThirdPersonController tpc;
    bool fireOn;
  //  GM gm;
    // Start is called before the first frame update
    void Start()
    {
      //  gm = GameObject.Find("gameManeger").GetComponent<GM>();
        Player = GameObject.Find("PlayerArmature");
        tpc = Player.GetComponent<StarterAssets.ThirdPersonController>();
        Steame = GameObject.Find("Steamer");
        fire = GameObject.Find("fire");
        steam = GameObject.Find("Steam");
        Steame.SetActive(false);
        fire.SetActive(false);
        steam.SetActive(false);
        fireOn = false;
        // Initialization code if needed
    }

    // Update is called once per frame
    void Update()
    {
       
        // Increment when F is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            tpc._animator.SetBool("blow", true);
            presscont++;
            conter += 0.1f; // Example: increment 'conter' by 0.1f each time 'F' is pressed
        }
		else
		{
            tpc._animator.SetBool("blow", false);

        }


        // Timer logic to decrement presscont every 5 seconds
        timer += Time.deltaTime;
        if (timer >= decrementInterval)
        {
            if (presscont > 0) // Ensure presscont doesn't go below 0
            {
                presscont--;
            }
            timer = 0f; // Reset the timer after decrement
        }
		if (presscont==20)
		{
            fire.SetActive(true);
            Steame.SetActive(true);
        }
        if (presscont>5)
		{
            steam.SetActive(true);

        }
		else
		{
            steam.SetActive(false);

        }
	
       


    }
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
            tpc._animator.SetBool("warming", true);
		}
	}
	private void OnTriggerExit(Collider other)
	{
        if (other.CompareTag("Player"))
        {
            tpc._animator.SetBool("warming", false);
        }
    }
   
   

}
