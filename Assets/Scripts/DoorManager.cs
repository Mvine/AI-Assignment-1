using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorManager : MonoBehaviour
{

    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material highlightMaterial;
    
    [SerializeField] private float rayDistance;
    [SerializeField] private string selectableTag = "Selectable";

    [SerializeField] private GameObject doorParent;

    private Transform _selection;

    //Types of doors
    private int YYY, YYN, YNY, YNN, NYY, NYN, NNY, NNN;

    // Start is called before the first frame update
    void Start()
    {


        //set up the probabilities here, use mod 4 cause there are 25 doors. Gonna have to find a way to make sure it adds up to 100% using a floor / clamp

        YYY = 3;
        YYN = 3;
        YNY = 3;
        YNN = 3;
        NYY = 3;
        NYN = 3;
        NNY = 3;
        NNN = 4;


        //initialize the doors with the right parameters here
        for(int i = 0 ; i < doorParent.transform.childCount ; i++)
        {
            if(i<YYY)
            {
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setHot(true);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setNoisy(true);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setSafe(true);
            }
            else if(i < YYY + YYN)
            {
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setHot(true);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setNoisy(true);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setSafe(false);
            }
            else if(i < YYY + YYN + YNY)
            {
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setHot(true);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setNoisy(false);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setSafe(true);
            }
            else if(i < YYY + YYN + YNY + YNN)
            {
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setHot(true);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setNoisy(false);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setSafe(false);
            }
            else if(i < YYY + YYN + YNY + YNN + NYY)
            {
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setHot(false);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setNoisy(true);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setSafe(true);
            }
            else if(i < YYY + YYN + YNY + YNN + NYY + NYN)
            {
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setHot(false);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setNoisy(true);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setSafe(false);
            }
            else if(i < YYY + YYN + YNY + YNN + NYY + NYN + NNY)
            {
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setHot(false);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setNoisy(false);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setSafe(true);
            }
            else
            {
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setHot(false);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setNoisy(false);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setSafe(false);
            }

             doorParent.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //deselcting object on hover off
        if(_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;
        }

        //raycast to get the door being pointed at
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit) && hit.distance < rayDistance)
        {
            
            var selection = hit.transform;

            if(selection.CompareTag(selectableTag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();

                if(selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterial;
                }
                _selection = selection;
                
                //if they hit E then select that door
                if(Input.GetKeyDown(KeyCode.E))
                {
                    selection.tag = "Untagged";
                    //should have an event happen here or something?
                    DoorBehaviour door =  selection.parent.GetComponent<DoorBehaviour>();
                    Debug.Log("you selected this door: " + selection.parent.name);

                    TMPro.TextMeshPro safetyText = door.gameObject.GetComponentInChildren<TMPro.TextMeshPro>();
                    safetyText.enabled = true; 
                }
            }          
        }
    }
}
