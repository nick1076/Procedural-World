using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBiome", menuName = "New Biome")]
public class BiomeData : ScriptableObject
{

    public BlockType surfaceBlock;
    public BlockType subSufaceBlock;
    public BlockType undergroundBlock;
    public BlockType ore;

    public int oreChance = 25;
    public int structureChance = 10;
    public int biomeHeightScale = 1;

    public List<GameObject> structures = new List<GameObject>();

}
