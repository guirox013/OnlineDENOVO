using UnityEngine;
using System.Collections;
[System.Serializable]
public class Item {
	public string itemName;
	public int itemId;
	public string itemDesc;
	public Texture2D itemIcon;
	public int itemAmount;
	public int itemPower;
	public ItemType itemType;
	
	public enum ItemType {
		Weapon,
		Consumable,
		Materia,
	}

	public Item(string name, int id, string desc, int amount, int power, ItemType type){
		itemName = name;
		itemId = id;
		itemDesc = desc;
		itemIcon = Resources.Load<Texture2D> ("Item Icons/" + name);
		itemAmount = amount;
		itemPower = power;
		itemType = type;
	}

	public Item(){
	}
}
