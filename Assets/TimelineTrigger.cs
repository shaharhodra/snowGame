using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerTimeline : MonoBehaviour
{
    // Reference to the PlayableDirector component
    public PlayableDirector timelineDirector;  // Drag your PlayableDirector object here in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player (using tag or layer)
        if (other.CompareTag("Player"))  // Make sure your player object has the "Player" tag
        {
            // Play the timeline when the player enters the trigger
            timelineDirector.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Optional: Stop or rewind the timeline when the player exits the trigger
        if (other.CompareTag("Player"))
        {
            // You can stop the timeline when the player leaves the area if you want
            // timelineDirector.Stop();
        }
    }
}

