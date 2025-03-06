using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace DirectoryManager
{

    class FileHandler
    {

        //DI, DIP a ISP pouzite pre lahsie testovanie a dodrzanie best practices 
        private readonly ISerializator<DirectoryData> _serializator;

        public FileHandler(ISerializator<DirectoryData> serializator)
        {
            _serializator = serializator ?? throw new ArgumentNullException($"{nameof(serializator)} can't be null!");
        }


        //Serializacia a nasledne ulozenie do suboru, ktory bol poskytnuty ako parameter
        public void SaveToFile(string path, DirectoryData directoryData)
        {
            if (string.IsNullOrEmpty(path) || directoryData == null)
            {
                throw new ArgumentNullException("Saving to file failed! Parameter can't be null!");
            }

            try
            {
                string serializedData = _serializator.Serialize(directoryData);
                File.WriteAllText(path, serializedData);
                Console.WriteLine($"File successfully saved to {path}");


            }
            catch (IOException ex)
            {
                throw new IOException($"Writing to the {path} failed. Details: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Saving to the file failed! Details: {ex.Message}");

            }


        }

        //Nacitanie suboru a nasledna deserializacia
        public DirectoryData LoadFromFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("Path cannot be null");
            }
            try
            {
                string fileRead = File.ReadAllText(path);
                DirectoryData deserializedData = _serializator.Deserialize(fileRead);
                return deserializedData;

            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException(ex.Message);
            }

            catch (IOException ex)
            {
                throw new IOException($"Loading data from the {path} failed! Details: {ex.Message}");
            }

            catch (Exception ex)
            {
                throw new Exception($"Loading from file failed. Details: {ex.Message}");
            }




        }
    }
}
