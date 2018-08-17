using ServiceModel.Models.Esteem;
using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace ServiceModel
{
    public static class EnumExtensions
    {
        public static string ToDescriptionString(this EST_HWAssetStatus val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        public static string ToBAMString(this EST_HWAssetStatus val)
        {
            BAMStringValueAttribute[] attributes = (BAMStringValueAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(BAMStringValueAttribute), false);
            return attributes.Length > 0 ? attributes[0].Value : string.Empty;
        }

        static Hashtable _stringValues;

        public static string GetStringValue(Enum value)
        {
            string output = null;
            Type type = value.GetType();

            //Check first in our cached results...
            if (_stringValues.ContainsKey(value))
                output = (_stringValues[value] as BAMStringValueAttribute).Value;
            else
            {
                //Look for our 'StringValueAttribute' 
                //in the field's custom attributes
                FieldInfo fi = type.GetField(value.ToString());
                BAMStringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(BAMStringValueAttribute), false) as BAMStringValueAttribute[];
                if (attrs.Length > 0)
                {
                    _stringValues.Add(value, attrs[0]);
                    output = attrs[0].Value;
                }
            }
            return output;
        }
    }

    public class BAMStringValueAttribute : Attribute
    {
        public BAMStringValueAttribute(string value)
        { Value = value; }
        public string Value { get; }
    }
}
