using System;

namespace ConsoleApp1
{
	public struct MatrixDimension
	{
		public int Rows { get; }
		public int Columns { get; }
		private MatrixDimension(int rows, int columns)
		{
			Rows = rows;
			Columns = columns;
		}

		public static implicit operator MatrixDimension(string dimension)
		{
			var dimensionParts = dimension.Split(',');
			if (dimensionParts.Length == 0 || dimensionParts.Length > 2)
				throw new ArgumentException("invalid dimension");
			if (string.IsNullOrEmpty(dimensionParts[0]) || string.IsNullOrEmpty(dimensionParts[1]))
				throw new ArgumentException("invalid dimension");
			if (!int.TryParse(dimensionParts[1], out var col))
				throw new InvalidCastException("invalid number of row");
			if (!int.TryParse(dimensionParts[0], out var row))
				throw new InvalidCastException("invalid number of column");

			return new MatrixDimension(row, col);
		}
	}
}
