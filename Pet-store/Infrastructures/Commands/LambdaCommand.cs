using System;
using World_of_books.Infrastructures.Commands.Base;

namespace World_of_books.Infrastructures.Commands
{
    internal class LambdaCommand : CommandBase
    {
        private readonly Action<object> _excute;
        private readonly Func<object, bool>? _canExcute;

        public LambdaCommand(Action<object> excute, Func<object, bool>? canExcute = null)
        {
            _excute = excute ?? throw new ArgumentNullException(nameof(excute));
            _canExcute = canExcute;
        }

        public override bool CanExecute(object parameter) => _canExcute?.Invoke(parameter) ?? true;
        public override void Execute(object parameter) => _excute(parameter);
    }
}
