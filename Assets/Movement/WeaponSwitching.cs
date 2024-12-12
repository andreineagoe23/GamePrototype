using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    public string IDLE = "Sword Idle";
    public string ATTACK1 = "Attack 1";
    public string ATTACK2 = "Attack 2";
    public float weaponSpeed = 1f;
    public int weaponDamage = 2;

    public GameObject arms; // Reference to the Arms GameObject


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
            selectedWeapon = 1;
            ATTACK1 = "Attack 1";
            ATTACK2 = "Attack 2";
            IDLE = "Sword Idle";
            ToggleArmsMeshRenderer(true);
            weaponSpeed = 1f;
            weaponDamage = 2;

    Debug.Log($"Weapon switched to Sword. Animations: IDLE={IDLE}, ATTACK1={ATTACK1}, ATTACK2={ATTACK2}");




        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // Key 2 for weapon 2
        {
            selectedWeapon = 0;
            ATTACK1 = "Spear Attack";
            ATTACK2 = "Spear Attack";
            IDLE = "Spear Idle";
            ToggleArmsMeshRenderer(false);
            weaponSpeed = 3f;
            weaponDamage = 2;
            Debug.Log($"Weapon switched to Spear. Animations: IDLE={IDLE}, ATTACK1={ATTACK1}, ATTACK2={ATTACK2}");



        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) // Key 3 for weapon 3 (if applicable)
        {
            selectedWeapon = 2;
            ATTACK1 = "Attack 1";
            ATTACK2 = "Attack 2";
            IDLE = "Sword Idle";
            weaponSpeed = 0.3f;
            weaponDamage = 1;
            ToggleArmsMeshRenderer(true);
            Debug.Log($"Weapon switched to Dagger. Animations: IDLE={IDLE}, ATTACK1={ATTACK1}, ATTACK2={ATTACK2}");



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

    private void ToggleArmsMeshRenderer(bool visible)
    {
        if (arms != null)
        {
            SkinnedMeshRenderer armsRenderer = arms.GetComponent<SkinnedMeshRenderer>();
            if (armsRenderer != null)
            {
                armsRenderer.enabled = visible; // Toggle the MeshRenderer
            }
            else
            {
                Debug.LogError("MeshRenderer component not found on Arms GameObject.");
            }
        }
        else
        {
            Debug.LogError("Arms GameObject is not assigned in WeaponSwitching script.");
        }
    }

}
