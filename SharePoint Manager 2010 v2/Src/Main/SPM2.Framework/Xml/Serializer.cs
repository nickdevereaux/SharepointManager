using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;


namespace SPM2.Framework
{
    public class Serializer
    {
        #region Members

        private static Object thisLock = new Object();
        private static Object lockDBAttributeOverride = new Object();

        private static Hashtable _serializerStore = new Hashtable();

        private static Hashtable _attributeOverrideStore = new Hashtable();

        private static XmlSerializerFactory factory = new XmlSerializerFactory();

        #endregion

        #region Methods


        public static XmlSerializer GetXmlSerializer(Type objType)
        {
            string key = "XML" + objType.FullName;

            XmlSerializer serializer = (XmlSerializer)GetSerializerObject(key);
            if (serializer == null)
            {
                serializer = factory.CreateSerializer(objType);

                SetSerializerObject(key, serializer);
            }
            return serializer;
        }

        public static XmlSerializer GetXmlSerializer(Type objType, XmlAttributeOverrides xmlOverrides)
        {
            string hashkey = string.Empty;
            if (xmlOverrides != null)
            {
                hashkey = xmlOverrides.GetHashCode().ToString();
            }
            string key = "XML" + objType.FullName + hashkey;

            XmlSerializer serializer = (XmlSerializer)GetSerializerObject(key);
            if (serializer == null)
            {
                serializer = factory.CreateSerializer(objType, xmlOverrides);
                SetSerializerObject(key, serializer);
            }
            return serializer;
        }


        private static object GetSerializerObject(string key)
        {
            object result = null;
            lock (thisLock)
            {
                if (_serializerStore.ContainsKey(key))
                {
                    result = _serializerStore[key];
                }
            }
            return result;
        }

        private static void SetSerializerObject(string key, object serializer)
        {
            lock (thisLock)
            {
                _serializerStore[key] = serializer;
            }
        }



        #endregion

        #region Serialize XML


        public static string ObjectToXML(object input)
        {
            XmlAttributeOverrides xmlOverrides = new XmlAttributeOverrides();

            return ObjectToXML(input, null);
        }


        public static string ObjectToXML(object input, XmlAttributeOverrides xmlOverrides)
        {
            string resultXml = null;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                XmlTextWriter writer = new XmlTextWriter(memoryStream, null);
                writer.Formatting = Formatting.Indented;

                ObjectToXML(input, writer, xmlOverrides);

                resultXml = Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            return resultXml;
        }

        public static void ObjectToXML(object input, XmlWriter writer, XmlAttributeOverrides xmlOverrides)
        {
            if (input == null)
            {
                throw new NullReferenceException("Input object cannot be null");
            }

            XmlSerializer serializer = GetXmlSerializer(input.GetType(), xmlOverrides);

            //Create our own namespaces for the output
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();

            //Add an empty namespace and empty value
            ns.Add("", "");

            // Do the serialization here!
            serializer.Serialize(writer, input, ns);
        }

        public static object XmlToObject(string xml, Type typeOfObject)
        {
            return null;
        }


        public static T XmlToObject<T>(string xml)
        {
            T resultObj = default(T);
            if (!String.IsNullOrEmpty(xml))
            {
                Type objType = typeof(T);
                XmlSerializer serializer = GetXmlSerializer(objType);
                StringReader sr = new StringReader(xml);
                try
                {
                    resultObj = (T)serializer.Deserialize(sr);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Unable to deserialize object '" + objType.FullName + "' from this xml :" + xml, ex);
                }
                finally
                {
                    sr.Close();
                }
            }
            return resultObj;
        }


        public static T Clone<T>(T instance)
        {
            string xml = ObjectToXML(instance);
            return XmlToObject<T>(xml);
        }

        public static object DeserializeFromElement(XmlElement element, Type objectType)
        {
            object result = null;
            using (XmlNodeReader reader = new XmlNodeReader(element))
            {
                XmlSerializer xs = Serializer.GetXmlSerializer(objectType);

                result = xs.Deserialize(reader);
            }
            return result;
        }

        public static string FormatXml(string xml)
        {
            //XmlDocument xDoc = new XmlDocument();

            //xDoc.LoadXml(xml);
            //string result = string.Empty;

            //using (StringWriter sw = new StringWriter())
            //{
            //    XmlTextWriter writer = new XmlTextWriter(sw);
            //    writer.Formatting = Formatting.Indented;

            //    xDoc.WriteTo(writer);
            //    result = sw.ToString();
            //}
            string tempXml = xml;
            bool rooted = xml.Trim().StartsWith("<?");
            if (!rooted)
            {
                tempXml = "<rootformatter>" + xml + "</rootformatter>";
            }


            string result = null;
            using (StringReader sr = new StringReader(tempXml))
            {
                XmlTextReader xtr = new XmlTextReader(sr);

                using (StringWriter sw = new StringWriter())
                {
                    XmlTextWriter xtw = new XmlTextWriter(sw);
                    xtw.Formatting = Formatting.Indented;

                    xtr.MoveToContent();
                    while (!xtr.EOF)
                    {
                        xtw.WriteNode(xtr, false);
                    }
                    result = sw.ToString();
                }
            }

            if (!rooted)
            {
                int rootlenght = "<rootformatter>".Length;
                int newLineLength = Environment.NewLine.Length;
                result = result.Substring(rootlenght + newLineLength, result.Length - ((rootlenght * 2) + 1 + newLineLength));
            }

            return result;
        }


        public static void LoadAttributes(object obj, XmlTextReader xtr)
        {
            if(xtr.HasAttributes)
            {
                
                Type objType = obj.GetType();
                PropertyInfo[] properties = objType.GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in properties)
                {
                    string rawValue = xtr.GetAttribute(prop.Name);

                    if (rawValue != null)
                    {
                        object parsedValue = null;
                        Type propType = prop.PropertyType;
                        if (propType.IsEnum)
                        {
                            parsedValue = Enum.Parse(propType, rawValue);
                        }
                        else
                        {
                            MethodInfo parseInfo = propType.GetMethod("Parse", BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
                            if (parseInfo != null)
                            {
                                parsedValue = parseInfo.Invoke(null, new object[] { rawValue });
                            }
                            else
                            {
                                parsedValue = rawValue;
                            }
                        }

                        if (parsedValue != null)
                        {
                            prop.SetValue(obj, parsedValue, null);
                        }
                    }
                }
            }
        }

        

        private static string Utf8ToUnicode(string utf8)
        {
            return Encoding.Unicode.GetString(Encoding.Convert(Encoding.UTF8, Encoding.Unicode, Encoding.UTF8.GetBytes(utf8)));
        }

        private static String UTF8ByteArrayToString(Byte[] characters)
        {

            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        private static Byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }


        #endregion

        #region Serialize Binary

        public static byte[] ObjectToBinary(object input, Type objType)
        {
            byte[] result = null;
            BinaryFormatter binFormatter = new BinaryFormatter();
            MemoryStream dataStream = new MemoryStream();
            try
            {
                binFormatter.Serialize(dataStream, input);
                result = dataStream.GetBuffer();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to binary serialize object '" + objType.FullName + "'", ex);
            }
            finally
            {
                dataStream.Close();
            }
            return result;
        }

        public static object BinaryToObject(byte[] data, Type objType)
        {
            object resultObj = null;
            BinaryFormatter binFormatter = new BinaryFormatter();
            MemoryStream dataStream = new MemoryStream(data);
            try
            {
                resultObj = binFormatter.Deserialize(dataStream);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to deserialize object '" + objType.FullName + "' from binary.", ex);
            }
            return resultObj;
        }
        #endregion

    }
}