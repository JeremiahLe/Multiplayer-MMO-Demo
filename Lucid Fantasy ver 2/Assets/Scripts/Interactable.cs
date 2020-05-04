using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    InputField inputfield;

    // Start is called before the first frame update
    void Start()
    {
        inputfield = GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && inputfield.interactable != true)
        {
            inputfield.interactable = true;
        }
        else if (Input.GetKeyDown(KeyCode.Return) && inputfield.interactable == true)
        {
            inputfield.interactable = false;
        }

    }
}
