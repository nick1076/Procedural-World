using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFace : MonoBehaviour
{
    public enum faceDirData {North, South, East, West, Top, Bottom };
    public faceDirData faceDirection;

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (this.GetComponent<Block>() != null)
            {
                this.GetComponent<Block>().DestroyBlock();
            }
            else
            {
                this.transform.parent.GetComponent<Block>().DestroyBlock();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("mod_block"));
            obj.transform.position = new Vector3(this.transform.parent.transform.position.x, this.transform.parent.transform.position.y + 1, this.transform.parent.transform.position.z);
            obj.transform.parent = GameObject.Find("ParentObject").transform;
            obj.GetComponent<Block>().Initialize(Resources.Load<BlockType>("blockWood"));
            //obj.GetComponent<Block>().ReRender(GameObject.Find("World Generator").GetComponent<WorldGenerator>());
        }
    }
}
