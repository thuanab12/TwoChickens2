using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : Singleton<ActiveInventory>
{
    private int activeSlotIndexNum = 0;
    private int playerCoins = 0; // Track player's coin count
    private bool isAK47Collected = false; // Flag to check if AK47 is collected
    private PlayerControls playerControls;

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    public void EquipStartingWeapon()
    {
        ToggleActiveHighlight(0);
    }

    private void ToggleActiveSlot(int numValue)
    {
        int slotIndex = numValue - 1;

        // Check if the player has enough coins to access the slot
        if ((slotIndex == 1 && playerCoins < 10) ||
            (slotIndex == 2 && playerCoins < 50))
        {
            Debug.Log("Not enough coins to unlock this slot!");
            return;
        }

        // Check if slot 3 should be unlocked
        if (slotIndex == 3 && !isAK47Collected)
        {
            Debug.Log("Slot 3 is locked until you collect the AK47!");
            return;
        }
        
        ToggleActiveHighlight(slotIndex);
    }

    private void ToggleActiveHighlight(int indexNum)
    {
        activeSlotIndexNum = indexNum;

        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }
        if(indexNum == 4)
        {
            return;
        }
        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {
        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        Transform childTransform = transform.GetChild(activeSlotIndexNum);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();
        GameObject weaponToSpawn = weaponInfo.weaponPrefab;

        if (weaponInfo == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform);

        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }

    // Method to add coins
    public void AddCoins(int amount)
    {
        playerCoins += amount;
        Debug.Log("Coins: " + playerCoins);
    }

    // Method to set AK47 collected
    public void SetAK47Collected(bool collected)
    {
        isAK47Collected = collected;
    }
}