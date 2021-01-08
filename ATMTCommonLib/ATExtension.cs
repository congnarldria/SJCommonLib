using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ATMTCommonLib
{
	/// <summary>
	/// ATMT Extended functions
	/// </summary>
    public static class ATExtension
    {
		/// <summary>
		/// Binary深層複製的泛型方法
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item"></param>
		/// <returns></returns>
		public static T DeepClone<T>(this T item)
		{
			try
			{
				if (item != null)
				{
					using (var stream = new MemoryStream())
					{
						var formatter = new BinaryFormatter();
						formatter.Serialize(stream, item);
						stream.Seek(0, SeekOrigin.Begin);
						var result = (T)formatter.Deserialize(stream);
						return result;
					}
				}
			}
			catch(Exception e)
            {
				LogMgr.SendLog("Clone Fail = " + e.Message , e);
			}
			return default;
		}
		/// <summary>
		/// Xml 深層複製的泛型方法
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item"></param>
		/// <returns></returns>
		public static T DeepCloneXml<T>(this T item)
		{
			try
			{
				string FileName = Environment.CurrentDirectory + "//temp.cln";
				XmlSerializer ser = new XmlSerializer(typeof(T));
				using (TextWriter writer = new StreamWriter(FileName))
				{
					ser.Serialize(writer, item);
				}
				using (FileStream myFileStream = new FileStream(FileName, FileMode.Open))
				{
					T cloneitem = (T)ser.Deserialize(myFileStream);
					return cloneitem;
				}
				
			}
			catch (Exception e)
			{
				LogMgr.SendLog("XmlClone Fail = " + e.Message , e);
			}
			return default;
		}
	}
}
