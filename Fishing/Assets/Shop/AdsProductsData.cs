using System.Collections.Generic;
using UnityEngine;

// This script defines a ScriptableObject to store information about ads products.
[CreateAssetMenu(fileName = "AdsProductsData", menuName = "ScriptableObject/AdsProductsData")]
public class AdsProductsData : ScriptableObject
{
    public List<Sprite> adsProductsDatas = new List<Sprite>(); // list of ads products.
}
