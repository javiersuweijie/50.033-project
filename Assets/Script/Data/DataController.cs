using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataController : MonoBehaviour {

	public static DataController dataController;

	public float param1;
	public float param2;

	void Awake () {
		if (dataController == null){
			DontDestroyOnLoad(gameObject);
			dataController = this;
		}
		else if (dataController != this){
			Destroy(gameObject);
		}
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/saveinfo.dat");

		PlayerData data = new PlayerData();
		data.saveParam1 = param1;
		data.saveParam2 = param2;

		bf.Serialize(file, data);
		file.Close();
	}
	
	public void Load(){
		if (File.Exists(Application.persistentDataPath + "/saveinfo.dat")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.OpenRead(Application.persistentDataPath + "/saveinfo.dat");
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();
			param1 = data.saveParam1;
			param2 = data.saveParam2;
		}
	}
}


[Serializable]
class PlayerData
{
	public float saveParam1;
	public float saveParam2;
}
