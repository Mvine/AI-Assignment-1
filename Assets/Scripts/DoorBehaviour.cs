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





    // Start is called before the first frame update

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

        //using the data from the spreadsheet, randomize the data here. Better yet find a way to do it on scene load
    }
    
    // Update is called once per frame
    void Update()
    {
        //I don't think I need this
    }
}
