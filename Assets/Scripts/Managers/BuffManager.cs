using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Nightmare {

public class BuffManager : MonoBehaviour
{
    static GridLayoutGroup gridLayoutGroup;

    void Start()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
    }
    
    public static GameObject AddBuff(GameObject buffPrefab)
    {
        GameObject buffHUD = Instantiate(buffPrefab, gridLayoutGroup.transform);
        return buffHUD;
    }

    public static void RemoveBuff(GameObject buff)
    {
        Destroy(buff);
    }
}
}
