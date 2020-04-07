using System;

namespace ConsoleApp1
{
	public struct Command
	{
		public Direction Direction { get; }

		public Command(int direction)
			=> Direction = direction switch
			{
				1 => Direction.Forward,
				2 => Direction.Backward,
				_ => Direction.Print,
			};
	}
}
