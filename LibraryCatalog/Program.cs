using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCatalog
{
	internal class Program
	{
		static void Main(string[] args)
		{

			// add the dictionary.
			Dictionary<string,Book> Catalog = new Dictionary<string, Book>();
			//IEnumerable<KeyValuePair<string,Book>> CatSearch;
			// i'll make them here because i like new lines
			Catalog.Add("AAA-1000",new Book("Secrets to coolness","Cool Guy"));
			Catalog.Add("AAA-1010",new Book("Shades at Midnight","Cool Guy"));
			Catalog.Add("AAA-1001",new Book("The recipe book of all time","Chef G"));
			Catalog.Add("AAA-1100",new Book("Where the time goes","Clock Master"));
			Catalog.Add("AAA-1011",new Book("24/7","Clock Master"));
			Catalog.Add("AAA-1110",new Book("To tie a ribbon","Rose Red"));

			// records made! now for my favorite! menus :sleeping_accomodation:
			var sel = -1;
			TopMenu: Console.Clear();
			Console.WriteLine("Select an option");
			Console.WriteLine("1. List all books");	
			Console.WriteLine("2. Search books by...");
			Console.WriteLine("3. Checkout/Return book");
			if (int.TryParse(Console.ReadLine(),out sel)){
				switch (sel){
					case 1:
						Console.WriteLine("Listing all books");
						ListBooks(Catalog);
					goto TopMenu;
					case 2:
						Console.WriteLine("1. Title");
						Console.WriteLine("2. Author");
						Console.WriteLine("3. Checked in");
						Console.WriteLine("4. Checked out");
						if (int.TryParse(Console.ReadLine(),out sel)){
							// ask for the name if we're 1 or 2 and then be done with it
							switch (sel){
								case 1:
									var finalTTL = Console.ReadLine();
									var TTLSearch = from book in Catalog where book.Value.Title == finalTTL select book;
									ListBooks(TTLSearch.ToDictionary(book => book.Key, book => book.Value));
								break;
								case 2:
									var finalAut = Console.ReadLine();
									var AutSearch = from book in Catalog where book.Value.Author == finalAut select book;
									ListBooks(AutSearch.ToDictionary(book => book.Key, book => book.Value));
								break;
								case 3:
									var InSearch = from book in Catalog where book.Value.CheckedOut == false select book;
									ListBooks(InSearch.ToDictionary(book => book.Key, book => book.Value));
								break;
								case 4:
									var OutSearch = from book in Catalog where book.Value.CheckedOut == true select book;
									ListBooks(OutSearch.ToDictionary(book => book.Key, book => book.Value));
								break;
							}
						}
					goto TopMenu;
					case 3:
						Console.WriteLine("Enter the ISBN to modify");
						var setISBN = Console.ReadLine();
						if (Catalog.ContainsKey(setISBN)){
							if (Catalog[setISBN].CheckedOut){
								Console.WriteLine(Catalog[setISBN].Title+" has been checked in.");
							}else{
								Console.WriteLine(Catalog[setISBN].Title+" has been checked out.");
							}
							Catalog[setISBN].CheckedOut = !Catalog[setISBN].CheckedOut;
							Console.WriteLine("Press any key to continue...");
							Console.ReadKey();
						}else{
							Console.WriteLine("Book does not exist. Press any key to continue...");
							Console.ReadKey();
						}
					goto TopMenu;
				}
			}
		}

		static void ListBooks(Dictionary<string,Book> bookList){
			if (bookList.Count == 0){
				Console.WriteLine("No results found");
			}else{
				foreach (KeyValuePair<string,Book> book in bookList){
					var separator = " - IN  - ";
					if (book.Value.CheckedOut) separator = " - OUT - ";
					Console.WriteLine(book.Key+separator+book.Value.Title+" by "+book.Value.Author);
				}
			}
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}

		public class Book{
			// ISBN will be used as the key. so it won't go in the book class itself
			public Book(string newTitle, string newAuth){
				// we won't bother with the checked out status. by default all books are checked in
				Title = newTitle;
				Author = newAuth;
				CheckedOut = false;
			}
			public string Title { get; set; }
			public string Author { get; set; }
			public bool CheckedOut { get; set; } // we only need two values. so a bool makes sense
		}
	}
}
