using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{

    public int xScale;
    public int yScale;
    public int zScale;
    public int yDepth;

    public int propChance = 25;

    public Material grassMaterial;
    public Material dirtMaterial;
    public Material stoneMaterial;

    public Dictionary<Vector3, Block> blocks = new Dictionary<Vector3, Block>();

    GameObject prevParent;

    public GameObject[] props;

    void Start()
    {
        Generate();
    }

    public void Generate()
    {
        blocks.Clear();
        int seed = UnityEngine.Random.Range(0, 100000000);
        UnityEngine.Random.InitState(seed);

        GameObject parent = new GameObject();
        Destroy(prevParent);
        parent.name = "ParentObject";
        prevParent = parent;

        for (int x = -(xScale / 2); x < (xScale / 2) + 1; x++)
        {
            for (int z = -(zScale / 2); z < (zScale / 2) + 1; z++)
            {
                int topBlockPosition = (int)(Mathf.Round(Mathf.PerlinNoise((float)(x + seed) * .156853f, (float)(z + seed) * .156853f) * yScale) - 1);
                
                if (topBlockPosition <= yDepth)
                {
                    topBlockPosition = yDepth + 1;
                }

                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = new Vector3(x, topBlockPosition, z);
                obj.GetComponent<Renderer>().material = grassMaterial;
                obj.AddComponent<Block>();
                obj.transform.parent = parent.transform;

                int topper = UnityEngine.Random.Range(0, propChance + 1);

                if (topper == propChance)
                {
                    Instantiate(props[UnityEngine.Random.Range(0, props.Length)], new Vector3(x, topBlockPosition + 1, z), Quaternion.identity, parent.transform);
                }

                for (int depth = topBlockPosition - 1; depth > yDepth; depth--)
                {
                    GameObject objD = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    objD.transform.position = new Vector3(x, depth, z);

                    if (depth + 2 < topBlockPosition)
                    {
                        objD.GetComponent<Renderer>().material = stoneMaterial;
                    }
                    else
                    {
                        objD.GetComponent<Renderer>().material = dirtMaterial;
                    }

                    objD.AddComponent<Block>();
                    objD.transform.parent = parent.transform;
                }
            }
        }
    }
}
