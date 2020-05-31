using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cubeWPF
{
    class Json
    {
        public void Wirte(JsonFormat json, string jsonPath)
        {
            string jsonSring = JsonConvert.SerializeObject(json);

            File.WriteAllText(jsonPath, jsonSring);
        }

        public JsonFormat Read(string jsonPath)
        {
            var jsonContent = File.ReadAllText(jsonPath);
            JsonFormat deserializedProduct = JsonConvert.DeserializeObject<JsonFormat>(jsonContent);
            return deserializedProduct;
        }
    }
}
