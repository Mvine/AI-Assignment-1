using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{

    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material highlightMaterial;
    
    [SerializeField] private float rayDistance;
    [SerializeField] private string selectableTag = "Selectable";

    private Transform _selection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;
        }


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
            }
            
        }
    }
}
