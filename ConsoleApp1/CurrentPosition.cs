using System;

namespace ConsoleApp1
{
	public struct CurrentPosition
	{
		public int Row { get; private set; }
		public int Column { get; private set; }
		public CurrentPosition(int row, int col)
		{
			Row = row;
			Column = col;
		}

		public static implicit operator CurrentPosition(string input)
		{
			if (string.IsNullOrEmpty(input))
				throw new ArgumentNullException(nameof(input));
			var inputs = input.Split(',');
			if (!string.IsNullOrEmpty(inputs[0]) && !string.IsNullOrEmpty(inputs[1]))
				if (int.TryParse(inputs[1].Trim(), out var row) && int.TryParse(inputs[0].Trim(), out var col))
					return new CurrentPosition(row, col);
			throw new InvalidOperationException("invalid current position");
		}

		public void SetPosition(int row, int column)
		{
			Row = row;
			Column = column;
		}

		public static CurrentPosition operator +(CurrentPosition pos, int step)
			=> new CurrentPosition(pos.Row + step, pos.Column);
		public static CurrentPosition operator -(CurrentPosition pos, int step)
			=> new CurrentPosition(pos.Row - step, pos.Column);

		public static CurrentPosition operator *(CurrentPosition pos, int step)
			=> new CurrentPosition(pos.Row, pos.Column + step);
		public static CurrentPosition operator /(CurrentPosition pos, int step)
			=> new CurrentPosition(pos.Row, pos.Column - step);
	}
}
