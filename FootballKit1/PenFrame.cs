using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballKit1
{
    public class PenFrame
    {
        public int idHeader { get; set; }
        public List<int> listPenStateA { get; set; } = new List<int>(Default.listPenState);
        public List<int> listPenStateB { get; set; } = new List<int>(Default.listPenState);
    }
}
