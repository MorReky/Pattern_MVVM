using Pattern_MVVM.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_MVVM.ViewModels
{
   internal class MainWindowViewModel:ViewModel
    {
        #region Заголовок окна
        private string _Title = "Анализ статистики ПО";
        /// <summary> Заголовок окна </summary>
        public string Title 
        {
            get => _Title;
            //Или еще проще
            //set
            //{
            //    //Т.К. есть ранее описнное свойство Set, то можем сократить код
            //    //if (Equals(_Title, value)) return;
            //    //_Title = value;
            //    //OnPropertyChanged();
            //    Set(ref _Title, value);
            //}
            set => Set(ref _Title, value);
        }
        #endregion
    }
}
