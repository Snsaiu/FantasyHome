﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Gardener.Common.Extensions
{
    public static class EnumExtension
    {
        /// <summary>
        /// 枚举转list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> EnumToList<T>()
        {
            List<T> list = new List<T>();
            if (!typeof(T).IsEnum)
            {
                return list;
            }
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                list.Add((T)item);
            }
            return list;
        }
        /// <summary>
        /// 枚举转字典
        /// key 枚举 value 描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Dictionary<T, string> EnumToDictionary<T>()
        {
            Dictionary<T, string> dic = new Dictionary<T, string>();
            if (!typeof(T).IsEnum)
            {
                return dic;
            }
            string desc = string.Empty;
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                //需要忽略的
                var igAttrs = item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(IgnoreConvertAttribute), true);
                if (igAttrs != null && igAttrs.Length > 0) continue;
                //枚举的Description
                var attrs = item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    DescriptionAttribute descAttr = attrs[0] as DescriptionAttribute;
                    desc = descAttr.Description;
                }
                dic.Add((T)item, desc);
            }
            return dic;
        }
        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum item)
        {
            var attrs = item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attrs != null && attrs.Length > 0)
            {
                DescriptionAttribute descAttr = attrs[0] as DescriptionAttribute;
                return descAttr.Description;
            }
            return string.Empty;
        }
    }
}
