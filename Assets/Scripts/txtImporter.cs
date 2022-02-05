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
        //Debug.Log(inputField.text);
        // do some data validation too
        string rawData = string.Empty;

        //Read the text from directly from the test.txt file

        using (StreamReader sr = new StreamReader(inputField.text))
        {
            while (sr.Peek() >= 0)
            {
                rawData = sr.ReadLine();
                string resultString = Regex.Match(rawData, "[+-]?([0-9]*[.])?[0-9]+").Value;
                Debug.Log(resultString);

                if (resultString != string.Empty)
                {
                    int toList = (int)((float.Parse(resultString, System.Globalization.CultureInfo.InvariantCulture)) * 100);
                    numDoors.Add(toList);
                }

            }

        }

        SceneManager.LoadScene("PlayScene");
    }
}