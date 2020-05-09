using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Xml;

namespace SeaBattle
{
    /// <summary>
    /// NOT thread-safe class to read the leaders from file.
    /// </summary>
    public class LeadershipReaderWriter
    {
        private LeadershipReaderWriter()
        { }

        static LeadershipReaderWriter _instance;
        /// <summary>
        /// Singleton-instance.
        /// </summary>
        public static LeadershipReaderWriter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LeadershipReaderWriter();
                }
                
                return _instance;
            }
        }

        List<Leader> _LeadersList { get; set; }

        readonly string _fileName = "Data/Leadership.xml";

        /// <summary>
        /// Adds the new leader into list and writes changes.
        /// </summary>
        /// <param name="p_Leader"></param>
        public void AddNewLeader(Leader p_Leader)
        {
            if (_LeadersList == null)
            {
                _LeadersList = ReadLeaders();
            }
            _LeadersList.Add(p_Leader);
            _LeadersList = _LeadersList.OrderByDescending(l => l).ToList();
            _LeadersList = _LeadersList.Take(10).ToList();

            WriteChangesToXML();            
        }
        /// <summary>
        /// Checks if current gamer is a leader.
        /// </summary>
        /// <param name="p_UserScore"></param>
        /// <param name="p_EnemyScore"></param>
        /// <returns></returns>
        public bool IsNewLeader(int p_UserScore)
        {
            bool _result = false;
            Leader _possibleLeader = new Leader("", p_UserScore, DateTime.Now.ToString("d"));

            if (_LeadersList == null)
            {
                _LeadersList = ReadLeaders();
            }

            Leader _minimalLeader = _LeadersList.Min();

            if (_possibleLeader.CompareTo(_minimalLeader) > 0)
            {
                _result = true;
            }

            return _result;
        }

        public List<Leader> ReadLeaders()
        {
            ReadLeadersFromXML();
            
            return _LeadersList;
        }

        /// <summary>
        /// Reads the list of leader from file.
        /// </summary>
        private void ReadLeadersFromXML()
        { 
            List<Leader> _leadersList = new List<Leader>();

            XmlDocument myXml = new XmlDocument();

            try
            {
                myXml.Load(_fileName);
                XmlElement myRoot = myXml.DocumentElement;

                foreach (XmlNode leader in myRoot.ChildNodes)
                {
                    string _date = leader.Attributes["Date"].Value;
                    string _name = leader.Attributes["Name"].Value;
                    int _userScore = Convert.ToInt32(leader.Attributes["UserScore"].Value);
                    //int _enemyScore = Convert.ToInt32(leader.Attributes["EnemyScore"].Value);

                    Leader myresult = new Leader(_name, _userScore, _date);

                    _leadersList.Add(myresult);
                }
            }
            catch { }

            _LeadersList = _leadersList.OrderByDescending(l=>l).ToList();
        }        

        /// <summary>
        /// Writes changed list into file.
        /// </summary>
        private void WriteChangesToXML()
        {
            XmlDocument myXml = new XmlDocument();
            XmlElement myRoot;

            try
            {
                myXml.Load(_fileName);
                myRoot = myXml.DocumentElement;
            }
            catch (Exception)
            {
                myXml.CreateXmlDeclaration("1.0", "utf-8", "");
                myRoot = myXml.CreateElement(_fileName);
                myXml.AppendChild(myRoot);
            }

            myRoot.RemoveAll();

            foreach (var leader in _LeadersList)
            {
                XmlElement LeaderElem = myXml.CreateElement("Leader");

                XmlAttribute DateAttr = myXml.CreateAttribute("Date");
                XmlAttribute NameAttr = myXml.CreateAttribute("Name");
                XmlAttribute UserScoreAttr = myXml.CreateAttribute("UserScore");

                XmlText DateText = myXml.CreateTextNode(leader.DateOfWin);
                XmlText NameText = myXml.CreateTextNode(leader.Name);
                XmlText UserScoreText = myXml.CreateTextNode(leader.UserScores.ToString());

                DateAttr.AppendChild(DateText);
                NameAttr.AppendChild(NameText);
                UserScoreAttr.AppendChild(UserScoreText);

                LeaderElem.Attributes.Append(DateAttr);
                LeaderElem.Attributes.Append(NameAttr);
                LeaderElem.Attributes.Append(UserScoreAttr);

                myRoot.AppendChild(LeaderElem);
            }

            myXml.Save(_fileName);
        }
    }
}
