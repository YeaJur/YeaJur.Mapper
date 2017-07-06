#region YeaJur.Mapper 4.0.30319.42000

/***
 *
 *	本代码版权归  侯兴鼎（YeaJur） 所有，All Rights Reserved (C) 2017
 * 	CLR版本：4.0.30319.42000
 *	唯一标识：bad1141d-010d-4ff3-bf78-62f9da84da89
 **
 *	所属域：DESKTOP-Q9MAAK4
 *	机器名称：DESKTOP-Q9MAAK4
 *	登录用户：houxi
 *	创建时间：2017/1/17 20:47:32
 *	作者：侯兴鼎（YeaJur）
 *	E_mail：houxingding@hotmail.com
 **
 *	命名空间：YeaJur.Mapper
 *	类名称：Mapper
 *	文件名：Mapper
 *	文件描述：
 *
 ***/

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace YeaJur.Mapper
{
    /// <summary>
    /// 对象映射和json实体转换
    /// </summary>
    public static class Mapper
    {

        #region  ToJson ToModel

        /// <summary>
        /// 对象转换成json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonObject">需要格式化的对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJson<T>(this T jsonObject)
        {
            var json = new JavaScriptSerializer();
            return json.Serialize(jsonObject);
        }

        /// <summary>
        /// 把对象转成json字符串
        /// </summary>
        /// <param name="obj">可序列化对象</param>
        /// <returns>json字符串</returns>
        public static string ToJson(this object obj)
        {
            var json = new JavaScriptSerializer();
            return json.Serialize(obj);
        }

        /// <summary>
        /// 对象转换成json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonObject">需要格式化的对象</param>
        /// <param name="dateFormat">存在时间类型,设置时间格式</param>
        /// <returns>Json字符串</returns>
        public static string ToJson<T>(this T jsonObject, string dateFormat)
        {
            var js = new JavaScriptSerializer();
            var json = js.Serialize(jsonObject);

            if (!string.IsNullOrEmpty(dateFormat))
            {
                //大于1970年的时间更换
                json = Regex.Replace(json, @"\\/Date\((\d+)\)\\/", match =>
                {
                    var dt = new DateTime(1970, 1, 1);
                    dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                    dt = dt.ToLocalTime();
                    return dt.ToString(dateFormat);
                });
                //小于1970年的时间更换
                json = Regex.Replace(json, @"\\/Date\(-(\d+)\)\\/", match =>
                {
                    var dt = new DateTime(1970, 1, 1);
                    dt = dt.AddMilliseconds(-long.Parse(match.Groups[1].Value));
                    dt = dt.ToLocalTime();
                    return dt.ToString(dateFormat);
                });
            }

            return json;
        }

        /// <summary>
        /// 将json字符串转换成对象
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="json">json字符串</param> 
        public static T ToModel<T>(this string json)
        {
            var data = new JavaScriptSerializer();
            return data.Deserialize<T>(json);
        }

        #endregion

        #region Map

        /// <summary>
        /// 同类型实体深度映射拷贝
        /// </summary>
        /// <param name="obj">可序列化对象</param>
        /// <returns>实体</returns>
        public static T Map<T>(this T obj)
        {
            return obj.ToJson().ToModel<T>();
        }

        /// <summary>
        /// 不同类型实体深度映射拷贝
        /// </summary>
        /// <typeparam name="T">被拷贝对象类型</typeparam>
        /// <typeparam name="T2">接收对象类型</typeparam>
        /// <param name="obj">可序列化对象</param>
        /// <returns>接收对象</returns>
        public static T2 Map<T, T2>(this T obj)
        {
            return obj.ToJson().ToModel<T2>();
        }

        /// <summary>
        /// 不同类型实体深度映射拷贝
        /// </summary>
        /// <typeparam name="T2">接收对象类型</typeparam>
        /// <param name="obj">可序列化对象</param>
        /// <returns>接收对象</returns>
        public static T2 Map<T2>(this object obj)
        {
            return obj.ToJson().ToModel<T2>();
        }

        /// <summary>
        /// 不同类型实体深度映射拷贝
        /// </summary>
        /// <typeparam name="T">被拷贝对象类型</typeparam>
        /// <typeparam name="T2">接收对象类型</typeparam>
        /// <param name="obj">可序列化对象</param>
        /// <param name="fieldsDictionary">字段名不同的字段字典集合，key：当前对象字段名，value：拷贝后的对象的字段名称</param>
        /// <returns>实体</returns>
        public static T2 Map<T, T2>(this T obj, Dictionary<string, string> fieldsDictionary)
        {
            var json = obj.ToJson();
            if (fieldsDictionary != null && fieldsDictionary.Count > 0)
            {
                json = fieldsDictionary.Aggregate(json, (current, field) => current.Replace(field.Key, field.Value));
            }

            return json.ToModel<T2>();
        }

        /// <summary>
        /// 不同类型实体深度映射拷贝
        /// </summary>
        /// <typeparam name="T2">接收对象类型</typeparam>
        /// <param name="obj">可序列化对象</param>
        /// <param name="fieldsDictionary">字段名不同的字段字典集合，key：当前对象字段名，value：拷贝后的对象的字段名称</param>
        /// <returns>实体</returns>
        public static T2 Map<T2>(this object obj, Dictionary<string, string> fieldsDictionary)
        {
            var json = obj.ToJson();
            if (fieldsDictionary != null && fieldsDictionary.Count > 0)
            {
                json = fieldsDictionary.Aggregate(json, (current, field) => current.Replace(field.Key, field.Value));
            }

            return json.ToModel<T2>();
        }

        #endregion

    }
}