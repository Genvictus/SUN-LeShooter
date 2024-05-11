using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] private ShopUI shopUI;

    bool isOpened = false;

    bool isPlayerOnShop = false;

    [SerializeField] private GameObject NotInShopUI;


    private void OnTriggerEnter(Collider other)
    {
        IShopCustomer shopCustomer = other.GetComponent<IShopCustomer>();
        if (shopCustomer != null)
        {
            isPlayerOnShop = true;
            if (Input.GetKeyDown(KeyCode.E))
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
            isPlayerOnShop = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isPlayerOnShop)
        {
            Debug.Log("Player is not on shop");
            NotInShopUI.gameObject.SetActive(true);
            StartCoroutine(HideNotInShop());
        }
    }

    private IEnumerator HideNotInShop()
    {
        yield return new WaitForSeconds(1);

        // Nonaktifkan havePet game object
        NotInShopUI.gameObject.SetActive(false);
    }
}
