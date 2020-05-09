using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace SeaBattle
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public Game MyGame
        {
            get
            {
                return _Game;
            }
            set
            {
                _Game = value;
                OnPropertyChanged("MyGame");
            }
        }

        Game _Game;

        public BattleFieldViewModel UserBattleFieldVM { get; set; }
        public BattleFieldViewModel EnemyBattleFieldVM { get; set; }

        public StageButtonClickCommand SetStage2 { get; set; }
        public StageButtonClickCommand SetStage3 { get; set; }

        public MainWindowViewModel()
        {
            MyGame = new Game();

            UserBattleFieldVM = new BattleFieldViewModel(_Game.UserBattleField);
            EnemyBattleFieldVM = new BattleFieldViewModel(_Game.EnemyBattleField);

            SetStage2 = new StageButtonClickCommand(_Game, GameStage.Playing);
            SetStage3 = new StageButtonClickCommand(_Game, GameStage.Finished);

            MyGame.PropertyChanged += _Game_PropertyChanged;
        }

        private void _Game_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Stage")
            {
                if (_Game.Stage==GameStage.Playing)
                {
                    // 1.Check if the User arranged his Boats correctly

                    _Game.UserBattleField.ParseArrangement();
                    bool _isArrangedCorrectly = false;
                    string _errorMessage;

                    _isArrangedCorrectly = _Game.UserBattleField.CheckArrangement(out _errorMessage);
                    if (_isArrangedCorrectly)
                    {                        
                        MessageBox.Show(_errorMessage); //successfully arranged

                        // 2. Fill Enemy's BattleField automatically
                        _Game.EnemyBattleField.ArrangeAutomatically();
                    }
                    else
                    {
                        _Game.Stage = GameStage.BoatsArrange;

                        MessageBox.Show(_errorMessage);                        
                    }
                }
                else if (_Game.Stage==GameStage.Finished)
                {
                    LeadersWindowViewModel LeadersWindowVM = new LeadersWindowViewModel(MyGame.UserScores, MyGame.EnemyScores,  (MyGame.Result == GameResult.Victory) );

                    LeadersWindow MyLeadersWindow = new LeadersWindow(LeadersWindowVM);

                    MyLeadersWindow.Show();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class StageButtonClickCommand : ICommand
    {
        Game _Game;

        GameStage _TargetStage;

        //TODO OnChange
        public bool CanExecute(object parameter)
        {
            return _Game.IsTargetStageAvailable(_TargetStage);
        }

        public event EventHandler CanExecuteChanged;

        private void Stage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public void Execute(object parameter)
        {
            _Game.Stage = _TargetStage;
        }

        public StageButtonClickCommand(Game p_Game, GameStage p_TargetStage)
        {
            _Game = p_Game;
            _TargetStage = p_TargetStage;

            _Game.PropertyChanged += new PropertyChangedEventHandler(Stage_PropertyChanged);
        }
    }
}
