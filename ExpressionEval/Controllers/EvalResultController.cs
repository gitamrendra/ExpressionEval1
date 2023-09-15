using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml;
namespace ExpressionEval.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvalResultController : ControllerBase
    {
        [HttpGet("eval")]
        public ActionResult GetExpressionEvalResult()
        {
            XmlDocument resXmlDocument = new XmlDocument();

            StringBuilder resXml = new StringBuilder();
            resXml.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            resXml.Append("<expressions>");
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("Data/Expressions.xml");
            //xmlDocument.SelectNodes("").
            foreach(XmlNode node in xmlDocument.ChildNodes[1])
            {
                switch (node.Name)
                {
                    case "addition":
                        int sum = 0;
                        for(int i=0;i<node.ChildNodes.Count;i++)
                        {
                            sum += Int32.Parse(node.ChildNodes[i].InnerText);
                        }
                        resXml.Append($"<result id=\"{node.Attributes[0].Value}\">{sum} </result>");
                        break;
                    case "subtraction":
                        int sub = 0;
                        int minuend = Int32.Parse(node.SelectSingleNode("minuend").InnerText);
                        int sutrahend = Int32.Parse(node.SelectSingleNode("sutrahend").InnerText);
                        resXml.Append($"<result id=\"{node.Attributes[0].Value}\">{minuend- sutrahend} </result>");
                        break;
                    case "multiplication":
                        int mul = 1;
                        for (int i = 0; i < node.ChildNodes.Count; i++)
                        {
                            mul *= Int32.Parse(node.ChildNodes[i].InnerText);
                        }
                        resXml.Append($"<result id=\"{node.Attributes[0].Value}\">{mul} </result>");
                        break;
                    case "division":
                        int div = 0;
                        int dividend = Int32.Parse(node.SelectSingleNode("dividend").InnerText);
                        int divisior = Int32.Parse(node.SelectSingleNode("divisior").InnerText);
                        resXml.Append($"<result id=\"{node.Attributes[0].Value}\">{dividend - divisior} </result>");
                        break;
                    default:
                        break;
                }
            }
            resXml.Append("</expressions>");
            return Ok(resXml.ToString());
        }
    }
}
