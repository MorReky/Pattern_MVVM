using Pattern_MVVM.Infrastructure.Commands;
using Pattern_MVVM.Models;
using Pattern_MVVM.Models.Decanat;
using Pattern_MVVM.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pattern_MVVM.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public ObservableCollection<Group> Groups { get; }
        /*-------------------------------------------------*/
        #region Выбранная страница
        /// <summary>Номер выбранной вкладки</summary>
        private int _SelectedPageIndex;

        /// <summary>Номер выбранной вкладки</summary>
        public int SelectedPageIndex
        {
            get => _SelectedPageIndex;
            set => Set(ref _SelectedPageIndex, value);
        }
        #endregion

        #region Тестовые данные для графика
        //3:55:00
        /// <summary>
        /// Тестовый набор данных для визуализации графиков
        /// </summary>
        private IEnumerable<DataPoint> _TestDataPoints;
        /// <summary>
        /// Тестовый набор данных для визуализации графиков
        /// </summary>
        public IEnumerable<DataPoint> TestDataPoints { get => _TestDataPoints; set => Set(ref _TestDataPoints, value); }
        #endregion

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
        /*-------------------------------------------------*/

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
        #region ChangeTabIndexCommand
        public ICommand ChangeTabIndexCommand { get; }

        private void OnChangeTabIndexCommandExecute(object p)
        {
            if (p is null) return;
            SelectedPageIndex +=Convert.ToInt32(p);
        }

        private bool CanChangeTabIndexCommand(object p)=>_SelectedPageIndex >=0;
        #endregion
        #endregion

        /*-------------------------------------------------*/

        public MainWindowViewModel()
        {
            #region Комманды
            //запихиваем в свойство комманду
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted,CanCloseApplicationCommandExecute);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecute, CanChangeTabIndexCommand);

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
            //Создали перечисление от 1 до 10,далее делаем преобразование,берем число и на его основе создаем студентов
            var student_index = 1;
            var students = Enumerable.Range(1, 10).Select(i => new Student()
            {
                Name = $"Name {student_index}",
                Surname = $"Surname {student_index}",
                Patronymic = $"Patronymic {student_index++}",
                Birthday = DateTime.Now,
                Rating = 0
            }) ;

            //Создали перечисление от 1 до 20,далее делаем преобразование,берем число и на его основе делаем группу
            var groups=Enumerable.Range(1,20).Select(i=>new Group()
            { 
            Name=$"Группа {i}",
            Students=new ObservableCollection<Student>(students)
            });

            Groups = new ObservableCollection<Group>(groups);

        }
        /*-----------------------------------------------*/
    }
}
