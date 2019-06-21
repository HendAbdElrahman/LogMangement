using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModels;

namespace Business.Tests
{

    [TestClass]
    public class ParserFactoryTest
    {
       
      
        [TestMethod]
        public void Build_PassXML_RetuenXMLParser()
        {
            //arrange
            var parserFactory = new ParserFactory<Modules> ();
            string xmlData = "<?xml version='1.0' encoding='UTF-8'?><Modules xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'><channel><resources> <resource name='x' refresh_interval='180'>text1</resource><resource name='y' refresh_interval='181'>text2</resource><resource name='z' refresh_interval='182'>text3</resource></resources></channel></Modules>";
            
            //act
            var result = parserFactory.Build(xmlData);

            //assert
            Assert.IsInstanceOfType(result, typeof(XMLParser<Modules>));

        }

        [TestMethod]
        public void Build_PassJson_RetuenJsonParser()
        {
            //arrange
            var parserFactory = new ParserFactory<Modules>();
            string jsonData = @"{
   'channel': {
      'resources': [
         {
            'name': 'x',
            'refresh_interval': 180,
            'text': 'text1'
         },
         {
            'name': 'y',
            'refresh_interval': 181,
            'text': 'text2'
         },
         {
            'name': 'z',
            'refresh_interval': 182,
            'text': 'text3'
         }
      ]
   }
}";
            //act
            var result = parserFactory.Build(jsonData);

            //assert
            Assert.IsInstanceOfType(result, typeof(JSONParser<Modules>));

        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Build_PassUnknown_ThrowException()
        {
            //arrange
            var parserFactory = new ParserFactory<Modules>();
            string unknowndata = "UnExpectedData";
            //act
            var result = parserFactory.Build(unknowndata);
        }

    }
}
