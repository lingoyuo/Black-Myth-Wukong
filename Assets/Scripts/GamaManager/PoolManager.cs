using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour {

    public GameObject objPool;
    public int size;

    List<GameObject> listObjPools;
    
    void Start()
    {

        listObjPools = new List<GameObject>();

        for(int i=0;i< size;i++)
        {
            var obj = (GameObject)Instantiate(objPool, Vector3.zero, Quaternion.identity);

            obj.SetActive(false);
            listObjPools.Add( obj);
        }
    }

    public GameObject RequestObjPool(Vector3 posSpawn)
    {
        size++;
        var obj = (GameObject)Instantiate(objPool, Vector3.zero, Quaternion.identity);

        listObjPools.Add(obj);
        obj.transform.position = posSpawn;
        return obj;
    }

    public GameObject GetObjPool(Vector3 posSpawn)
    {
        foreach(var obj in listObjPools)
        {
            if (!obj.activeSelf)
            {

                obj.SetActive(true);

                obj.transform.position = posSpawn;         

                return obj;
            }
        }
        return null;
    } 
}
