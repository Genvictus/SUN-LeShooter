using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour, IShopCustomer
{
    // Start is called before the first frame update
    public int initialGold;
    private int goldAmount;
    GameObject player;
    GameObject petHolder;
    [Header("References")]
    [SerializeField] private Transform[] pets = new Transform[2];

    void Start()
    {
        petHolder = GameObject.FindGameObjectWithTag("PetHolder");
        player = GameObject.FindGameObjectWithTag("Player");
        goldAmount = initialGold;

        for (int i = 0; i < petHolder.transform.childCount; i++)
            pets[i] = petHolder.transform.GetChild(i);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BoughItem(int index)
    {
        Debug.Log("Buying item");

        Debug.Log("membeli pet: " + pets[index].gameObject.name);
        pets[index].gameObject.SetActive(true);
        GameObject[] avaiablePet = GameObject.FindGameObjectsWithTag("Pet");
        foreach(var temp in avaiablePet)
        {
            if(temp.name == pets[index].gameObject.name)
            {
                PetHealth health = temp.GetComponent<PetHealth>();
                health.transform.position = new Vector3(0,0,0);
                health.currentHealth = health.maxHealth;
                health.isDead = false;
                health.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            }
        }

        /*GameObject[] pets = GameObject.FindGameObjectsWithTag("Pet");
        Debug.Log("jumlah pet: " + pets.Length);
        foreach(var pet in pets)
        {
            Debug.Log("nama pet: " + pet.name);
            if(pet.name == petName)
            {
                Debug.Log("set active: " + petName);
                pet.SetActive(true);
            }
        }*/
        /*var pet = Resources.Load(petName) as GameObject;
        Debug.Log("nama pet: " + pet.name);
        Instantiate(pet, player.transform.position, player.transform.rotation);*/

        spendGold(200);
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
