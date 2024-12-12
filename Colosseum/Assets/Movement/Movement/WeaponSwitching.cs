using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int previousSelected = selectedWeapon;

        // Switch weapon based on input
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Key 1 for weapon 1
        {
            selectedWeapon = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // Key 2 for weapon 2
        {
            selectedWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) // Key 3 for weapon 3 (if applicable)
        {
            selectedWeapon = 2;
        }

        // Select the new weapon if changed
        if (previousSelected != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    private void SelectWeapon()
    {
        int weaponIndex = 0;

        // Loop through all child weapons
        foreach (Transform weapon in transform)
        {
            // Activate the selected weapon and deactivate others
            weapon.gameObject.SetActive(weaponIndex == selectedWeapon);
            weaponIndex++;
        }
    }
}
