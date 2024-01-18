using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{
    //public Button terrainButton;

    public Sprite inventoryImage;
    public Sprite terrainImage;

    private bool isOnTheGround = false;
    private bool isOnTheInventory = false;

    private float damage;
    private float bonusDamageStatus;

    private int inventorySlots = 6;
    /*
     * Flail Inventory disposition
     * 
     *          [0][1]
     *          [2][3]
     *          [4][5]
     * 
     */

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            Destroy(this.gameObject);
    }

    private void Drop()
    {
        isOnTheGround = true;
    }

}
