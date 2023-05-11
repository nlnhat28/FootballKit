using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FootballKit1
{
    public class PensController
    {
        public static fPenalties fPenalties { get; set; }
        public static int currentIdPenFrame { get; set; } = 0;
        public static List<PenFrame> listPenFrame { get; set; } = new List<PenFrame>(5)
                  { new PenFrame(){idHeader = 0 },
                    new PenFrame(){idHeader = 1 },
                    new PenFrame(){idHeader = 2 },
                    new PenFrame(){idHeader = 3 },
                    new PenFrame(){idHeader = 4 }
                  };

        public static int tiSoLuanLuuA { get; set; }
        public static int tiSoLuanLuuB { get; set; }

        public static void resetListPenFrame()
        {
            listPenFrame  = new List<PenFrame>(5)
                  { new PenFrame(){idHeader = 0 },
                    new PenFrame(){idHeader = 1 },
                    new PenFrame(){idHeader = 2 },
                    new PenFrame(){idHeader = 3 },
                    new PenFrame(){idHeader = 4 }
                  };
        }
    }
}
