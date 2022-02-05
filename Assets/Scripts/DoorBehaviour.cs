using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] public bool safe;
    [SerializeField] private bool hot;
    [SerializeField] private bool noisy;
    [SerializeField] private GameObject doorHandle;
    [SerializeField] private Material hotMaterial;
    [SerializeField] private AudioSource audioSource;
     [SerializeField] private TMPro.TextMeshPro safetyText;


    //Doors start disabled and then are enabled by the door manager after the parameters have been changed
    void OnEnable()
    {
        Renderer handleRenderer = doorHandle.GetComponent<Renderer>();

        if(hot)
        {
            handleRenderer.material = hotMaterial;
        }
        if(noisy)
        {
            audioSource.Play();
        }
        if(safe)
        {
            safetyText.text = "Safe";
        }
    }

    public void setHot(bool p_hot)
    {
        hot = p_hot;
    }

    public bool getHot()
    {
        return hot;
    }

     public void setNoisy(bool p_noisy)
    {
        hot = p_noisy;
    }

    public bool getNoisy()
    {
        return noisy;
    }

     public void setSafe(bool p_safe)
    {
        hot = p_safe;
    }

    public bool getSafe()
    {
        return safe;
    }



    
}
