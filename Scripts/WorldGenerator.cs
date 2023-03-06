using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{

    public GameObject blockModel;
    public BiomeData biome;

    public int xScale;
    public int yScale;
    public int zScale;
    public int yDepth;

    public Dictionary<Vector3, Block> blocks = new Dictionary<Vector3, Block>();

    GameObject prevParent;

    void Start()
    {
        Generate(biome, this.transform, xScale, yScale, zScale);
    }

    public void ReGenerate()
    {
        Generate(biome, this.transform, xScale, yScale, zScale);
    }

    public void Generate(BiomeData b, Transform origin, int xS, int yS, int zS)
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
                int topBlockPosition = (int)(Mathf.Round(Mathf.PerlinNoise((float)(x + seed) * .156853f, (float)(z + seed) * .156853f) * yScale * b.biomeHeightScale) - 1);
                
                if (topBlockPosition <= yDepth)
                {
                    topBlockPosition = yDepth + 1;
                }

                GameObject obj = Instantiate(blockModel);
                obj.transform.position = new Vector3(x, topBlockPosition, z);
                obj.GetComponent<Block>().Initialize(b.surfaceBlock);
                obj.transform.parent = parent.transform;

                int topper = UnityEngine.Random.Range(0, b.structureChance + 1);

                if (topper == b.structureChance)
                {
                    Instantiate(b.structures[UnityEngine.Random.Range(0, b.structures.Count)], new Vector3(x, topBlockPosition + 1, z), Quaternion.identity, parent.transform);
                }

                for (int depth = topBlockPosition - 1; depth > yDepth; depth--)
                {
                    GameObject objD = Instantiate(blockModel);
                    objD.transform.position = new Vector3(x, depth, z);

                    if (depth + 2 < topBlockPosition)
                    {
                        int ore = UnityEngine.Random.Range(0, b.oreChance + 1);

                        if (ore == b.oreChance)
                        {
                            objD.GetComponent<Block>().Initialize(b.ore);
                        }
                        else
                        {
                            objD.GetComponent<Block>().Initialize(b.undergroundBlock);
                        }
                    }
                    else
                    {
                        objD.GetComponent<Block>().Initialize(b.subSufaceBlock);
                    }

                    objD.transform.parent = parent.transform;
                }
            }
        }

        foreach (Block blockObj in blocks.Values)
        {
            blockObj.ReRender(this);
        }
    }
}
