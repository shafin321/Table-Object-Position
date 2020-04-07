namespace ConsoleApp1
{
	public class Matrix
	{
		public MatrixDimension Dimension { get; }

		public Matrix(MatrixDimension dimension) 
			=> Dimension = dimension;
	}
}
