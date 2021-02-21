//----------------------------------------------------------------------------------------
//	Copyright © 2006 - 2018 Tangible Software Solutions, Inc.
//	This class can be used by anyone provided that the copyright notice remains intact.
//
//	This class provides the ability to initialize and delete array elements.
//----------------------------------------------------------------------------------------
using System;
using System.IO;

internal static class Arrays
{
    public static T[] CreateAndInitialize<T> ( int length, T initialValue ) where T: new()
    {
        var array = new T[length];
        for (var index = 0; index < length; ++index)
            array[index] = initialValue;

        return array;
    }

	public static T[] InitializeWithDefaultInstances<T>(int length) where T : new()
	{
		T[] array = new T[length];
		for (int i = 0; i < length; i++)
		{
			array[i] = new T();
		}
		return array;
	}

	public static void DeleteArray<T>(T[] array) where T: System.IDisposable
	{
		foreach (T element in array)
		{
			if (element != null)
				element.Dispose();
		}
	}
}