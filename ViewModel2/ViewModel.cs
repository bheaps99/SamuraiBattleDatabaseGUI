using System.ComponentModel;
using System.Windows.Input;
// using Repositories;
// using Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DomainClasses;
using Repository2;


namespace ViewModel2
{

    public class SamuraiBattleViewModel : INotifyPropertyChanged
    {

        #region  Fields
        protected IRepository Repository;
        //SamuraiBattleViewModel viewModel;
        #region Fields of Samurai

        // store one Samurai
        private Samurai _samurai;
        public Samurai samurai
        {
            get { return _samurai; }
            set
            {
                if (_samurai == value) return;
                _samurai = value;
                RaisePropertyChanged("samurai");
            }
        }

        private IEnumerable<Samurai> _warriors;
        public IEnumerable<Samurai> Warriors   // store the List of Samurais 
        {
            get { return _warriors; }
            set
            {
                if (_warriors == value)
                    return;
                _warriors = value;

                RaisePropertyChanged("Warriors");
            }
        }
        #endregion
        #region fields of Battle

        public Battle _contest;
        public Battle contest
        {
            get { return _contest; }
            set
            {
                if (_contest == value) return;
                _contest = value;
                RaisePropertyChanged("contest");
            }
        }


        private IEnumerable<Battle> _contests;  // store one Battle
        public IEnumerable<Battle> Contests     // store the List of Battles

        {
            get { return _contests; }
            set
            {
                if (_contests == value)
                    return;
                _contests = value;
                // 
                RaisePropertyChanged("Contests");
            }
        }
        #endregion
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public SamuraiBattleViewModel(IRepository repository)
        {
            Repository = repository;
        }



        #endregion

        #region Commands
        #region Commands group1

        #region RefreshCommand1 


        private RefreshCommand1 _refreshSamuraiCommand = new RefreshCommand1();
        public RefreshCommand1 RefreshSamuraiCommand
        {
            get
            {
                if (_refreshSamuraiCommand.ViewModel == null)
                    _refreshSamuraiCommand.ViewModel = this;
                return _refreshSamuraiCommand;
            }
        }

        /// <summary>
        /// RefreshCommand Button 1 : Fetch data of Samurai with Asyn/await
        /// </summary>
        public class RefreshCommand1 : ICommand
        {
            public SamuraiBattleViewModel ViewModel { get; set; }
            public event EventHandler CanExecuteChanged;

            //
            //new
            private bool _isExecuting;
            public bool IsExecuting
            {
                get { return _isExecuting; }

                set
                {
                    _isExecuting = value;
                    CanExecuteChanged?.Invoke(this, new EventArgs());

                }
            }
            public bool CanExecute(object parameter)
            {
                return !IsExecuting;
            }
            public async void Execute(object parameter)
            {
                IsExecuting = true;
                Task t1 = new Task
                (
                  () =>
                  {
                      ViewModel.Warriors = ViewModel.Repository.GetAllSamurai();
                  }
                );
                // start the task
                t1.Start();

                await t1;
                IsExecuting = false;

            }
            // end of new
        }
        #endregion

        #region ClearCommand1 

        private ClearCommand1 _clearSamuraiCommand = new ClearCommand1();
        public ClearCommand1 ClearSamuraiCommand
        {
            get
            {
                if (_clearSamuraiCommand.ViewModel == null)
                    _clearSamuraiCommand.ViewModel = this;
                return _clearSamuraiCommand;
            }
        }

        public class ClearCommand1 : ICommand
        {
            public SamuraiBattleViewModel ViewModel { get; set; }
            public event EventHandler CanExecuteChanged;

            //new
            private bool _isExecuting;
            public bool IsExecuting
            {
                get { return _isExecuting; }

                set
                {
                    _isExecuting = value;
                    CanExecuteChanged?.Invoke(this, new EventArgs());

                }
            }
            public bool CanExecute(object parameter)
            {
                return !IsExecuting;
            }

            public void Execute(object parameter)
            {
                IsExecuting = true;
                try
                {

                    ViewModel.Warriors = new List<Samurai>();

                }
                catch (Exception)
                { }
                IsExecuting = false;
            }
            // end of new
        }

        #endregion
        #region UpdateCommand1  

        private UpdateCommand1 _updateSamuraiCommand = new UpdateCommand1();
        public UpdateCommand1 UpdateSamuraiCommand
        {
            get
            {
                if (_updateSamuraiCommand.ViewModel == null)
                    _updateSamuraiCommand.ViewModel = this;
                return _updateSamuraiCommand;
            }
        }

        public class UpdateCommand1 : ICommand
        {
            public SamuraiBattleViewModel ViewModel { get; set; }
            public event EventHandler CanExecuteChanged;
            //new 
            private bool _isExecuting;
            public bool IsExecuting
            {
                get { return _isExecuting; }

                set
                {
                    _isExecuting = value;
                    CanExecuteChanged?.Invoke(this, new EventArgs());

                }
            }
            public bool CanExecute(object parameter)
            {
                return !IsExecuting;
            }

            public void Execute(object parameter)
            {
                IsExecuting = true;
                try
                {
                    if(ViewModel.samurai != null)
                    ViewModel.Repository.UpdateSamurai(ViewModel.samurai.SamuraiId, ViewModel.samurai.Name,
                                                  ViewModel.samurai.Age,
                                                  ViewModel.samurai.Town);
                }
                catch (Exception)
                { }
                IsExecuting = false;
            }
        }
        #endregion
        #endregion

        #region Commands group2


        /// <summary>
        /// Commands of Battle
        /// </summary>
        #region RefreshCommand2  



        private RefreshCommand2 _refreshBattleCommand = new RefreshCommand2();
        public RefreshCommand2 RefreshBattleCommand
        {
            get
            {
                if (_refreshBattleCommand.ViewModel == null)
                    _refreshBattleCommand.ViewModel = this;
                return _refreshBattleCommand;
            }
        }

        /// <summary>
        /// RefreshCommand Button 2 : Fetch data of Battle with Asyn/await
        /// </summary>
        public class RefreshCommand2 : ICommand
        {
            public SamuraiBattleViewModel ViewModel { get; set; }
            public event EventHandler CanExecuteChanged;
            //
            //new
            private bool _isExecuting;
            public bool IsExecuting
            {
                get { return _isExecuting; }

                set
                {
                    _isExecuting = value;
                    CanExecuteChanged?.Invoke(this, new EventArgs());

                }
            }
            public bool CanExecute(object parameter)
            {
                return !IsExecuting;
            }
            public async void Execute(object parameter)
            {
                IsExecuting = true;
                Task t2 = new Task
                 (
                    () =>
                    {
                        ViewModel.Contests = ViewModel.Repository.GetAllBattle();
                    }
                  );
                // start the task
                t2.Start();
                await t2;

                IsExecuting = false;

            }
            // end of new
        }
        #endregion
        #region ClearCommand2 

        private ClearCommand2 _clearBattleCommand = new ClearCommand2();
        public ClearCommand2 ClearBattleCommand
        {
            get
            {
                if (_clearBattleCommand.ViewModel == null)
                    _clearBattleCommand.ViewModel = this;
                return _clearBattleCommand;
            }
        }

        public class ClearCommand2 : ICommand
        {
            public SamuraiBattleViewModel ViewModel { get; set; }
            public event EventHandler CanExecuteChanged;
            //
            //new
            private bool _isExecuting;
            public bool IsExecuting
            {
                get { return _isExecuting; }

                set
                {
                    _isExecuting = value;
                    CanExecuteChanged?.Invoke(this, new EventArgs());

                }
            }
            public bool CanExecute(object parameter)
            {
                return !IsExecuting;
            }

            public void Execute(object parameter)
            {
                IsExecuting = true;
                try
                {

                    ViewModel.Contests = new List<Battle>();

                }
                catch (Exception)
                { }
                IsExecuting = false;
            }
            // end of new

        }
        #endregion
        #region UpdateCommand2  

        private UpdateCommand2 _updateBattleCommand = new UpdateCommand2();
        public UpdateCommand2 UpdateBattleCommand
        {
            get
            {
                if (_updateBattleCommand.ViewModel == null)
                    _updateBattleCommand.ViewModel = this;
                return _updateBattleCommand;
            }
        }

        public class UpdateCommand2 : ICommand
        {
            public SamuraiBattleViewModel ViewModel { get; set; }
            public event EventHandler CanExecuteChanged;
            //
            //new 
            private bool _isExecuting;
            public bool IsExecuting
            {
                get { return _isExecuting; }

                set
                {
                    _isExecuting = value;
                    CanExecuteChanged?.Invoke(this, new EventArgs());

                }
            }
            public bool CanExecute(object parameter)
            {
                return !IsExecuting;
            }

            public void Execute(object parameter)
            {
                IsExecuting = true;
                try
                {
                    if (ViewModel.contest != null)
                        ViewModel.Repository.UpdateBattle(ViewModel.contest.BattleId, ViewModel.contest.Name,
                                                      ViewModel.contest.Date,
                                                      ViewModel.contest.City,
                                                      ViewModel.contest.Country);

                }
                catch (Exception)
                { }
                IsExecuting = false;
            }
            // end of new

        }
        #endregion
        #region   AddSamuraiCommand
        private AddCommand _addSamuraiCommand = new AddCommand();
        public AddCommand AddSamuraiCommand
        {
            get
            {
                if (_addSamuraiCommand.ViewModel == null)
                    _addSamuraiCommand.ViewModel = this;
                return _addSamuraiCommand;
            }
        }

        public class AddCommand : ICommand
        {
            public SamuraiBattleViewModel ViewModel { get; set; }
            public event EventHandler CanExecuteChanged;

            //new 
            private bool _isExecuting;
            public bool IsExecuting
            {
                get { return _isExecuting; }

                set
                {
                    _isExecuting = value;
                    CanExecuteChanged?.Invoke(this, new EventArgs());

                }
            }
            public bool CanExecute(object parameter)
            {
                return !IsExecuting;
            }

            public void Execute(object parameter)
            {
                IsExecuting = true;
                try
                {
                    if (ViewModel.samurai != null && ViewModel.contest!=null)
                        ViewModel.Repository.AddSamuraiToBattle(ViewModel.samurai.SamuraiId,
                                                            ViewModel.contest.BattleId);

                }
                catch (Exception)
                { }
                IsExecuting = false;
            }
            // end of new

        }
        #endregion
        #endregion end of command group2
        #endregion  end of Commands
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}