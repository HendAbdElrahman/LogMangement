using System;

namespace Business.Helpers
{
    public class Helper
    {
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
