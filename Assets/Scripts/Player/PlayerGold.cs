using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour, IShopCustomer
{
    // Start is called before the first frame update
    public int initialGold;
    public int goldAmount;
    GameObject player;
    GameObject petHolder;
    [Header("References")]
    [SerializeField] private Transform[] pets = new Transform[2];
    public bool godMode = false;

    void Start()
    {
        petHolder = GameObject.FindGameObjectWithTag("PetHolder");
        player = GameObject.FindGameObjectWithTag("Player");
        goldAmount = initialGold;
        for (int i = 0; i < petHolder.transform.childCount; i++)
            pets[i] = petHolder.transform.GetChild(i);
        EventManager.StartListening("PlayerEarnGold", AddGold);
    }

    void OnDestroy()
    {
        EventManager.StopListening("PlayerEarnGold", AddGold);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGold(int amount)
    {
        this.goldAmount += amount;
    }

    public bool BuyItem(int index, int price)
    {
        Debug.Log("Buying item");
        Debug.Log("membeli pet: " + pets[index].gameObject.name);

        if (!SpendGold(price)) return false;

        pets[index].gameObject.SetActive(true);
        GameObject[] avaiablePet = GameObject.FindGameObjectsWithTag("Pet");
        foreach (var temp in avaiablePet)
        {
            if (temp.name == pets[index].gameObject.name)
            {
                PetHealth health = temp.GetComponent<PetHealth>();
                health.transform.position = new Vector3(0, 0, 0);
                health.currentHealth = health.maxHealth;
                health.isDead = false;
                health.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                return true;
            }
        }
        pets[index].gameObject.SetActive(false);
        return false;

    }

    public int GetGoldAmount()
    {
        return goldAmount;
    }

    public bool SpendGold(int spend)
    {
        if (spend <= goldAmount)
        {
            goldAmount -= spend;
            return true;
        }
        return false;
    }
}
