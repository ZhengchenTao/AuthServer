﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AuthServer.Common.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        /// 转成dictionary类型
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static Dictionary<int, string> EnumToDictionary(this Type enumType)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            Type typeDescription = typeof(DescriptionAttribute);
            FieldInfo[] fields = enumType.GetFields();
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    int sValue = ((int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null));
                    object[] arr = field.GetCustomAttributes(typeDescription, true);
                    string sText;
                    if (arr.Length > 0)
                    {
                        DescriptionAttribute da = (DescriptionAttribute)arr[0];
                        sText = da.Description;
                    }
                    else
                    {
                        sText = field.Name;
                    }
                    dictionary.Add(sValue, sText);
                }
            }
            return dictionary;
        }

        /// <summary>
        /// 枚举成员转成键值对Json字符串
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static string EnumToDictionaryString(this Type enumType)
        {
            List<KeyValuePair<int, string>> dictionaryList = EnumToDictionary(enumType).ToList();
            var sJson = JsonConvert.SerializeObject(dictionaryList);
            return sJson;
        }

        /// <summary>
        /// 获取枚举值对应的描述
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumType)
        {
            FieldInfo EnumInfo = enumType.GetType().GetField(enumType.ToString());
            if (EnumInfo != null)
            {
                DescriptionAttribute[] EnumAttributes = (DescriptionAttribute[])EnumInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (EnumAttributes.Length > 0)
                {
                    return EnumAttributes[0].Description;
                }
            }
            return enumType.ToString();
        }

        /// <summary>
        /// 根据值获取枚举的描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetDescriptionByEnum<T>(this object obj)
        {
            var tEnum = Enum.Parse(typeof(T), obj.ToString()) as Enum;
            var description = tEnum.GetDescription();
            return description;
        }
    }
}
