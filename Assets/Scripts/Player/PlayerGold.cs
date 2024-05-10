using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour, IShopCustomer
{
    // Start is called before the first frame update
    public int initialGold;
    public int goldAmount;
    public bool godMode = false;

    void Start()
    {
        goldAmount = initialGold;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool BuyItem(int amount)
    {
        if (amount <= goldAmount)
        {
            Debug.Log("Buying item");
            SpendGold(amount);
            return true;
        }
        return false;
    }

    public int GetGoldAmount()
    {
        return goldAmount;
    }

    public void SpendGold(int spend)
    {
        goldAmount -= spend;
    }
}
