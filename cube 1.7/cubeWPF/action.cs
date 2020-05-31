using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;


namespace cubeWPF
{
    class action
    {
        public void safeRotation(JsonFormat Variables, List<Cordinate> points, List<Lines>linesV, string jsonPath)
        {

            Variables = new JsonFormat
            {
                p = points,
                l = linesV
            };

            Json json = new Json();
            json.Wirte(Variables, jsonPath);
        }

        public List<Cordinate> reset(List<Cordinate> points, List<Lines> linesV, List<Line> linesR, float centerY, float centerX)
        {

            Real real = new Real();
            points = new List<Cordinate>
            {
                new Cordinate {x = -100, y = -100, z = -100},
                new Cordinate {x = -100, y = 100, z = -100},
                new Cordinate {x = 100, y = 100, z = -100},
                new Cordinate {x = 100, y = -100, z = -100},
                new Cordinate {x = 100, y = 100, z = 100},
                new Cordinate {x = -100, y = 100, z = 100},
                new Cordinate {x = -100, y = -100, z = 100},
                new Cordinate {x = 100, y = -100, z = 100},

            };
            real.RotatingOnX(points, linesR, linesV, centerX, centerY, 0);
            return points;
        }
    }
}
