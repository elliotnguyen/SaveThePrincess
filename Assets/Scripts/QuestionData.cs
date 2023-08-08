using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using UnityEngine;


public class QuestionData : ScriptableObject
{
    public string questionText;
    public string[] answerOptions;
    public int correctAnswerIndex;
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


