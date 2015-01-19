using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyIOS
{
	public static class Extensions
	{
		public static bool Is(this Type type, Type typeToBe)
		{
			if (!typeToBe.IsGenericTypeDefinition)
				return typeToBe.IsAssignableFrom(type);

			var toCheckTypes = new List<Type> { type };
			if (typeToBe.IsInterface)
				toCheckTypes.AddRange(type.GetInterfaces());

			var basedOn = type;
			while (basedOn.BaseType != null)
			{
				toCheckTypes.Add(basedOn.BaseType);
				basedOn = basedOn.BaseType;
			}
			bool isAs = toCheckTypes.Any (x => x.IsGenericType && x.GetGenericTypeDefinition () == typeToBe);
			return  isAs;
		}

		public static bool IsNint(this Type type, Type typeToBe)
		{
			if (!typeToBe.IsGenericTypeDefinition)
				return typeToBe.IsAssignableFrom(type);

			var toCheckTypes = new List<Type> { type };
			if (typeToBe.IsInterface)
				toCheckTypes.AddRange(type.GetInterfaces());

			var basedOn = type;
			while (basedOn.BaseType != null)
			{
				toCheckTypes.Add(basedOn.BaseType);
				basedOn = basedOn.BaseType;
			}
			bool isAs = toCheckTypes.Any (x => x.IsGenericType && x.GetGenericTypeDefinition () == typeToBe);
			return  isAs;
		}

		public static bool IsNfloat(this Type type, Type typeToBe)
		{
			if (!typeToBe.IsGenericTypeDefinition)
				return typeToBe.IsAssignableFrom(type);

			var toCheckTypes = new List<Type> { type };
			if (typeToBe.IsInterface)
				toCheckTypes.AddRange(type.GetInterfaces());

			var basedOn = type;
			while (basedOn.BaseType != null)
			{
				toCheckTypes.Add(basedOn.BaseType);
				basedOn = basedOn.BaseType;
			}
			bool isAs = toCheckTypes.Any (x => x.IsGenericType && x.GetGenericTypeDefinition () == typeToBe);
			return  isAs;
		}

	}
}

