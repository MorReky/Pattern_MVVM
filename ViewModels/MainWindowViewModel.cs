using Pattern_MVVM.Infrastructure.Commands;
using Pattern_MVVM.Models;
using Pattern_MVVM.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pattern_MVVM.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        //3:55:00
        /// <summary>
        /// Тестовый набор данных для визуализации графиков
        /// </summary>
        private IEnumerable<DataPoint> _TestDataPoints;
        /// <summary>
        /// Тестовый набор данных для визуализации графиков
        /// </summary>
        public IEnumerable<DataPoint> TestDataPoints { get => _TestDataPoints; set => Set(ref _TestDataPoints, value); }

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

        #region Статус программы 
        private string _Status = "Готов!";
        /// <summary> Статус программы </summary>
        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }
        #endregion

        #region Комманды
        #region CloseApplicationCommand
        //В этом свойстве находится сама комманда, а методы лишь оперделяют её
        public ICommand CloseApplicationCommand { get; }
        //Выполняется в момент выполнения комманды
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        #endregion
        #endregion

        public MainWindowViewModel()
        {
            #region Комманды
            //запихиваем в свойство комманду
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted,CanCloseApplicationCommandExecute);

            #endregion
            var data_points = new List<DataPoint>((int)(360/0.1));
            for(var x=0d;x<=360; x+=0.1)
            {
                //неважно где находится,компилятор сам вынесет ее
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(x*to_rad);

                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }
            TestDataPoints = data_points;
        }
    }
}
