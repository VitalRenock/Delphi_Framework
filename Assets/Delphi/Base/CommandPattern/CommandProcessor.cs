using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommandProcessor : MonoBehaviour
{
	[ShowInInspector] [ReadOnly] protected List<Command> commands = new List<Command>();

	public virtual void ExecuteCommand(Command command)
	{
		commands.Add(command);
		command.Execute();
	}
	public virtual void UndoCommand()
	{
		if (commands.Count > 0)
		{
			commands[commands.Count - 1].Undo();
			commands.RemoveAt(commands.Count - 1);
		}
	}
}