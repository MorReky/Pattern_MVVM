using Pattern_MVVM.Infrastructure.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_MVVM.Infrastructure.Commands
{
    internal class LambdaCommand : Command
    {
        
        private readonly Action<object> _Execute;
        private readonly Func<object, bool> _CanExecute;
        //ЧТО?
        //2:12:30
        //Получаем 2 делегата, которые будут выполняться CanExecute и Execute
        //Делегаты - это указатели на методы
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Execute">Комманды могут из разметки получать параметры, потому он принимает Object</param>
        /// <param name="CanExecute"></param>
        public LambdaCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            //Исключение, если в Execute не передали ссылку на делегат
            _Execute = Execute??throw new ArgumentNullException(nameof(Execute));
            _CanExecute = CanExecute;
        }
        public override bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter)??true;
        public override void Execute(object parameter) => _Execute(parameter);
    }
}
