using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace LABA_8.Game
{
    /// <summary>
    /// Обрабатывает все события на карте
    /// </summary>
    [DataContract]
    public class TriggersManager : GameStatistics
    {
        [DataMember]
        private int activeplayernumber;
        [DataMember]
        private int[] yellow_points = new int[]{
            6,
            48,
            64,
            84,
            89
        };
        [DataMember]
        private int[] green_points = new int[] {
            10,
            52,
            71,
            76
        };

        [DataMember]
        bool green_flag;
        [DataMember]
        private int skip_number = -5;

        [DataMember]
        Way blue_way_1 = new Way(
            new PointF[] {
            new PointF(396, 477),
            new PointF(428, 516),
            new PointF(438, 568)
            }, 27);

        [DataMember]
        Way blue_way_2 = new Way(
            new PointF[] {
            new PointF(381, 636),
            new PointF(404, 673),
            new PointF(471, 680)
            }, 43);

        [DataMember]
        Way blue_way_3 = new Way(
            new PointF[] {
            new PointF(860, 112),
            new PointF(921, 163),
            new PointF(963, 221)
            }, 67);

        [DataMember]
        Way blue_way_4 = new Way(
            new PointF[] {
            new PointF(820, 711),
            new PointF(898, 656),
            new PointF(964, 619)
            }, 101);

        [DataMember]
        Way red_way_1 = new Way(
            new PointF[] {
            new PointF(381, 289),
            new PointF(197, 365),
            new PointF(29, 419)
            }, 14);

        [DataMember]
        Way red_way_2 = new Way(
            new PointF[] {
            new PointF(253, 731),
            new PointF(317, 674),
            new PointF(329, 603)
            }, 29);

        [DataMember]
        Way red_way_3 = new Way(
            new PointF[] {
            new PointF(680, 325),
            new PointF(636, 249),
            new PointF(593, 191)
            }, 74);

        [DataMember]
        protected Way active_way;

        public int ActivePlayerNumber
        {
            get { return activeplayernumber; }
            set
            {
                activeplayernumber = value % _players.Count;
            }
        }

        public void TriggerCheking()
        {
            CheckingGreenAndYellowPoints();
        }

        public void CheckingGreenAndYellowPoints()
        {
            green_flag = false;

            for (int i = 0; i < green_points.Length; i++)
                if (_players[(activeplayernumber == -1) ? (0) : (activeplayernumber)].point_number == green_points[i])
                {
                    green_flag = true;
                    break;
                }

            for (int i = 0; i < yellow_points.Length; i++)
                if (_players[(activeplayernumber == -1) ? (0) : (activeplayernumber)].point_number == yellow_points[i])
                {
                    skip_number = ActivePlayerNumber;
                    break;
                }


            if (!green_flag)
                if ((ActivePlayerNumber + 1) % _players.Count == skip_number)
                {
                    ActivePlayerNumber += 2;
                    skip_number = -5;
                }
                else
                {
                    ActivePlayerNumber += 1;
                    while (players_finish.Contains(_players[ActivePlayerNumber]) && players_finish.Count != _players.Count)
                        ActivePlayerNumber += 1;
                }
            else
                green_flag = false;
        }

        public void CheckingBlueAndRedPoints()
        {
            switch (_players[(activeplayernumber == -1) ? (0) : (activeplayernumber)].point_number)
            {
                case 20: active_way = blue_way_1; break;
                case 28: active_way = blue_way_2; break;
                case 60: active_way = blue_way_3; break;
                case 97: active_way = blue_way_4; break;
                case 51: active_way = red_way_1; break;
                case 38: active_way = red_way_2; break;
                case 79: active_way = red_way_3; break;
            }
        }
    }

    [DataContract]
    public struct Way
    {
        [DataMember]
        private PointF[] points;
        [DataMember]
        private int finish_point_number;
        [DataMember]
        private int player_on_way_number;

        public Way(PointF[] new_points, int finish)
        {
            this.player_on_way_number = 0;
            this.points = new_points;
            this.finish_point_number = finish;
            this.Active = true;
        }

        public PointF[] GetPoints()
        {
            return points;
        }

        public int GetFinish()
        {
            return finish_point_number;
        }

        public int ActivePlayerPoint
        {
            get { return player_on_way_number; }
            set
            {
                player_on_way_number = value;
            }
        }

        public bool Active
        {
            get { return !(finish_point_number == 0); }
            set
            {
                if (value == false)
                    finish_point_number = 0;
            }
        }
    }
}
