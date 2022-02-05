using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class txtImporter : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField inputField;

    [SerializeField] private string stringData;

    // Start is called before the first frame update

    public float hotOdds = 0.0f;
    public float noisyOdds = 0.0f;
    public float safeOdds = 0.0f;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    void Start()
    {
        Debug.Log("Need this to import the txt and parse it");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Import()
    {
        //something something something import "inputField.text"

        //something something parse the file here
        Debug.Log(inputField.text);
        stringData = inputField.text;
        // do some data validation too

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(inputField.text); 
        Debug.Log(reader.ReadToEnd());
        reader.Close();
        

        SceneManager.LoadScene("PlayScene");
    }
}