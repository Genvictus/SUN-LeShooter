using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer
{
    void BoughItem();

    int getGoldAmount();

    void spendGold(int spend);
}