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
        EventManager.StartListening("EnemyGold", AddGold);
    }

    void OnDestroy()
    {
        EventManager.StopListening("EnemyGold", AddGold);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGold(int amount)
    {
        this.goldAmount += amount;
    }

    public void BoughItem()
    {
        Debug.Log("Buying item");
        spendGold(50);
    }

    public int getGoldAmount()
    {
        return goldAmount;
    }

    public void spendGold(int spend)
    {
        goldAmount -= spend;
    }
}
