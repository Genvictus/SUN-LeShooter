using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] private ShopUI shopUI;

    bool isOpened = false;

    private void OnTriggerEnter(Collider other)
    {
        IShopCustomer shopCustomer = other.GetComponent<IShopCustomer>();
        if (shopCustomer != null && Input.GetKeyDown(KeyCode.E))
        {
            if (isOpened)
            {
                shopUI.Hide();
            }
            else
            {
                shopUI.Show(shopCustomer);
            }
            isOpened = !isOpened;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        IShopCustomer shopCustomer = other.GetComponent<IShopCustomer>();
        if (shopCustomer != null && Input.GetKeyDown(KeyCode.E))
        {
            if (isOpened)
            {
                shopUI.Hide();
            }
            else
            {
                shopUI.Show(shopCustomer);
            }
            isOpened = !isOpened;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IShopCustomer shopCustomer = other.GetComponent<IShopCustomer>();
        if (shopCustomer != null)
        {
            shopUI.Hide();
        }
    }
}
