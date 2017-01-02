using System;
using System.Collections.Generic;
using System.Linq;

namespace RPChat.Commands
{
    /// <summary>
    /// Represents a command - the base of client &lt;---&gt; server communication.
    /// </summary>
    /// <typeparam name="TOrigin">The origin of the command.</typeparam>
    /// <typeparam name="TTarget">The target of the command.</typeparam>
    /// <typeparam name="TContext">The context in which the command is supposed to be executed.</typeparam>
    public abstract class Command<TOrigin, TTarget, TContext>
        where TOrigin : class, ICommandOrigin
        where TTarget : class, ICommandTarget
        where TContext : class, ICommandContext
    {
        public string Id { get; private set; }

        public TOrigin Origin { get; set; }
        public TTarget Target { get; private set; }

        protected Command(TTarget target = null)
        {
            Id = GetType().FullName;
            Target = target;
        }

        public abstract void Execute(TContext context);
    }
}