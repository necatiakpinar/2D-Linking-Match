using System;
using Interfaces;
using UnityEngine;

namespace Helpers
{
    public class JsonHelper : IJsonHelper
    {
        public string ToJson(object obj, bool prettyPrint)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "Object cannot be null.");

            return prettyPrint ? JsonUtility.ToJson(obj, true) : JsonUtility.ToJson(obj);
        }
        public object FromJson(string json, Type type)
        {
            if (string.IsNullOrEmpty(json))
                throw new ArgumentException("JSON string cannot be null or empty.", nameof(json));

            if (type == null)
                throw new ArgumentNullException(nameof(type), "Type cannot be null.");

            return JsonUtility.FromJson(json, type);
        }
    }
}