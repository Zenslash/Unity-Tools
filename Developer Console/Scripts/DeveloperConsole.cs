using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeveloperConsole
{
    
    private readonly string _prefix;
    private readonly IEnumerable<IConsoleCommand> _commands;

    public DeveloperConsole(string prefix, IEnumerable<IConsoleCommand> commands)
    {
        this._prefix = prefix;
        this._commands = commands;
    }

    public void ProcessCommand(string inputValue)
    {
        if(!inputValue.StartsWith(_prefix))
        {
            return;
        }

        inputValue = inputValue.Remove(0, _prefix.Length);

        string[] inputSplit = inputValue.Split(' ');

        string commandInput = inputSplit[0];
        string[] args = inputSplit.Skip(1).ToArray();

        ProcessCommand(commandInput, args);
    }

    public void ProcessCommand(string commandInput, string[] args)
    {
        foreach(var command in _commands)
        {
            if (!commandInput.Equals(command.CommandWord, System.StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }
            
            if(command.Process(args))
            {
                return;
            }
        }
    }

}
