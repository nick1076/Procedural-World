using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    [SerializeField] private BlockType block;

    public Renderer faceSouth;
    public Renderer faceNorth;
    public Renderer faceEast;
    public Renderer faceWest;
    public Renderer faceTop;
    public Renderer faceBottom;
    bool init;

    public void Initialize(BlockType type)
    {
        if (init)
        {
            return;
        }

        init = true;

        WorldGenerator gen = GameObject.Find("World Generator").GetComponent<WorldGenerator>();

        if (gen.blocks.ContainsKey(this.transform.position))
        {
            if (gen.blocks[this.transform.position] == this.gameObject)
            {
                //Same block
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            gen.blocks.Add(this.transform.position, this);
        }

        block = type;

        if (faceSouth == null)
        {
            return;
        }

        if (block.blockFaceMaterials.Count == 0)
        {
            Material missing = Resources.Load<Material>("m_missing");
            faceSouth.material = missing;
            faceNorth.material = missing;
            faceEast.material = missing;
            faceWest.material = missing;
            faceTop.material = missing;
            faceBottom.material = missing;
        }
        else if (block.blockFaceMaterials.Count == 1)
        {
            faceSouth.material = block.blockFaceMaterials[0];
            faceNorth.material = block.blockFaceMaterials[0];
            faceEast.material = block.blockFaceMaterials[0];
            faceWest.material = block.blockFaceMaterials[0];
            faceTop.material = block.blockFaceMaterials[0];
            faceBottom.material = block.blockFaceMaterials[0];
        }
        else if (block.blockFaceMaterials.Count == 2)
        {
            faceSouth.material = block.blockFaceMaterials[0];
            faceNorth.material = block.blockFaceMaterials[1];
            faceEast.material = block.blockFaceMaterials[1];
            faceWest.material = block.blockFaceMaterials[1];
            faceTop.material = block.blockFaceMaterials[1];
            faceBottom.material = block.blockFaceMaterials[1];
        }
        else if (block.blockFaceMaterials.Count == 6)
        {
            faceSouth.material = block.blockFaceMaterials[0];
            faceNorth.material = block.blockFaceMaterials[1];
            faceEast.material = block.blockFaceMaterials[2];
            faceWest.material = block.blockFaceMaterials[3];
            faceTop.material = block.blockFaceMaterials[4];
            faceBottom.material = block.blockFaceMaterials[5];
        }
        else
        {
            Material missing = Resources.Load<Material>("m_missing");
            faceSouth.material = missing;
            faceNorth.material = missing;
            faceEast.material = missing;
            faceWest.material = missing;
            faceTop.material = missing;
            faceBottom.material = missing;
        }
    }

    private void Start()
    {
        if (block != null)
        {
            Initialize(block);
        }
    }

    public void DestroyBlock()
    {
        WorldGenerator gen = GameObject.Find("World Generator").GetComponent<WorldGenerator>();

        gen.blocks.Remove(this.transform.position);

        if (gen.blocks.ContainsKey(new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z)))
        {
            gen.blocks[new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z)].ReRender(gen);
        }

        if (gen.blocks.ContainsKey(new Vector3(this.transform.position.x, this.transform.position.y - 1, this.transform.position.z)))
        {
            gen.blocks[new Vector3(this.transform.position.x, this.transform.position.y - 1, this.transform.position.z)].ReRender(gen);
        }

        if (gen.blocks.ContainsKey(new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z)))
        {
            gen.blocks[new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z)].ReRender(gen);
        }

        if (gen.blocks.ContainsKey(new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z)))
        {
            gen.blocks[new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z)].ReRender(gen);
        }

        if (gen.blocks.ContainsKey(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1)))
        {
            gen.blocks[new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1)].ReRender(gen);
        }

        if (gen.blocks.ContainsKey(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1)))
        {
            gen.blocks[new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1)].ReRender(gen);
        }

        Instantiate(Resources.Load<GameObject>("fx_breakEffect"), this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if (GameObject.Find("World Generator").GetComponent<WorldGenerator>().blocks.ContainsKey(this.transform.position))
        {
            GameObject.Find("World Generator").GetComponent<WorldGenerator>().blocks.Remove(this.transform.position);
        }
    }

    public void ReRender(WorldGenerator gen)
    {
        print("Re-Render Called at: " + this.transform.position);

        if (faceSouth == null)
        {
            return;
        }

        if (gen.blocks.ContainsKey(new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z)))
        {
            if (!gen.blocks[new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z)].block.transparent)
            {
                faceTop.gameObject.SetActive(false);
            }
            else
            {
                faceTop.gameObject.SetActive(true);
            }
        }
        else
        {
            faceTop.gameObject.SetActive(true);
        }

        if (gen.blocks.ContainsKey(new Vector3(this.transform.position.x, this.transform.position.y - 1, this.transform.position.z)))
        {
            if (!gen.blocks[new Vector3(this.transform.position.x, this.transform.position.y -1, this.transform.position.z)].block.transparent)
            {
                faceBottom.gameObject.SetActive(false);
            }
            else
            {
                faceBottom.gameObject.SetActive(true);
            }
        }
        else
        {
            faceBottom.gameObject.SetActive(true);
        }

        if (gen.blocks.ContainsKey(new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z)))
        {
            if (!gen.blocks[new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z)].block.transparent)
            {
                faceWest.gameObject.SetActive(false);
            }
            else
            {
                faceWest.gameObject.SetActive(true);
            }
        }
        else
        {
            faceWest.gameObject.SetActive(true);
        }

        if (gen.blocks.ContainsKey(new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z)))
        {
            if (!gen.blocks[new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z)].block.transparent)
            {
                faceEast.gameObject.SetActive(false);
            }
            else
            {
                faceEast.gameObject.SetActive(true);
            }
        }
        else
        {
            faceEast.gameObject.SetActive(true);
        }

        if (gen.blocks.ContainsKey(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1)))
        {
            if (!gen.blocks[new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1)].block.transparent)
            {
                faceSouth.gameObject.SetActive(false);
            }
            else
            {
                faceSouth.gameObject.SetActive(true);
            }
        }
        else
        {
            faceSouth.gameObject.SetActive(true);
        }

        if (gen.blocks.ContainsKey(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1)))
        {
            if (!gen.blocks[new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1)].block.transparent)
            {
                faceNorth.gameObject.SetActive(false);
            }
            else
            {
                faceNorth.gameObject.SetActive(true);
            }
        }
        else
        {
            faceNorth.gameObject.SetActive(true);
        }
    }
}
