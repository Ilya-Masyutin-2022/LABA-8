using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace LABA_8.Game
{
    [DataContract]
    public class GameManager : TriggersManager
    {
        /// <summary>
        /// Сохранение логики игры
        /// </summary>
        [DataMember]
        PointF[] points;
        [DataMember]
        PointF[] next_point;

        [DataMember]
        private int move_steps;

        [DataMember]
        private int firstmoveflag = 0;

        public GameManager(PointF[] map_points, int player_count)
        {
            points = map_points;

            next_point = new PointF[4] {
                map_points[0],
                map_points[0],
                map_points[0],
                map_points[0],
            };

            Player[] players = new Player[4] {
                new Player(1, "Player1", new Size(50, 70), points[0]),
                new Player(2, "Player2", new Size(50, 70), points[0]),
                new Player(3, "Player3", new Size(50, 70), points[0]),
                new Player(4, "Player4", new Size(50, 70), points[0])
            };


            _players = new List<Player>(player_count);
            players_finish = new List<Player>(0);

            for (int i = 0; i < player_count; i++)
                _players.Add(players[i]);

            move_steps = 0;
            ActivePlayerNumber = -1;

            active_way = new Way(new PointF[0], 0);
            active_way.Active = false;
        }

        public void Draw(Graphics graphics)
        {
            float[] Y_Position = new float[_players.Count];
            int[] Number = new int[_players.Count];
            for (int i = 0; i < _players.Count; i++)
            {
                Y_Position[i] = _players[i].Position.Y;
                Number[i] = i;
            }
            Array.Sort(Y_Position, Number);
            for (int i = 0; i < _players.Count; i++)
                _players[Number[i]].DrawSprite(graphics);
        }

        public void GameTic()
        {
            var number = ActivePlayerNumber;

            if (players_finish.Count == _players.Count)
                EndGame();

            if (!active_way.Active)
                if (move_steps > 0)
                    if (_players[number].Position != next_point[number])
                    {
                        _players[number].MoveToward(next_point[number], Properties.Settings.Default.MoveSpeed);
                    }
                    else
                    {
                        if (_players[number].point_number == points.Length - 1)
                        {
                            players_finish.Add(_players[number]);
                            TriggerCheking();
                            move_steps = 0;

                            if (players_finish.Count == _players.Count)
                                EndGame();

                            return;
                        }

                        next_point[number] = points[++_players[number].point_number];

                        if (firstmoveflag == number && firstmoveflag <= _players.Count)
                        {
                            move_steps++;
                            firstmoveflag++;
                        }
                        move_steps--;
                    }
                else
                    CheckingBlueAndRedPoints();
            else
            {
                var way_point = active_way.GetPoints();
                var nextWayPoint = way_point[active_way.ActivePlayerPoint];

                if (_players[number].Position != nextWayPoint)
                {
                    _players[number].MoveToward(nextWayPoint, Properties.Settings.Default.MoveSpeed);
                }
                else
                {
                    if (_players[number].Position != way_point[way_point.Length - 1])
                        active_way.ActivePlayerPoint++;
                    else
                    {
                        _players[number].point_number = active_way.GetFinish();

                        next_point[number] = points[active_way.GetFinish()];

                        active_way.Active = false;
                    }
                }
            }

            CheckPositions();
        }

        public int Move_steps
        {
            get { return move_steps; }
            set { move_steps = value; }
        }

        private void CheckPositions()
        {
            for (int i = 0; i < _players.Count; i++)
                for (int j = 0; j < _players.Count; j++)
                {
                    if ((_players[i].Name != _players[j].Name) && (_players[i].point_number == _players[j].point_number))
                        _players[i].Shift = _players[j].Shift = true;
                    else
                        _players[i].Shift = _players[j].Shift = false;
                }
        }

        private void EndGame()
        {
            string[] names = new string[players_finish.Count];

            for (int i = 0; i < names.Length; i++)
                names[i] = players_finish[i].Name;

            players_finish.Clear();

            EndGameForm endGameForm = new EndGameForm(names);
            endGameForm.ShowDialog();
        }
    }
}
