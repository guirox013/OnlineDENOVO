using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Inventory : MonoBehaviour {

	// Variaveis do inventario
	public int slotsX, slotsY;
	public GUISkin skin;
	public List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item> ();
	private ItemDatabase database;

	// Variaveis de apoio
	public bool showInventory;
	public bool showToolTip;
	private string toolTip;
	private bool draggingItem;
	private Item draggedItem;
	private int prevIndex;

	// Use this for initialization
	void Start () {
		// Inicializa o inventario com items vazios
		for (int i = 0; i < (slotsX*slotsY); i++){
			slots.Add(new Item());
			inventory.Add (new Item());
		}
		// Adiciona alguns itens
		database = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<ItemDatabase>();
		AddItem (1);
		AddItem (2);
		AddItem (2);
	}
	
	// Update is called once per frame
	void Update () {
		// Abre/fecha o inventario e esconde/mostra o cursos
		if (Input.GetButtonDown ("Inventory")) {
			showInventory = !showInventory;
			Screen.showCursor = !Screen.showCursor;
		}
	}

	void OnGUI (){
		toolTip = "";
		GUI.skin = skin;

		// Abre o inventario caso a variavel seja true
		if (showInventory) {
			DrawInventory ();
		}

		// Abre o tooltip caso a variavel seja true
		if (showToolTip && showInventory)
			GUI.Box (new Rect (Event.current.mousePosition.x + 15f ,Event.current.mousePosition.y, 200,200), toolTip, skin.GetStyle("ToolTip"));

		// Exibe o icone do item sendo arrastado
		if (draggingItem)
			GUI.DrawTexture (new Rect (Event.current.mousePosition.x,Event.current.mousePosition.y, 50,50), draggedItem.itemIcon);
	}

	void DrawInventory(){
		Event e = Event.current;
		int i = 0;

		// Monta o Grid do inventario
		for (int y = 0; y< slotsY; y++)
		{
			for (int x = 0; x < slotsX; x++){
				Rect slotRect = (new Rect(x*60, y*60, 50, 50));
				GUI.Box (slotRect,"", skin.GetStyle("Slot"));
				slots[i] = inventory[i];

				// Coloca as informaçoes no slot caso um item esteja adicionado
				if (slots[i].itemName != null){
					GUI.DrawTexture (slotRect, slots[i].itemIcon);
					if (slotRect.Contains(e.mousePosition)){
						toolTip = CreateToolTip(inventory[i]);
						showToolTip = true;

						// Pega e arrasta um item
						if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem){
							draggingItem = true;
							prevIndex = i;
							draggedItem = slots[i];
							inventory[i] = new Item();
						}

						// Drop item em um espaço que ja tem um item
						if(e.type == EventType.mouseUp && draggingItem){
							inventory[prevIndex] = inventory[i];
							inventory[i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
					}

				// Dropa item em um espaço nulo
				} else {
					if (slotRect.Contains (e.mousePosition)){
						if (e.type == EventType.mouseUp && draggingItem){
							inventory[i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						} 
					}
				}
				// Controla o tooltip
				if (toolTip == "")
					showToolTip = false;
				i++;
			}
		}
	}

	// Cria um tooltipe com base nos dados do item
	string CreateToolTip(Item item){
		toolTip = "<color=#4DA4BF>" + item.itemName + "</color>" + "<color=#f2f2f2>" + " x " + "</color>" + "<color=#ff0000>" + item.itemAmount + "</color>\n\n" + "<color=#f2f2f2>" + item.itemDesc + "</color>\n\n" + "<color=#0000ff>" + item.itemType + "</color>";
		return toolTip;
	}

	// Adiciona um Item no inventario
	void AddItem (int id){
		for (int i = 0; i < inventory.Count; i ++){
			if (inventory[i].itemName == null){
				for (int j = 0; j < database.items.Count; j++){
					if (database.items[j].itemId == id){
						if (InventoryContains(id)){
							AddExistenceItem(id);
							return;
						}
						inventory[i] = database.items[j];
						print (inventory[i].itemId);
						return;
					}
				}
			}
		}
	}

	// Remove um item do inventario
	void removeItem (int id){
		for (int i=0; i < inventory.Count; i++){
			if (inventory[i].itemId == id){
				if (inventory[i].itemAmount > 1){
					inventory[i].itemAmount--;
					break;
				}
				inventory[i] = new Item();
				break;
			}
		}
	}

	// Checa se um item consta no inventario
	bool InventoryContains(int id){
		foreach(Item item in inventory){
			if(item.itemId == id) return true;
		}
		return false;
	}

	// Adiciona um item que ja existe no inventario
	void AddExistenceItem(int id){
		for (int i=0; i < inventory.Count; i++){
			if (inventory[i].itemId == id){
				inventory[i].itemAmount++;
				break;
			}
		}
	}
}
