using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.Common
{
	public class ConstantExtension
	{
		public static string GetDisPlayConstant<T>(string propertyName)
		{
			Type type = typeof(T);
			PropertyInfo pro = type.GetProperty(propertyName);
			if (pro == null)
			{
				return propertyName;
			}
			else
			{
				DisplayNameAttribute dpname = pro.GetCustomAttribute<DisplayNameAttribute>();
				return dpname?.DisplayName ?? pro.Name;
			}

		}
	}
}
