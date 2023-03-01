using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private void Start()
    {
        if (GameObject.Find("World Generator").GetComponent<WorldGenerator>().blocks.ContainsKey(this.transform.position))
        {
            Destroy(this.gameObject);
        }
        else
        {
            GameObject.Find("World Generator").GetComponent<WorldGenerator>().blocks.Add(this.transform.position, this);
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Destroy(this.gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            obj.GetComponent<Renderer>().material = Resources.Load<Material>("wood");
            obj.AddComponent<Block>();
            obj.transform.parent = GameObject.Find("ParentObject").transform;
        }
    }

    private void OnDestroy()
    {
        GameObject.Find("World Generator").GetComponent<WorldGenerator>().blocks.Remove(this.transform.position);
        Instantiate(Resources.Load<GameObject>("fx_breakEffect"), this.transform.position, Quaternion.identity);
    }
}
