using UnityEngine;

public class KeyPressCounter : MonoBehaviour
{
    [SerializeField] GameObject fire;
    [SerializeField] int presscont;
    public float conter = 1.0f;
    GameObject steam;
    GameObject Steame;
    private float timer = 0f;
    [SerializeField] private float decrementInterval = 5f; // 5 seconds interval
    GameObject Player;
    StarterAssets.ThirdPersonController tpc;
    bool fireOn;
    bool activate;
    bool activSet;

    void Start()
    {
        // Initial setup
        Player = GameObject.Find("PlayerArmature");
        tpc = Player.GetComponent<StarterAssets.ThirdPersonController>();
        Steame = GameObject.Find("Steamer");
        steam = GameObject.Find("Steam");
        Steame.SetActive(false);
        steam.SetActive(false);
        fireOn = false;
        activate = false;
    }

    void Update()
    {
        // Increment when F is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            tpc._animator.SetBool("blow", true);
            presscont++;
            conter += 0.1f; // Increment conter with each key press
        }
        else
        {
            tpc._animator.SetBool("blow", false);
        }

        // Decrement counter every decrementInterval seconds
        timer += Time.deltaTime;
        if (timer >= decrementInterval)
        {
            if (presscont > 0)
            {
                presscont--;
            }
            timer = 0f; // Reset timer
        }

        // Manage fire and steam based on press count
        if (presscont == 20)
        {
            fire.SetActive(true);
            Steame.SetActive(true);
            activate = true;

            // Start heat/cold counters
            if (activSet && !IsInvoking("heatconter"))
            {
                InvokeRepeating("heatconter", 0, 2);
                InvokeRepeating("coldCounter", 0, 10);
            }
        }

        // Steam activation
        steam.SetActive(presscont > 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tpc._animator.SetBool("warming", true);
            activSet = true;

            // Start the InvokeRepeating methods again when the player re-enters
            if (activate && activSet)
            {
                // Cancel any ongoing invokes to avoid multiple calls stacking up
                CancelInvoke("heatconter");
                CancelInvoke("coldCounter");

                // Start the invokes again
                InvokeRepeating("heatconter", 0, 2);
                InvokeRepeating("coldCounter", 0, 10);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tpc._animator.SetBool("warming", false);
            activSet = false;

            // Cancel the invoke methods when the player exits the trigger zone
            CancelInvoke("heatconter");
            CancelInvoke("coldCounter");
        }
    }


    public void heatconter()
    {
        if (tpc.cold > 0)
        {
            tpc.cold--; // Decrease cold value over time
        }
    }

    public void coldCounter()
    {
        if (tpc.PlayerState < 4)
        {
            tpc.PlayerState++; // Increase player state as cold builds up
        }
    }
}
