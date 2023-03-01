using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="New Block",fileName="newBlock")]
public class BlockType : ScriptableObject
{
    public string blockName;
    public Material blockMaterial;
    public GameObject blockModel;
}
