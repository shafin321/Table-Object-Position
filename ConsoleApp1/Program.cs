using System;
using System.Linq;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			var player = new Player();
			player.SetBoard(() => Console.ReadLine());
			player.SetCurrentPosition(() => Console.ReadLine());
			player.SetCommands(() => Console.ReadLine());
			if (player.Commands.Last().Direction != Direction.Print)
				throw new InvalidOperationException("no terminating command found");

			foreach (var cmd in player.Commands)
			{
				Rotatation rotation;
				try
				{
					rotation = player.Rotations.Dequeue();
					player.Move(cmd.Direction, rotation);
				}
				catch
				{
					player.Move(cmd.Direction, null);
				}
				if (cmd.Direction == Direction.Print)
					break;
			}
			Console.WriteLine($"[{player.CurrentPosition.Column},{player.CurrentPosition.Row}]");
			Console.ReadKey();
		}
	}
}
