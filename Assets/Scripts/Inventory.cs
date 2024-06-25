using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Inventory : MonoBehaviour
{
    public int _keyCount;
    private int _coinCount;


    public void PickUp(GameObject drop)
    {
        if (drop.name == "key")
        {
            _keyCount++;
        }

        else if (drop.name == "coin")
        {
            _coinCount++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "key")
        {
            collision.gameObject.SetActive(false);
            PickUp(collision.gameObject);
        }

        if (collision.gameObject.name == "coin")
        {
            collision.gameObject.SetActive(false);
            PickUp(collision.gameObject);
        }
    }
}
