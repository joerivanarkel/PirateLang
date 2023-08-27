using System.Runtime.Serialization;
using Pirate.Common.FileHandlers;
using Newtonsoft.Json;
using Pirate.Common.Enum;
using Pirate.Common.FileHandlers.Interfaces;
using Pirate.Common.Interfaces;

namespace Pirate.Common;

/// <summary>
/// This class is used to serialize and deserialize objects to and from JSON.
/// </summary>
public class ObjectSerializer : IObjectSerializer
{
    public string Location { get; set; }
    public ILogger Logger { get; set; }

    private IFileWriteHandler _fileWriteHandler;
    private IFileReadHandler _fileReadHandler;

    public ObjectSerializer(ILogger logger, IEnvironmentVariables environmentVariables, IFileWriteHandler fileWriteHandler, IFileReadHandler fileReadHandler)
    {
        _fileReadHandler = fileReadHandler;
        _fileWriteHandler = fileWriteHandler;

        Location = environmentVariables.GetVariable("location") + "/cache";
        bool exists = Directory.Exists(Location);
        if (!exists)
            Directory.CreateDirectory(Location);
        Logger = logger;
    }

    public void SerializeObject(object ObjectToSerialize, string FileName)
    {
        try
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                // TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple, 
                Formatting = Formatting.Indented
            };
            string json = JsonConvert.SerializeObject(ObjectToSerialize, settings);
            _fileWriteHandler.WriteToFile(new FileWriteModel(FileName, FileExtension.JSON, Location, json));

            Logger.Log($"Serialized and written \"{FileName}\" to \"{FileName}\".json", LogType.INFO);
        }
        catch (Exception ex)
        {
            Logger.Log($"Failed to Serialize {FileName}.json. \"{ex.ToString()}\"", LogType.ERROR);
            throw;
        }
    }

    public T Deserialize<T>(string FileName) where T : class
    {
        try
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                // TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple, 
                Formatting = Formatting.Indented

            };
            string json = _fileReadHandler.ReadAllTextFromFile(FileName, FileExtension.JSON, Location).Result;
            T deserializedObject = JsonConvert.DeserializeObject<T>(json, settings);

            if (deserializedObject == null) throw new SerializationException("Deserialized object is null");
            Logger.Log($"Deserialized and converted {FileName} to {FileName}.json", LogType.INFO);

            return deserializedObject;
        }
        catch (SerializationException ex)
        {
            Logger.Log($"Failed to Deserialize {FileName}.json. \"{ex.ToString() + "\n" + ex.Source}\"", LogType.ERROR);
            throw new SerializationException(ex.ToString() + "\n" + ex.Source);
        }
    }
}