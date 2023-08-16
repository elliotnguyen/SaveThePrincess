using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
//using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;


public abstract class Question : ScriptableObject
{
    public string QuestionText;

    public string GetQuestionText()
    {
        return QuestionText;
    }

    public abstract string[] GetAnswerOptions();

    public abstract string GetCorrectAnswer();
    
}

public class FillInQuestion : Question
{
    public string correctAnswer;

    public override string[] GetAnswerOptions()
    {
        return null;
    }

    public override string GetCorrectAnswer()
    {
        return correctAnswer;
    }
}

public class MCQuestion : Question
{
    public string[] answerOptions;
    public int correctAnswerIndex;
    public override string GetCorrectAnswer()
    {
         return answerOptions[correctAnswerIndex];
    }
    public override string[] GetAnswerOptions()
    {
        return answerOptions;
    } 
}

//Solve the warning: "Must be instantiated using the ScriptableObject.CreateInstance method instead of new SO"
public class SOConverter<T> : CustomCreationConverter<T> where T : ScriptableObject
{
    public override T Create(Type aObjectType)
    {
        if (typeof(T).IsAssignableFrom(aObjectType))
            return (T)ScriptableObject.CreateInstance(aObjectType);
        return null;
    }
}

public class QuestionConverter : JsonCreationConverter<Question>
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    protected override Question Create(Type objectType, JObject jObject)
    {
        if (FieldExists("correctAnswer", jObject))
        {
            if (typeof(Question).IsAssignableFrom(objectType))
                return (FillInQuestion)ScriptableObject.CreateInstance(typeof(FillInQuestion));
        }
        else if (FieldExists("correctAnswerIndex", jObject))
        {
            if (typeof(Question).IsAssignableFrom(objectType))
                return (MCQuestion)ScriptableObject.CreateInstance(typeof(MCQuestion));
        }

        return null;
    }

    private bool FieldExists(string fieldName, JObject jObject)
    {
        return jObject[fieldName] != null;
    }
}


public abstract class JsonCreationConverter<T> : JsonConverter
{
    /// <summary>
    /// Create an instance of objectType, based properties in the JSON object
    /// </summary>
    /// <param name="objectType">type of object expected</param>
    /// <param name="jObject">
    /// contents of JSON object that will be deserialized
    /// </param>
    /// <returns></returns>
    protected abstract T Create(Type objectType, JObject jObject);

    public override bool CanConvert(Type objectType)
    {
        return typeof(T).IsAssignableFrom(objectType);
    }

    public override bool CanWrite
    {
        get { return false; }
    }

    public override object ReadJson(JsonReader reader,
                                    Type objectType,
                                     object existingValue,
                                     JsonSerializer serializer)
    {
        // Load JObject from stream
        JObject jObject = JObject.Load(reader);

        // Create target object based on JObject
        T target = Create(objectType, jObject);

        // Populate the object properties
        serializer.Populate(jObject.CreateReader(), target);

        return target;
    }
}


