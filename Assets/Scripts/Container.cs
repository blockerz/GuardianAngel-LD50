using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField]
    int damageAmount = 5;
    
    [SerializeField]
    float damageTickFreq = 0.5f;

    GameObject player;
    bool playerOutsideContainer = false;
    float lastDamageTime = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");  
    }

    void Update()
    {
        lastDamageTime += Time.deltaTime;

        if (playerOutsideContainer && lastDamageTime > damageTickFreq)
        {
            player.GetComponent<PlayerHealth>().DecreaseHealth(damageAmount);
            lastDamageTime = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Triggered");
        if (collision.gameObject.tag == "Player")
        {
            playerOutsideContainer = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerOutsideContainer = true;
        }
    }


    private void OnMouseDown()
    {
        
    }
}
