using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] private ShopUI shopUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        IShopCustomer shopCustomer = other.GetComponent<IShopCustomer>();
        if (shopCustomer != null && Input.GetKey(KeyCode.E))
        {
            shopUI.Show(shopCustomer);
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
