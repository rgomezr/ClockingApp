using System;
using System.Globalization;

namespace ClockingApp.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class DoublePrecisionAttribute : Attribute
	{
		private readonly double value;

		public DoublePrecisionAttribute(int decimalPlaces)
		{
			value = Math.Round(this.value, decimalPlaces);
		}
	}
}

