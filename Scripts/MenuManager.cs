using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public TextMeshProUGUI textCameraSize;
    public TextMeshProUGUI textCameraRotation;
    public Slider sliderCameraSize;
    public Slider sliderCameraRotation;
    public Camera objectMainCamera;

    public TMP_InputField inputFieldXScale;
    public TMP_InputField inputFieldYScale;
    public TMP_InputField inputFieldZScale;
    public WorldGenerator objectWorldGenerator;

    public GameObject[] generationMenuObjects;
    public GameObject[] firstPersonObjects;
    public GameObject playerObject;
    bool inFirstPerson = false;

    private void Start()
    {
        ToggleMode(0);
    }

    public void ToggleMode(int id)
    {
        if (id == 0)
        {
            inFirstPerson = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            foreach (GameObject obj in firstPersonObjects)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in generationMenuObjects)
            {
                obj.SetActive(true);
            }
        }
        else if (id == 1)
        {
            inFirstPerson = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            playerObject.transform.position = new Vector3(0, 10, 0);

            foreach (GameObject obj in generationMenuObjects)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in firstPersonObjects)
            {
                obj.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (inFirstPerson)
            {
                ToggleMode(0);
            }
            else
            {
                ToggleMode(1);
            }
        }
    }

    public void SetUIElement(int id)
    {
        /*
        ID List:
        
        0 = Camera Size Text
        1 = X Scale Input
        2 = Y Scale Input
        3 = Z Scale Input
        4 = Camera Rotation

        */

        if (id == 0)
        {
            textCameraSize.text = Mathf.Round(sliderCameraSize.value).ToString();
            objectMainCamera.orthographicSize = sliderCameraSize.value;
        }
        else if (id == 1)
        {
            int reading = int.Parse(inputFieldXScale.text);

            if (reading == 0)
            {
                reading = 1;
            }

            objectWorldGenerator.xScale = reading;
        }
        else if (id == 2)
        {
            int reading = int.Parse(inputFieldYScale.text);

            if (reading == 0)
            {
                reading = 1;
            }

            objectWorldGenerator.yScale = reading;
        }
        else if (id == 3)
        {
            int reading = int.Parse(inputFieldZScale.text);

            if (reading == 0)
            {
                reading = 1;
            }

            objectWorldGenerator.zScale = reading;
        }
        else if (id == 4)
        {
            int reading = (int)(sliderCameraRotation.value);
            textCameraRotation.text = reading.ToString() + " Degrees";

            objectMainCamera.transform.parent.transform.eulerAngles = new Vector3(objectMainCamera.transform.parent.transform.rotation.eulerAngles.x, reading, objectMainCamera.transform.parent.transform.rotation.eulerAngles.z);
        }
    }

}
