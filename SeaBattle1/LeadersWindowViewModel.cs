using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SeaBattle
{

    public class LeadersWindowViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p_UserScore">Users Score Value</param>
        /// <param name="p_EnemyScore">Computers Score Value</param>
        /// <param name="p_isWinner">If User won</param>
        public LeadersWindowViewModel(int p_UserScore, int p_EnemyScore, bool p_isWinner)
        {
            UserScores = p_UserScore;
            EnemyScores = p_EnemyScore;

            if (p_isWinner)
            {
                IsNewLeader = LeadershipReaderWriter.Instance.IsNewLeader(p_UserScore);

                if (_isNewLeader)
                {
                    AddNewLeader = new LeaderButtonClickCommand(p_UserScore, p_EnemyScore);
                    AddNewLeader.Executed += AddNewLeader_Executed;
                }

                Message = "Congrats! You won!";
            }
            else
            {
                Message = "Sorry, you lost";

                if (LeadershipReaderWriter.Instance.IsNewLeader(p_EnemyScore))
	            {
		             Leader _leader = new Leader("Computer", p_EnemyScore, DateTime.Now.ToString("d"));
                     LeadershipReaderWriter.Instance.AddNewLeader(_leader);
	            }                
            }

            Leaders = LeadershipReaderWriter.Instance.ReadLeaders();
        }

        
        public List<Leader> Leaders
        {
            get
            {
                return _Leaders;
            }
            set
            {
                _Leaders = value;
                OnPropertyChanged("Leaders");
            }
        }
        List<Leader> _Leaders;

        public LeaderButtonClickCommand AddNewLeader { get; set; }

        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }
        string _message;

        public bool IsNewLeader
        {
            get
            {
                return _isNewLeader;
            }
            set
            {
                _isNewLeader = value;
                OnPropertyChanged("IsNewLeader");
            }
        }
        bool _isNewLeader = false;

        public string NewLeaderName
        {
            get
            {
                return _newLeaderName;
            }
            set
            {
                _newLeaderName = value;
                OnPropertyChanged("NewLeaderName");
            }
        }
        string _newLeaderName;

        public int UserScores
        {
            get
            {
                return _userScores;
            }
            set
            {
                _userScores = value;
                OnPropertyChanged("UserScores");
            }
        }
        int _userScores;

        public int EnemyScores
        {
            get
            {
                return _enemyScores;
            }
            set
            {
                _enemyScores = value;
                OnPropertyChanged("EnemyScores");
            }
        }
        int _enemyScores;

        private void AddNewLeader_Executed(object sender, EventArgs e)
        {
            Leaders = LeadershipReaderWriter.Instance.ReadLeaders();
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

    public class LeaderButtonClickCommand : ICommand
    {
        //string _Name;
        int _userScore;
        int _enemyScore;

        bool _canExecute = true;

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public event EventHandler Executed;

        public void Execute(object parameter)
        {
            string _Name = parameter.ToString();

            Leader _possibleLeader = new Leader(_Name, _userScore, DateTime.Now.ToString("d"));

            LeadershipReaderWriter.Instance.AddNewLeader(_possibleLeader);

            _canExecute = false;

            CanExecuteChanged(this, EventArgs.Empty);

            Executed(this, EventArgs.Empty);
        }

        public LeaderButtonClickCommand( int p_UserScore, int p_EnemyScore)
        {
            _userScore = p_UserScore;
            _enemyScore = p_EnemyScore;
        }
    }

    
}
