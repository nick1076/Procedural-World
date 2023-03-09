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
            WorldGenerator gen = GameObject.Find("World Generator").GetComponent<WorldGenerator>();
            Vector3 blockLocat = this.transform.parent.transform.position;

            if (!gen.blocks.ContainsKey(new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z)) && faceDirection == faceDirData.Top)
            {
                //Place block above
                blockLocat.y += 1;
            }

            if (!gen.blocks.ContainsKey(new Vector3(this.transform.position.x, this.transform.position.y - 1, this.transform.position.z)) && faceDirection == faceDirData.Bottom)
            {
                //Place block below
                blockLocat.y -= 1;
            }

            if (!gen.blocks.ContainsKey(new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z)) && faceDirection == faceDirData.West)
            {
                //Place block west
                blockLocat.x -= 1;
            }

            if (!gen.blocks.ContainsKey(new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z)) && faceDirection == faceDirData.East)
            {
                //Place block east
                blockLocat.x += 1;
            }

            if (!gen.blocks.ContainsKey(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1)) && faceDirection == faceDirData.South)
            {
                //Place block south
                blockLocat.z -= 1;
            }

            if (!gen.blocks.ContainsKey(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1)) && faceDirection == faceDirData.North)
            {
                //Place block north
                blockLocat.z += 1;
            }

            BlockType toBuild = GameObject.Find("Managers").GetComponent<InventoryManager>().blocksAvaliable[GameObject.Find("Managers").GetComponent<InventoryManager>().index];

            if (toBuild.blockModel != null)
            {
                GameObject obj = Instantiate(toBuild.blockModel);
                obj.transform.position = blockLocat;
                obj.transform.parent = GameObject.Find("ParentObject").transform;

                obj.GetComponent<Block>().Initialize(toBuild);
            }
            else
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("mod_block"));
                obj.transform.position = blockLocat;
                obj.transform.parent = GameObject.Find("ParentObject").transform;

                obj.GetComponent<Block>().Initialize(toBuild);
            }

        }
    }
}
