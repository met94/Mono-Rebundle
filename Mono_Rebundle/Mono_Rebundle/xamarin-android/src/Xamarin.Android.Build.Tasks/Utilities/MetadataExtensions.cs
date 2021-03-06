using System;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;

namespace Xamarin.Android.Tasks
{
	public static class MetadataExtensions
	{
		public static string GetCustomAttributeFullName (this MetadataReader reader, CustomAttribute attribute)
		{
			if (attribute.Constructor.Kind == HandleKind.MemberReference) {
				var ctor = reader.GetMemberReference ((MemberReferenceHandle)attribute.Constructor);
				var type = reader.GetTypeReference ((TypeReferenceHandle)ctor.Parent);
				return reader.GetString (type.Namespace) + "." + reader.GetString (type.Name);
			} else if (attribute.Constructor.Kind == HandleKind.MethodDefinition) {
				var ctor = reader.GetMethodDefinition ((MethodDefinitionHandle)attribute.Constructor);
				var type = reader.GetTypeDefinition (ctor.GetDeclaringType ());
				return reader.GetString (type.Namespace) + "." + reader.GetString (type.Name);
			}
			return null;
		}
	}
}
