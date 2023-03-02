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
