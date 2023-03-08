using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="New Block",fileName="newBlock")]
public class BlockType : ScriptableObject
{
    public string blockName;
    public GameObject blockModel;
    public Sprite blockIcon;
    public bool transparent;

    public List<Material> blockFaceMaterials = new List<Material>();
}
