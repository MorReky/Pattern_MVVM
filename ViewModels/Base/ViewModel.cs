using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_MVVM.ViewModels.Base
{
    //internal-внутренние типы или члены доступны только внутри файлов в той же сборке
    internal abstract class ViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // protected-Доступ к защищенному элементу может быть получен из соответствующего класса, а также экземплярами производных классов.
        //Те методы и свойства, которые мы хотим сделать доступными для переопределения, в базовом классе помечается модификатором virtual
        //При использовании CallerMemberName компилятор автоматически подставит имя вызывающего объекта в параметр
        protected virtual void OnPropertyChanged([CallerMemberName]string PropertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        /// <summary>
        /// Обновление значения свойства для которого определено поле, в котором это свойство хранит свои данные.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">  Сюда попадает ссылка на свойство</param>
        /// <param name="value"> Новое значение</param>
        /// <param name="PeopsertName">Имя свойства которое передастся в OnPropertyChanged</param>
        /// <returns></returns>
        protected virtual bool Set<T>(ref T field,T value,[CallerMemberName] string PropertyName = null)
        {
            //Задача метода: Разрешить кольцевые изменения свойств,которые могут возникать
            //Чтобы стэк,например, не переполнялся
            if (Equals(field, value)) return false;//Если свойство уже имеет устанавливаемое значение, вернет false
            field = value;
            OnPropertyChanged(PropertyName);
            return false;

        }
    }
}
