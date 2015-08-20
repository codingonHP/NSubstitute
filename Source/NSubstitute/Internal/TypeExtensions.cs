﻿#if DNXCORE50
using ms=System.Reflection.TypeExtensions;
#endif

namespace System.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal static class TypeExtensions
    {
        public static Type[] GetGenericParameters(this Type type)
        {
#if DNXCORE50
            return type.GetGenericTypeDefinition().GetTypeInfo().GenericTypeParameters;
#elif NET4
            return type.GetGenericTypeDefinition().GetGenericArguments();
#else
            return type.GetGenericTypeDefinition().GetTypeInfo().GenericTypeParameters;
#endif
        }

        public static IEnumerable<ConstructorInfo> GetDeclaredConstructors(this Type type)
        {
#if DNXCORE50
            return type.GetTypeInfo().DeclaredConstructors;
#elif NET4
            return type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
#else
            return type.GetTypeInfo().DeclaredConstructors;
#endif
        }

        public static IEnumerable<MemberInfo> GetDeclaredMembers(this Type type)
        {
#if DNXCORE50
            return type.GetTypeInfo().DeclaredMembers;
#elif NET4
            return type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
#else
            return type.GetTypeInfo().DeclaredMembers;
#endif
        }

        public static IEnumerable<MethodInfo> GetDeclaredMethods(this Type type)
        {
#if DNXCORE50
            return type.GetTypeInfo().DeclaredMethods;
#elif NET40
            return type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
#else
            return type.GetTypeInfo().DeclaredMethods;
#endif
        }

        public static IEnumerable<MethodInfo> GetAllMethods(this Type type)
        {
#if NET4
            return type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
#elif DNXCORE50
            return type.GetMethods();
#else
            return type.GetRuntimeMethods();
#endif
        }

        public static IEnumerable<PropertyInfo> GetDeclaredProperties(this Type type)
        {
#if DNXCORE50
            return type.GetTypeInfo().DeclaredProperties;
#elif NET4
            return type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
#else
            return type.GetTypeInfo().DeclaredProperties;
#endif
        }

        public static bool IsStatic(this FieldInfo fieldInfo)
        {
            return fieldInfo?.IsStatic ?? false;
        }

        public static bool IsStatic(this PropertyInfo propertyInfo)
        {
            return propertyInfo?.GetGetMethod(true)?.IsStatic
                ?? propertyInfo?.GetSetMethod(true)?.IsStatic
                ?? false;
        }

        public static bool IsStatic(this MemberInfo memberInfo)
        {
            return (memberInfo as FieldInfo).IsStatic() || (memberInfo as PropertyInfo).IsStatic();
        }

        public static bool IsPublic(this PropertyInfo propertyInfo)
        {
            return (propertyInfo?.GetGetMethod(true)?.IsPublic ?? false)
                || (propertyInfo?.GetSetMethod(true)?.IsPublic ?? false);
        }

        public static bool IsPublic(this MemberInfo memberInfo)
        {
            return (memberInfo as FieldInfo)?.IsPublic ?? (memberInfo as PropertyInfo).IsPublic();
        }

        public static bool IsNotPublic(this ConstructorInfo constructorInfo)
        {
            return constructorInfo.IsPrivate
                   || constructorInfo.IsFamilyAndAssembly
                   || constructorInfo.IsFamilyOrAssembly
                   || constructorInfo.IsFamily;
        }

        public static Assembly Assembly(this Type type)
        {
#if DNXCORE50
            return type.GetTypeInfo().Assembly;
#elif NET4
            return type.Assembly;
#else
            return type.GetTypeInfo().Assembly;
#endif
        }

        public static Type BaseType(this Type type)
        {
#if DNXCORE50
            return type.GetTypeInfo().BaseType;
#elif NET4
            return type.BaseType;
#else
            return type.GetTypeInfo().BaseType;
#endif
        }

        public static object[] GetCustomAttributes(this Type type, Type attributeType, bool inherit)
        {
#if DNXCORE50
            return type.GetTypeInfo().GetCustomAttributes(attributeType, inherit).ToArray();
#else
            return type.GetCustomAttributes(attributeType, inherit).ToArray();
#endif
        }

        public static bool IsAbstract(this Type type)
        {
#if DNXCORE50
            return type.GetTypeInfo().IsAbstract;
#elif NET4
            return type.IsAbstract;
#else
            return type.GetTypeInfo().IsAbstract;
#endif
        }

        public static bool IsClass(this Type type)
        {
#if DNXCORE50
            return type.GetTypeInfo().IsClass;
#elif NET40
            return type.IsClass;
#else
            return type.GetTypeInfo().IsClass;
#endif
        }

        public static bool IsEnum(this Type type)
        {
#if DNXCORE50
            return type.GetTypeInfo().IsEnum;
#elif NET40
            return type.IsEnum;
#else
            return type.GetTypeInfo().IsEnum;
#endif
        }

        public static bool IsGenericType(this Type type)
        {
#if DNXCORE50
            return type.GetTypeInfo().IsGenericType;
#elif NET4
            return type.IsGenericType;
#else
            return type.GetTypeInfo().IsGenericType;
#endif
        }

        public static bool IsGenericTypeDefinition(this Type type)
        {
#if DNXCORE50
            return type.GetTypeInfo().IsGenericTypeDefinition;
#elif NET40
            return type.IsGenericTypeDefinition;
#else
            return type.GetTypeInfo().IsGenericTypeDefinition;
#endif
        }

        public static bool IsInterface(this Type type)
        {
#if DNXCORE50
            return type.GetTypeInfo().IsInterface;
#elif NET40
            return type.IsInterface;
#else
            return type.GetTypeInfo().IsInterface;
#endif
        }

        public static bool IsPrimitive(this Type type)
        {
#if DNXCORE50
            return type.GetTypeInfo().IsPrimitive;
#elif SILVERLIGHT || MONODROID || MONOTOUCH || __IOS__ || NET4
            return type.IsPrimitive;
#else
            return type.GetTypeInfo().IsPrimitive;
#endif
        }

        public static bool IsSealed(this Type type)
        {
#if DNXCORE50
            return type.GetTypeInfo().IsSealed;
#elif NET4
            return type.IsSealed;
#else
            return type.GetTypeInfo().IsSealed;
#endif
        }

        public static bool IsValueType(this Type type)
        {
#if DNXCORE50
            return type.GetTypeInfo().IsValueType;
#elif NET4
            return type.IsValueType;
#else
            return type.GetTypeInfo().IsValueType;
#endif
        }


#if !NET40
        public static bool IsInstanceOfType(this Type type, object o)
        {
            return o != null && type.IsAssignableFrom(o.GetType());
        }

        public static ConstructorInfo[] GetConstructors(this Type type)
        {
            return type.GetTypeInfo().DeclaredConstructors.ToArray();
        }

#if !DNXCORE50
        public static Type[] GetTypes(this Assembly assembly)
        {
            return assembly.ExportedTypes.ToArray();
        }

        public static MethodInfo GetGetMethod(this PropertyInfo propertyInfo, bool ignored)
        {
            return propertyInfo.GetMethod;
        }

        public static MethodInfo GetSetMethod(this PropertyInfo propertyInfo, bool ignored)
        {
            return propertyInfo.SetMethod;
        }

        public static FieldInfo GetField(this Type type, string name)
        {
            return type.GetTypeInfo().GetDeclaredField(name);
        }

        public static MemberInfo[] GetMember(this Type type, string name)
        {
            return type.GetTypeInfo().DeclaredMembers
                .Where(m => m.Name == name).ToArray()
            ;
        }

        public static MethodInfo[] GetAccessors(this PropertyInfo propertyInfo)
        {
            return new[] { propertyInfo.GetMethod, propertyInfo.SetMethod };
        }

        public static PropertyInfo GetProperty(this Type type, string name)
        {
            return type.GetTypeInfo().GetDeclaredProperty(name);
        }

        public static MethodInfo GetMethod(this Type type, string name)
        {
            return type.GetTypeInfo().GetDeclaredMethod(name);
        }

        public static MethodInfo GetMethod(this Type type, string name, Type[] parameterTypes)
        {
            return type.GetTypeInfo().GetDeclaredMethods(name).FirstOrDefault(mi => mi.GetParameters().Select(pi => pi.ParameterType).ToArray().SequenceEqual(parameterTypes));
        }

        public static bool IsAssignableFrom(this Type type, Type other)
        {
            return type.GetTypeInfo().IsAssignableFrom(other.GetTypeInfo());
        }

        public static Type[] GetGenericArguments(this Type type)
        {
            return type.GetTypeInfo().GenericTypeArguments;
        }

        public static IEnumerable<Type> GetInterfaces(this Type type)
        {
            return type.GetTypeInfo().ImplementedInterfaces;
        }
#endif

#endif
    }
}