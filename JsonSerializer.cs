using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace DirectoryManager
{
    class JsonSerializer : ISerializator<DirectoryData>
    {

        public string Serialize(DirectoryData directoryData)
        {
            if (directoryData == null)
            {
                throw new ArgumentNullException("No data or empty data provided!");
            }

            try
            {
                string serialized = System.Text.Json.JsonSerializer.Serialize(directoryData, new JsonSerializerOptions { WriteIndented = true });
                return serialized;
            }
            catch (Exception ex)
            {
                throw new Exception($"Serialization failed! Details: {ex.Message}");

            }



        }

        public DirectoryData Deserialize(string serializedData)
        {
            if (serializedData == null)
            {
                throw new ArgumentNullException("No data for deserialization!");
            }

            try
            {

                DirectoryData deserialized = System.Text.Json.JsonSerializer.Deserialize<DirectoryData>(serializedData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return deserialized;
            }

            catch (Exception ex)
            {
                throw new Exception($"Deserialization failed! Details: {ex.Message}");
            }

        }


    }
}
