using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mimicObject : MonoBehaviour
{
    public Transform transToMimic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(transToMimic.position.x, this.transform.position.y, transToMimic.position.z);
        this.transform.rotation = transToMimic.rotation;
    }
}
