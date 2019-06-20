using System;

namespace Business.Helpers
{
    public class Helper
    {
        //string str = "";

        //private static PropertyInfo[] GetProperties(object obj)
        //{
        //    return obj.GetType().GetProperties();
        //}
        //public string PrintTModelPropertyAndValue1<T>(object tmodelObj)
        //{
        //    string str = "";
        //    // Get property array
        //    var properties = GetProperties(tmodelObj);

        //    foreach (var prop in properties)
        //    {
        //        string name = prop.Name;
        //        var value = prop.GetValue(tmodelObj, null);
        //        str += name;
        //        str += " :  ";
        //        str += value;
        //        str += "," +Environment.NewLine;
        //    }

        //    return str;
        //    /* string str = "";
        //     foreach (var prop in typeof(T).GetFields())
        //     {
        //         str += prop.Name.ToString();
        //         str += " :  ";
        //         str += prop.GetValue(tmodelObj);
        //         str += ",   ";
        //     }
        //     return str;*/
        //}


        //public string PrintTModelPropertyAndValue<T>(object obj)
        //{
        //    PrintProperties<T>(obj, 0);
        //    return str;
        //}
        //public void PrintProperties<T>(object obj, int indent)
        //{
        //    if (obj == null) return;
        //    string indentString = new string(' ', indent);
        //    Type objType = obj.GetType();
        //    PropertyInfo[] properties = objType.GetProperties();
        //    foreach (PropertyInfo property in properties)
        //    {
        //        object propValue = property.GetValue(obj, null);
        //        if (property.PropertyType.Assembly == objType.Assembly && !property.PropertyType.IsEnum)
        //        {
        //            str = str + indentString+ property.Name;
        //            PrintProperties<T>(propValue, indent + 2);
        //        }
        //        else
        //        {
        //            str= str + indentString + property.Name + propValue;
        //        }
        //    }
        //}


        public string PrintTModelPropertyAndValue<T>(object output)
        {
            string objectAsXmlString;

            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(output.GetType());
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                try
                {
                    xs.Serialize(sw, output);
                    objectAsXmlString = sw.ToString();
                }
                catch (Exception ex)
                {
                    objectAsXmlString = ex.ToString();
                }
            }

            return objectAsXmlString;
        }

    }
}
