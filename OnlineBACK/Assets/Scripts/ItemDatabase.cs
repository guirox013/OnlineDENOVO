using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {
	public List<Item> items = new List<Item>();

	void Start(){
		items.Add(new Item("Axe", 1, "A cool fucking axe", 1, 90, Item.ItemType.Weapon));
		items.Add (new Item ("Apple", 2, "A delicious Apple",1,0, Item.ItemType.Consumable));
		items.Add (new Item ("Plank", 3, "A solid plank",1,9,Item.ItemType.Materia));
	}
}
