using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
	public class Player
	{
		public CurrentPosition CurrentPosition;
		private Matrix _currentBoard;
		public IList<Command> Commands { get; }
		public Queue<Rotatation> Rotations { get; }
		public Facing Face { get; private set; } = Facing.North;

		public Player()
		{
			Commands = new List<Command>();
			Rotations = new Queue<Rotatation>();
		}

		public void Move(Direction dir, Rotatation? rotation)
		{
			switch (Face)
			{
				case Facing.North:
				if (dir == Direction.Forward)
					CurrentPosition -= 1;
				else if (dir == Direction.Backward)
					CurrentPosition += 1;
				if (rotation.HasValue)
				{
					if (rotation == Rotatation.Clockwise)
						Face = Facing.East;
					else if (rotation == Rotatation.Anticlockwise)
						Face = Facing.West;
				}
				break;
				case Facing.South:
				if (dir == Direction.Forward)
					CurrentPosition += 1;
				else if (dir == Direction.Backward)
					CurrentPosition -= 1;
				if (rotation == Rotatation.Clockwise)
					Face = Facing.West;
				else if (rotation == Rotatation.Anticlockwise)
					Face = Facing.East;
				break;
				case Facing.East:
				if (dir == Direction.Forward)
					CurrentPosition *= 1;
				else if (dir == Direction.Backward)
					CurrentPosition /= 1; 
				if (rotation.HasValue)
				{
					if (rotation == Rotatation.Clockwise)
						Face = Facing.South;
					else if (rotation == Rotatation.Anticlockwise)
						Face = Facing.North;
				}
				break;
				case Facing.West:
				if (dir == Direction.Forward)
					CurrentPosition /= 1;
				else if (dir == Direction.Backward)
					CurrentPosition *= 1;

				if (rotation == Rotatation.Clockwise)
					Face = Facing.North;
				else if (rotation == Rotatation.Anticlockwise)
					Face = Facing.South;
				break;
				default: break;
			}
			if (
				CurrentPosition.Row < 0 ||
				CurrentPosition.Column < 0 ||
				CurrentPosition.Row >= _currentBoard.Dimension.Rows ||
				CurrentPosition.Column >= _currentBoard.Dimension.Columns
			)
				CurrentPosition.SetPosition(-1, -1);
		}

		public void SetBoard(Func<MatrixDimension> dimensionFunction)
		{
			if (dimensionFunction == null)
				throw new ArgumentNullException(nameof(dimensionFunction));
			_currentBoard = new Matrix(dimensionFunction());
		}

		public void SetCurrentPosition(Func<string> positionFunction)
		{
			if (positionFunction == null)
				throw new ArgumentNullException(nameof(positionFunction));
			var position = positionFunction();
			if (string.IsNullOrEmpty(position))
				throw new ArgumentException("invalid position");
			CurrentPosition = position;
		}

		public void SetCommands(Func<string> commandFunction)
		{
			if (commandFunction == null)
				throw new ArgumentNullException(nameof(commandFunction));
			var command = commandFunction();
			if (string.IsNullOrEmpty(command))
				throw new ArgumentException("command can not be null");
			var commands = command.Split(',');
			if (commands.Length == 0)
				throw new ArgumentException("command can not be empty");
			foreach (var c in commands)
			{
				if (!int.TryParse(c, out var commandInt))
					throw new InvalidCastException("invalid command");
				if (commandInt < 0 || commandInt > 4)
					throw new InvalidOperationException("invalid command");
				if (commandInt == 0 || commandInt == 1 || commandInt == 2)
					Commands.Add(new Command(commandInt));
				else if (commandInt == 3)
					Rotations.Enqueue(Rotatation.Clockwise);
				else if (commandInt == 4)
					Rotations.Enqueue(Rotatation.Anticlockwise);
			}
		}
	}
}
