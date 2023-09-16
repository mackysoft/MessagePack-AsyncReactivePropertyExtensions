using Cysharp.Threading.Tasks;
using MessagePack.Formatters;

namespace MessagePack.AsyncReactivePropertyExtension
{

	public sealed class AsyncReactivePropertyFormatter<T> : IMessagePackFormatter<AsyncReactiveProperty<T>>
	{
		public void Serialize (ref MessagePackWriter writer, AsyncReactiveProperty<T> value, MessagePackSerializerOptions options)
		{
			if (value == null)
			{
				writer.WriteNil();
				return;
			}

			options.Resolver.GetFormatterWithVerify<T>().Serialize(ref writer, value.Value, options);
		}

		public AsyncReactiveProperty<T> Deserialize (ref MessagePackReader reader, MessagePackSerializerOptions options)
		{
			if (reader.TryReadNil())
			{
				return null;
			}

			var value = options.Resolver.GetFormatterWithVerify<T>().Deserialize(ref reader, options);
			return new AsyncReactiveProperty<T>(value);
		}
	}

	public sealed class InterfaceAsyncReactivePropertyFormatter<T> : IMessagePackFormatter<IAsyncReactiveProperty<T>>
	{
		public void Serialize (ref MessagePackWriter writer, IAsyncReactiveProperty<T> value, MessagePackSerializerOptions options)
		{
			if (value == null)
			{
				writer.WriteNil();
				return;
			}

			options.Resolver.GetFormatterWithVerify<T>().Serialize(ref writer, value.Value, options);
		}

		public IAsyncReactiveProperty<T> Deserialize (ref MessagePackReader reader, MessagePackSerializerOptions options)
		{
			if (reader.TryReadNil())
			{
				return null;
			}

			var value = options.Resolver.GetFormatterWithVerify<T>().Deserialize(ref reader, options);
			return new AsyncReactiveProperty<T>(value);
		}
	}

	public sealed class ReadOnlyInterfaceAsyncReactivePropertyFormatter<T> : IMessagePackFormatter<IReadOnlyAsyncReactiveProperty<T>>
	{
		public void Serialize (ref MessagePackWriter writer, IReadOnlyAsyncReactiveProperty<T> value, MessagePackSerializerOptions options)
		{
			if (value == null)
			{
				writer.WriteNil();
				return;
			}

			options.Resolver.GetFormatterWithVerify<T>().Serialize(ref writer, value.Value, options);
		}

		public IReadOnlyAsyncReactiveProperty<T> Deserialize (ref MessagePackReader reader, MessagePackSerializerOptions options)
		{
			if (reader.TryReadNil())
			{
				return null;
			}

			var value = options.Resolver.GetFormatterWithVerify<T>().Deserialize(ref reader, options);
			return new AsyncReactiveProperty<T>(value);
		}
	}
}
