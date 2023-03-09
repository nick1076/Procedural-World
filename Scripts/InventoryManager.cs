using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public BlockType[] blocksAvaliable;
    public UnityEngine.UI.Image icon;
    public int index = 0;

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            index++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            index--;
        }

        if (index < 0)
        {
            index = blocksAvaliable.Length - 1;
        }
        else if (index > blocksAvaliable.Length - 1)
        {
            index = 0;
        }

        icon.sprite = blocksAvaliable[index].blockIcon;
    }

}
