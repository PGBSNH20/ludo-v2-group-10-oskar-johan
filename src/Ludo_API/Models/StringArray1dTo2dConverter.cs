//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text.Json.Serialization;

//namespace Ludo_API.Models
//{
//    //public class StringArray1dTo2dConverter : JsonConverter
//    //{
//    //    public override bool CanConvert(Type objectType)
//    //    {
//    //        return (objectType == typeof(string[,]));
//    //    }

//    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
//    //    {
//    //        JArray array = JArray.Load(reader);
//    //        return new SubmissionDataRow
//    //        {
//    //            Reading1 = array[0].Value<int>(),
//    //            Reading2 = array[1].Value<int>()
//    //        };
//    //    }

//    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
//    //    {
//    //        throw new NotImplementedException();
//    //    }

//    //public class StringArray1dTo2dConverter : CustomCreationConverter<string[]>
//    //{
//    //    public override string[] Create(Type objectType)
//    //    {
//    //        //this.ReadJson()
//    //        //return new Employee();
//    //        return null;
//    //    }
//    //}
//    //public class StringArray1dTo2dConverter : CustomCreationConverter<string[]>
//    public class StringArray1dTo2dConverter : Newtonsoft.Json.JsonConverter
//    {
//        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
//        {
//            //    string[,] array2d = (string[,])value;
//            //string[,] array2d = new string[]
//            string[] _array1d = ((string[])value);
//            string[] array1d = new string[_array1d.Length];
//            List<List<string>> list2d = new List<List<string>>(); 

//            for (int i = 0; i < _array1d.Length; i++)
//            {
//                list2d.Add(_array1d[0].Split(",", StringSplitOptions.TrimEntries & StringSplitOptions.RemoveEmptyEntries).ToList());
//            }

//            writer.WriteValue(list2d);
//        }

//        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
//        {
//            if (reader.TokenType == JsonToken.Null)
//            {
//                return null;
//            }
//            else if (reader.TokenType == JsonToken.StartArray)
//            {
//                var values = new List<object>();
//                while (reader.TokenType != JsonToken.EndArray)
//                {
//                    values.Add(reader.Value);
//                    reader.Read();
//                }

//                var bool1 = reader.Read();

//                //string[] _array1d = ((string[])values);
//                var a = values.ToArray();
//                string[] _array1d = new string[a.Length];
//                for (int i = 0; i < a.Length; i++)
//                {
//                    _array1d[i] = (string)a[i];
//                }
//                string[] array1d = new string[_array1d.Length];
//                List<List<string>> list2d = new List<List<string>>();

//                for (int i = 0; i < _array1d.Length; i++)
//                {
//                    if (_array1d[i] == null)
//                    {
//                        continue;
//                    }

//                    list2d.Add(_array1d[i].Split(",", StringSplitOptions.TrimEntries & StringSplitOptions.RemoveEmptyEntries).ToList());
//                }

//                return list2d;
//                //return values.ToArray();
//            }
//            else
//            {
//                throw new Exception("not expected"); ;
//            }
//            //string[,] array1d = (string[,])reader.Value;
//            //user.UserName = (string)reader.Value;
//            //var a_0 = reader.TokenType;
//            //var a = (List<List<string>>)reader.Value;
//            //string[,] array1d = new string[a.Count, a.Count];

//            //for (int i = 0; i < a.Count; i++)
//            //{
//            //    for (int j = 0; j < a.Count; j++)
//            //    {
//            //        //var b = new ArrayList
//            //        array1d[i,j] = a[i][j];
//            //    }
//            //}
//            ////var b = aToArray<string[]>();
//            //return array1d;
//            //string[,] array1d = (string[,])reader.Value;
//            //return array1d;
//            //return null;
//        }

//        public override bool CanConvert(Type objectType)
//        {
//            return objectType == typeof(string[,]);
//        }
//    }
//}