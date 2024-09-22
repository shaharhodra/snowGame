using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class postProssControler : MonoBehaviour
{
    public PostProcessVolume volume;  // Reference to the Post Processing Volume
    private Bloom bloom;              // Reference to the Bloom effect
    private DepthOfField DepthOfField;  // Reference to the DepthOfField effect
    private ChromaticAberration ChromaticAberration;//reference to the ChromaticAberration effect
    StarterAssets.ThirdPersonController tpc;
    GameObject player;
    void Start()
    {
        volume = GameObject.Find("Global Volume player").GetComponent<PostProcessVolume>();

        player = GameObject.Find("PlayerArmature");
      
        tpc = player.GetComponent<StarterAssets.ThirdPersonController>();
        // Check if the volume has a Bloom effect and get a reference to it
        if (volume.profile.TryGetSettings(out bloom))
        {
            // Initial setup or debugging if needed
            Debug.Log("Bloom effect found.");
        }
        else
        {
            Debug.LogError("Bloom effect not found.");
        }
        if (volume.profile.TryGetSettings(out DepthOfField))
        {
            // Initial setup or debugging if needed
            Debug.Log("DepthOfField effect found.");
        }
        else
        {
            Debug.LogError("DepthOfField effect not found.");
        }
        if (volume.profile.TryGetSettings(out ChromaticAberration))
        {
            // Initial setup or debugging if needed
            Debug.Log("ChromaticAberration effect found.");
        }
        else
        {
            Debug.LogError("ChromaticAberration effect not found.");
        }
    }

    void Update()
    {
        // Example: Change the Bloom intensity based on input
        if (tpc.PlayerState ==1|| tpc.PlayerState == 0)
        {
            Debug.Log("fall blur");
            SetBloomIntensity(bloom.intensity.value = 5) ;
            SetDepthOfFieldintensity(DepthOfField.focalLength.value = 150);
            setChromaticAberrationEffect(ChromaticAberration.intensity.value = 1);
        }
		 else if (tpc.PlayerState==2)
		{
            Debug.Log("half blur");
            SetBloomIntensity(bloom.intensity.value = 2);
            SetDepthOfFieldintensity(DepthOfField.focalLength.value = 80);
            setChromaticAberrationEffect(ChromaticAberration.intensity.value = .5f);
        }
		 else if (tpc.PlayerState==3)
		{
            Debug.Log("qurter blur");
            SetBloomIntensity(bloom.intensity.value = 1);
            SetDepthOfFieldintensity(DepthOfField.focalLength.value = 20);
            setChromaticAberrationEffect(ChromaticAberration.intensity.value = .2f);
        }
        else
        {
            SetBloomIntensity(bloom.intensity.value =0);
            SetDepthOfFieldintensity(DepthOfField.focalLength.value = 0);
            setChromaticAberrationEffect(ChromaticAberration.intensity.value = 0);

        }
    }

    public void SetBloomIntensity(float intensity)
    {
        if (bloom != null)
        {
            bloom.intensity.value = Mathf.Clamp(intensity, 0f, 10);  // Clamp the value to avoid extreme values
        }
    }
    public void SetDepthOfFieldintensity(float focalLength)
	{
		if (DepthOfField != null )
		{
            DepthOfField.focalLength.value = Mathf.Clamp(focalLength, 0f,300f);
        }
	}
    public void setChromaticAberrationEffect(float intensity)
	{
		if (ChromaticAberration !=null)
		{
            ChromaticAberration.intensity.value = Mathf.Clamp(intensity, 0f, 1f);
		}
	}
   
}
