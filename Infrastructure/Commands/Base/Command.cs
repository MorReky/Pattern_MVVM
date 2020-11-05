using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pattern_MVVM.Infrastructure.Commands.Base
{
    internal abstract class Command : ICommand
    {
        //Событие перехода состояния возможности выполнения комманды CanExecute
        public event EventHandler CanExecuteChanged
        {
            //Сделали так,чтобы WPF сама управляла союытием EventHandler
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        //реализацией этих методов займется наследник

        /// <summary>
        /// Проверка выполнения некторого условия для выполнения комманды
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public abstract bool CanExecute(object parameter);
        /// <summary>
        /// Основная логика комманды
        /// </summary>
        /// <param name="parameter"></param>
        public abstract void Execute(object parameter);
    }
}
