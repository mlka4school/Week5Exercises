using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySys
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// itemName is screaming in vars for some reason so i'll just put a string definition here and forget
			string itemName = "";
			Dictionary<string,int> invDictionary = new Dictionary<string,int>();

			TopMenu: Console.Clear();
			Console.WriteLine("What would you like to do?");
			Console.WriteLine();
			Console.WriteLine("1. Add an item");
			Console.WriteLine("2. Remove an item");
			Console.WriteLine("3. Update item's quantity");
			Console.WriteLine("4. List all items");
			Console.WriteLine("5. Exit");

			var sel = -1;
			if (int.TryParse(Console.ReadLine(),out sel)){
				switch (sel){
					case 1:
						TryName: Console.WriteLine("What is the name of the item?");
						itemName = Console.ReadLine();
						if (itemName.Length < 1){ // assume it was a mistake and the user wants to exit.
							goto TopMenu;
						}
						if (invDictionary.ContainsKey(itemName)){
							Console.WriteLine("Duplicate item exists");
							goto TryName;
						}
						TryInt: Console.WriteLine("How many of this item (int)?");
						if (int.TryParse(Console.ReadLine(),out sel)){
							if (sel <= 0){
								Console.WriteLine("Must be greater than 0");
								goto TryInt;
							}
							Console.WriteLine("Added item successfully! Press any key to continue...");
							invDictionary.Add(itemName,sel); // why are you such a problem child?
							Console.ReadKey();
							goto TopMenu;
						}else{
							Console.WriteLine("Not a number");
							goto TryInt;
						}
					case 2:
						DelItem: Console.WriteLine("Name the item for deletion");
						var delName = Console.ReadLine();
						if (delName.Length < 1){ // same use case as adding an item
							goto TopMenu;
						}
						if (invDictionary.Remove(delName)){ // oh hey, it returns bool. that makes things easier
							Console.WriteLine("Item has been deleted. Press any key to continue...");
							Console.ReadKey();
						}else{
							Console.WriteLine("That item does not exist");
							goto DelItem;
						}
					goto TopMenu;
					case 3:
						UpdItem: Console.WriteLine("Name the item for updating");
						var updName = Console.ReadLine();
						if (updName.Length < 1){ // same use case as adding/removing an item. you would think i would use a method at this point. you would be wrong
							goto TopMenu;
						}
						if (invDictionary.ContainsKey(updName)){ // oh hey, it returns bool. that makes things easier
							TryNewInt: Console.WriteLine("What will the new value be?");
							if (int.TryParse(Console.ReadLine(),out sel)){
								if (sel <= 0){
									Console.WriteLine("Must be greater than 0");
									goto TryInt;
								}
								invDictionary[updName] = sel;
								Console.WriteLine("Updated item successfully! Press any key to continue...");
								Console.ReadKey();
							}else{
								Console.WriteLine("Not a number");
								goto TryNewInt;
							}
						}else{
							Console.WriteLine("That item does not exist");
							goto UpdItem;
						}
					goto TopMenu;
					case 4:
						if (invDictionary.Count == 0){
							Console.WriteLine("There are no items in inventory");
						}else{
							Console.WriteLine("Listing all items in inventory");
							foreach (KeyValuePair<string,int> item in invDictionary){
								Console.WriteLine(item.Value.ToString()+"x of "+item.Key);
							}
						}
						Console.WriteLine("Press any key to continue...");
						Console.ReadKey();
					goto TopMenu;
					case 5:
					return;
					default:
					goto TopMenu;
				}
			}else{
				goto TopMenu;
			}
		}
	}
}
