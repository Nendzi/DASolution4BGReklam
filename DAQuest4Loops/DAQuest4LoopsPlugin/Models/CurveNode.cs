using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAQuest4LoopsPlugin.Models
{
    public class CurveNode
    {
        public Point NodePoint { get; set; }
        public List<ICustomizedObject> Edges { get; set; } = new List<ICustomizedObject>();
    }
}
