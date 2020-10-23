using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupReferences : MonoBehaviour
{
    public Pickups[] pickupRefence;

    public Pickups GetPickupDetails(string pickup)
    {
        for (int i = 0; i < pickupRefence.Length; i++)
        {
            if (pickupRefence[i].pickupName == pickup)
            {
                return pickupRefence[i];
            }
        }
        return null;
    }
}
