using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ItemInventory : MonoBehaviour{

    //public IEnumerable<Item> items = Enumerable.Empty<Item>();
    public List<Item> items = new List<Item>();

    public void initialise()
    {
        items.Add(new LifeNecklace());
        items.Add(new StaffOfTrials());
    }

}
