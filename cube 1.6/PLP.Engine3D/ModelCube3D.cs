using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLP.Engine3D
{

    // a class on what the user can do in the window 
    public class ModelCube3D
    {
        public ModelCube3D()
        {
        }

        public List<Point3D> Points { get; set; } = new List<Point3D>();

        public List<LineConection> LineConnections { get; set; } = new List<LineConection>();

        public void Save(string path)
        {
            var json = JsonConvert.SerializeObject(this);
            File.WriteAllText(path, json);
        }

        public static ModelCube3D Load(string path)
        {
            var model = new ModelCube3D();
            model.Reset();

            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                var jsonContent = File.ReadAllText(path);

                if (!string.IsNullOrEmpty(jsonContent))
                {
                    model = JsonConvert.DeserializeObject<ModelCube3D>(jsonContent);
                }
            }

            return model;
        }

        public void Reset()
        {
            this.Points.Clear();
            this.Points.AddRange(new List<Point3D>
            {
                new Point3D {X = -100, Y = -100, Z = -100},
                new Point3D {X = -100, Y = 100, Z = -100},
                new Point3D {X = 100, Y = 100, Z = -100},
                new Point3D {X = 100, Y = -100, Z = -100},
                new Point3D {X = 100, Y = 100, Z = 100},
                new Point3D {X = -100, Y = 100, Z = 100},
                new Point3D {X = -100, Y = -100, Z = 100},
                new Point3D {X = 100, Y = -100, Z = 100},
            });

            this.LineConnections.Clear();
            this.LineConnections.AddRange(new []
            {
                new LineConection {Id1 = 0, Id2 =1 },
                new LineConection {Id1 = 1, Id2 =2 },
                new LineConection {Id1 = 2, Id2 =3 },
                new LineConection {Id1 = 3, Id2 =0 },
                new LineConection {Id1 = 2, Id2 =4 },
                new LineConection {Id1 = 1, Id2 =5 },
                new LineConection {Id1 = 0, Id2 =6 },
                new LineConection {Id1 = 3, Id2 =7 },
                new LineConection {Id1 = 4, Id2 =5 },
                new LineConection {Id1 = 6, Id2 =7 },
                new LineConection {Id1 = 7, Id2 =4 },
                new LineConection {Id1 = 6, Id2 = 5}
            });
    }
    }
}
