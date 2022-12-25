using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace LABA_8
{
    [DataContract]
    public class SerializeProp
    {
        /// <summary>
        /// Сохранение игры
        /// </summary>
        [DataMember]
        public Dices dices { get; set; }
        [DataMember]
        public int timer { get; set; }
        [DataMember]
        GameManager gameManager;

        internal SerializeProp(Dices dices, int timer, GameManager gameManager)
        {
            this.dices = dices;
            this.timer = timer;
            this.gameManager = gameManager;
        }

        internal GameManager GetGameManager()
        {
            return gameManager;
        }
    }
}
