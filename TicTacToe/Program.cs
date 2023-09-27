using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// i have renewed purpose
			int[,] ticBoard = new int[3,3];
			int currentTurn = 0;
			bool CPUmode = false;
			string failmessage = "";
			
			StartGame: Console.Clear();
			Console.WriteLine("Select a mode:");
			
			Console.WriteLine("1. P1 vs P2");
			Console.WriteLine("2. P1 vs CPU");
			Console.WriteLine("3. Exit");

			var selection = 0;
			if (int.TryParse(Console.ReadLine(),out selection)){
				switch (selection){
					case 1:
						CPUmode = false;
					goto GameLogic;
					case 2:
						CPUmode = true;
					goto GameLogic;
					case 3:
					return;
				}
			}else goto StartGame;

			GameLogic: 
			currentTurn = 1;
			ticBoard = new int[3,3];
			NextTurn: 
			// probably a good idea to check the win condition here. there are an infinite amount of ways to do this better but we'll do it my way.
			for (var v = 0; v < 3; ++v){
				for (var h = 0; h < 3; ++h){
					// vertical checks
					if (v == 1 && h == 1){
						if (ticBoard[0,0] == currentTurn+1 && ticBoard[1,1] == currentTurn+1 && ticBoard[2,2] == currentTurn+1) goto GameOver;
						if (ticBoard[2,0] == currentTurn+1 && ticBoard[1,1] == currentTurn+1 && ticBoard[0,2] == currentTurn+1) goto GameOver;
					}else{
						if (v == 1){
							if (ticBoard[h,0] == currentTurn+1 && ticBoard[h,1] == currentTurn+1 && ticBoard[h,2] == currentTurn+1) goto GameOver;
						}
						// horizontal checks
						if (h == 1){
							if (ticBoard[0,v] == currentTurn+1 && ticBoard[1,v] == currentTurn+1 && ticBoard[2,v] == currentTurn+1) goto GameOver;
						}
					}
				}
			}
			// check the draw condition too
			++currentTurn; if (currentTurn > 1) currentTurn = 0;
			RetryTurn: Console.Clear();
			if (failmessage != "") Console.WriteLine(failmessage);
			Console.WriteLine("It is Player "+(currentTurn+1).ToString()+"'s turn");
			DrawBoard(ticBoard);
			Console.WriteLine("Type out your next move (xy)");
			var move = 0;
			if (int.TryParse(Console.ReadLine(),out move)){
				// convert this into something trymove can actually use
				var xtry = -1;
				var ytry = -1;
				if (move.ToString().Length == 2){
					var mvstring = move.ToString();
					xtry = int.Parse(mvstring.Substring(0,1));
					ytry = int.Parse(mvstring.Substring(1,1));
					if (TryMove(xtry,ytry,ref ticBoard,ref currentTurn,ref failmessage)){
						goto NextTurn;
					}else{
						goto RetryTurn;
					}
				}else{
					failmessage = "Incorrect format (eg. try 11, 23, etc.)";
					goto RetryTurn;
				}
			}else{
				failmessage = "Invalid cast";
				goto RetryTurn;
			}

			GameOver: Console.Clear();
			Console.WriteLine("Player "+(currentTurn+1).ToString()+" wins!");
			DrawBoard(ticBoard);
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
			goto StartGame;
		}

		static void DrawBoard(int[,] boardVar){
			// draw the top of the grid
			Console.WriteLine("   "+" 1 "+" 2 "+" 3 ");
			for (var v = 0; v < 3; ++v){
				Console.Write(" "+(v+1).ToString()+" ");
				for (var h = 0; h < 3; ++h){
					switch (boardVar[h,v]){
						case 1:
							Console.Write(" O ");
						break;
						case 2:
							Console.Write(" X ");
						break;
						default:
							Console.Write(" - ");
						break;
					}
				}
				Console.WriteLine();
			}
		}

		static bool TryMove(int xcord, int ycord, ref int[,] board, ref int playernum, ref string fail){
			--xcord; --ycord; // for computer math friendly reasons
			if (board[xcord,ycord] > 0){
				fail = "Illegal move";
				return false;
			}else{
				fail = "";
				board[xcord,ycord] = playernum+1;
				return true;
			}
		}
	}
}
