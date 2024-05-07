using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour, IShopCustomer
{
    // Start is called before the first frame update
    public int initialGold;

    void Start()
    {
              
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BoughItem()
    {
        Debug.Log("Buying item");
    }
}
