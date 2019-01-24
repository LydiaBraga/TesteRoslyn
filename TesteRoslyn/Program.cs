using static System.Console;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;
using Microsoft.CodeAnalysis.Text;

namespace TesteRoslyn
{
    class Program
    {

        const string programText = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [SimpleAttr(intProp: 5, stringProp: ""Hello World"")]
    public class SimpleClassToAnalyze
        {
            public int MySimpleProperty { get; set; }

            public event Action<object> MySimpleEvent;

            public int MySimpleMethod(string str, out bool flag, int i = 5)
            {
                flag = true;

                return 5;
            }
        }
    }";

        static void Main(string[] args){
            SyntaxTree tree = CSharpSyntaxTree.ParseText(programText);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();


            foreach(SyntaxNode node in root.DescendantNodes()){
                if (node.GetType() == typeof(AttributeListSyntax)){
                    AttributeListSyntax attr = (AttributeListSyntax) node;
                    foreach (AttributeSyntax attribute in attr.Attributes){
                        WriteLine($"Attribute Name: {attribute.Name.ToString()}");

                        AttributeArgumentListSyntax arguments = attribute.ArgumentList;

                        foreach (AttributeArgumentSyntax argument in arguments.Arguments){
                            WriteLine($"Argument Name: {argument.NameColon.ToString()} " +
                                      "- Value: {argument.Expression.ToString()}");
                        }
                    }
                }
            }

            //Teste lendo arquivo externo
            var currentWd = Directory.GetParent(Directory.GetCurrentDirectory()).
                                    Parent.Parent.FullName;
            var path = currentWd + "/TestClass.cs";
            var stream = File.OpenRead(path);
            tree = CSharpSyntaxTree.ParseText
                                    (SourceText.From(stream), path: path);
            
            root = tree.GetCompilationUnitRoot();


            foreach (SyntaxNode node in root.DescendantNodes()) {
                if (node.GetType() == typeof(AttributeListSyntax)) {
                    AttributeListSyntax attr = (AttributeListSyntax)node;
                    foreach (AttributeSyntax attribute in attr.Attributes) {
                        WriteLine($"Attribute Name: {attribute.Name.ToString()}");

                        AttributeArgumentListSyntax arguments = attribute.ArgumentList;

                        /*foreach (AttributeArgumentSyntax argument in arguments.Arguments) {
                            WriteLine($"Argument Name: {argument.NameColon.ToString()} - Value: {argument.Expression.ToString()}");
                        }*/
                    }
                }
            }

            ReadLine();
        }
    }
}
