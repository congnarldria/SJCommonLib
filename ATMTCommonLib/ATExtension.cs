using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ATMTCommonLib
{
	/// <summary>
	/// ATMT Extended functions
	/// </summary>
    public static class ATExtension
    {
		/// <summary>
		/// 深層複製的泛型方法
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item"></param>
		/// <returns></returns>
		public static T DeepClone<T>(this T item)
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
			LogMgr.SendLog("Clone Fail");
			return default;
		}
	}
}
