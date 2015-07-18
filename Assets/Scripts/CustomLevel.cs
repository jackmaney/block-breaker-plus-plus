using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;

[Serializable]
public class CustomLevel : Level {
    
    public string FileName;
    
    public CustomLevel(){}
    
    public CustomLevel(string name, 
                       Dictionary<int, int[]> brickRows, 
                       string fileName, int backgroundIndex) :
                            base(name, brickRows, backgroundIndex)
    {
        this.FileName = fileName;
    }
    
    protected new void Validate(){
        base.Validate();
        
        #if UNITY_WEBPLAYER
        
        #else
        if(!FileName.EndsWith(".bbpp-level")){
            throw new ArgumentException(
                "Wrong file extension.");
        }
        
        if(!File.Exists(FullFileName())){
            throw new FileNotFoundException(FullFileName());
        }
        #endif
    }
    
    private string FullFileName(){
        
        return Application.persistentDataPath + "/" + FileName;
    
	}
    
    public void LoadFromFile(string fileName){
        string fullFile = Application.persistentDataPath + 
            "/" + fileName;
        if(File.Exists(fullFile)){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(fullFile, FileMode.Open);
            CustomLevel cl = (CustomLevel)bf.Deserialize(fs);
            fs.Close();
            
            this.Name = cl.Name;
            this.BrickRows = cl.BrickRows;
            this.BackgroundIndex = cl.BackgroundIndex;
            this.FileName = cl.FileName;
            
            //cl = null;
        }
    }
    
    public void Save(){
        
        #if UNITY_WEBPLAYER
        Debug.Log(
            "Loading or saving levels is not supported on the web player.");
        #else
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(FullFileName());
        
        CustomLevel clone = new CustomLevel(Name,
            BrickRows, FileName, BackgroundIndex);
        
        bf.Serialize(file, clone);
        file.Close();
        #endif
    }
    
    public static CustomLevel[] AllCustomLevels(){
        
        CustomLevel[] result = null;
        #if UNITY_WEBPLAYER
        Debug.Log(
            "Loading or saving levels is not supported on the web player.");
        #else
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] info = dir.GetFiles("*.bbpp-level");
        
        result = new CustomLevel[info.Length];
        for(int i=0; i<info.Length; i++){
            CustomLevel cl = new CustomLevel();
            cl.LoadFromFile(info[i].Name);
            result[i] = cl;
        }
        #endif
        return result;
    }
}
