using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

public class txtImporter : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField inputField;

    // Start is called before the first frame update

    public List<int> numDoors = new List<int>();

    void Awake()
    {
        //need to pass the loaded data between scenes
        DontDestroyOnLoad(transform.gameObject);
    }
    void Start()
    {
        //Debug.Log("Need this to import the txt and parse it");
    }

    // Update is called once per frame

    public void Import()
    {
        string rawData = string.Empty;

        //Read the text from directly from the test.txt file

        using (StreamReader sr = new StreamReader(inputField.text))
        {
            //check if there is a next line
            while (sr.Peek() >= 0)
            {
                rawData = sr.ReadLine();
                string resultString = Regex.Match(rawData, "[+-]?([0-9]*[.])?[0-9]+").Value;
                Debug.Log(resultString);
                //if there is a number then add it to the list. I am assuming that the properties order remains consistent between txt files
                if (resultString != string.Empty)
                {
                    //I have 25 doors so i multiply the float by 25 to get my door value
                    int toList = (int)((float.Parse(resultString, System.Globalization.CultureInfo.InvariantCulture)) * 25);
                    numDoors.Add(toList);
                }

            }

        }
        //load the play scene
        SceneManager.LoadScene("PlayScene");
    }
}