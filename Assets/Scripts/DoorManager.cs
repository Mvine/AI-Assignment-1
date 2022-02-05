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
    private GameObject importedData;
    private List<int> doorList;

    // Start is called before the first frame update
    void Start()
    {


        //set up the probabilities here, use mod 4 cause there are 25 doors. Gonna have to find a way to make sure it adds up to 100% using a floor / clamp

        importedData = GameObject.Find("FileImporter");
        if(importedData != null)
        {
            doorList = importedData.GetComponent<txtImporter>().numDoors;
        }


        //initialize the doors with the right parameters here
        for(int i = 0 ; i < doorParent.transform.childCount ; i++)
        {
            if(i<doorList[0])
            {
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setHot(true);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setNoisy(true);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setSafe(true);
            }
            else if(i < doorList[0] + doorList[1])
            {
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setHot(true);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setNoisy(true);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setSafe(false);
            }
            else if(i < doorList[0] + doorList[1] + doorList[2])
            {
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setHot(true);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setNoisy(false);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setSafe(true);
            }
            else if(i < doorList[0] + doorList[1] + doorList[2] + doorList[3])
            {
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setHot(true);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setNoisy(false);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setSafe(false);
            }
            else if(i < doorList[0] + doorList[1] + doorList[2] + doorList[3] + doorList[4])
            {
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setHot(false);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setNoisy(true);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setSafe(true);
            }
            else if(i < doorList[0] + doorList[1] + doorList[2] + doorList[3] + doorList[4] + doorList[5])
            {
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setHot(false);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setNoisy(true);
                doorParent.transform.GetChild(i).gameObject.GetComponent<DoorBehaviour>().setSafe(false);
            }
            else if(i < doorList[0] + doorList[1] + doorList[2] + doorList[3] + doorList[4] + doorList[5] + doorList[6])
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
