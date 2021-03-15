using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace AtomicCore
{
    /// <summary>
    /// 缓存区拓展类
    /// </summary>
    /// <remarks>seven_880107</remarks>
    public static partial class BufferExtensions
    {
        /// <summary>
        /// 将一个object对象序列化，返回一个byte[]
        /// </summary>
        /// <param name="obj">能序列化的对象</param>
        /// <returns></returns>
        public static byte[] ToBytes(this object obj)
        {
            byte[] buffer = null;
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                buffer = ms.GetBuffer();
            }

            return buffer;
        }

        /// <summary>
        /// 将一个序列化后的byte[]数组还原 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="buffer">字节流</param>
        /// <returns></returns>
        public static T ToObject<T>(this byte[] buffer)
        {
            object obj = null;
            using (MemoryStream ms = new MemoryStream(buffer))
            {
                IFormatter formatter = new BinaryFormatter();
                obj = formatter.Deserialize(ms);
            }

            Type conversionType = typeof(T);
            if (conversionType.IsAssignableFrom(obj.GetType()))
                return (T)Convert.ChangeType(obj, conversionType);
            else
                throw new ArgumentException(string.Format("Unable to successfully convert to {0}", conversionType.FullName));
        }

        /// <summary>
        /// 将byte数组转换成内存流
        /// </summary>
        /// <param name="buffer">缓存数组</param>
        /// <param name="offSet">设置内存流起始偏移量,默认为0</param>
        /// <returns></returns>
        public static Stream ToStream(this byte[] buffer, int offSet = 0)
        {
            if (null == buffer || 0 >= buffer.Length)
                throw new ArgumentException("buffer");
            MemoryStream ms = new MemoryStream(buffer);
            ms.Seek(offSet, SeekOrigin.Begin);

            return ms;
        }
    }
}
