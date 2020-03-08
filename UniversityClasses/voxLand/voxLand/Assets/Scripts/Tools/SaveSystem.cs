using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void ItemContainerSave(ItemContainerData container)
    {
        //Create the stream to add object into it.
        System.IO.Stream ms = File.OpenWrite(Application.persistentDataPath + container.name + ".save");
        //Format the object as Binary

        BinaryFormatter formatter = new BinaryFormatter();

        formatter.Serialize(ms, container);
        ms.Flush();
        ms.Close();
        ms.Dispose();
    }
    public static bool ItemContainerLoad(ItemContainerData container)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            FileStream fs = File.Open(Application.persistentDataPath + container.name + ".save", FileMode.Open);
            object obj = formatter.Deserialize(fs);
            container.items = ((ItemContainerData)obj).items;
            fs.Flush();
            fs.Close();
            fs.Dispose();
            return true;
        }
        catch (FileNotFoundException)
        {
            return false;
        }
        catch (SerializationException)
        {
            return false;
        }
    }
}
