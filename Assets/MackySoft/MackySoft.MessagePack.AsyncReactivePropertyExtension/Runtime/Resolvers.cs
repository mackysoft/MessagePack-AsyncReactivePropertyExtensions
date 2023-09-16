using System;
using System.Collections.Generic;
using System.Reflection;
using Cysharp.Threading.Tasks;
using MessagePack.Formatters;

namespace MessagePack.AsyncReactivePropertyExtension
{
	public sealed class AsyncReactivePropertyResolver : IFormatterResolver
	{
		public static readonly AsyncReactivePropertyResolver Instance = new AsyncReactivePropertyResolver();

		AsyncReactivePropertyResolver () { }

		public IMessagePackFormatter<T> GetFormatter<T> ()
		{
			return FormatterCache<T>.Formatter;
		}

		internal static class FormatterCache<T>
		{
			public static readonly IMessagePackFormatter<T> Formatter;

			static FormatterCache ()
			{
				Formatter = (IMessagePackFormatter<T>)AsyncReactivePropertyResolverGetFormatterHelper.GetFormatter(typeof(T));
			}
		}
	}

	internal static class AsyncReactivePropertyResolverGetFormatterHelper
	{

		static readonly Dictionary<Type, Type> s_FormatterMap = new Dictionary<Type, Type>()
		{
			{typeof(AsyncReactiveProperty<>), typeof(AsyncReactivePropertyFormatter<>) },
			{typeof(IAsyncReactiveProperty<>), typeof(InterfaceAsyncReactivePropertyFormatter<>) },
			{typeof(IReadOnlyAsyncReactiveProperty<>), typeof(ReadOnlyInterfaceAsyncReactivePropertyFormatter<>) },
		};

		public static object GetFormatter (Type t)
		{
			TypeInfo typeInfo = t.GetTypeInfo();
			if (typeInfo.IsGenericType)
			{
				Type genericType = typeInfo.GetGenericTypeDefinition();
				Type formatterType;
				if (s_FormatterMap.TryGetValue(genericType, out formatterType))
				{
					return Activator.CreateInstance(formatterType.MakeGenericType(typeInfo.GenericTypeArguments));
				}
			}
			return null;
		}
	}
}
