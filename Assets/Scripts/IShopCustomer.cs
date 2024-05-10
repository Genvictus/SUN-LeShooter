using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer
{
    bool BuyItem(int amount);

    int GetGoldAmount();

    void SpendGold(int spend);
}
