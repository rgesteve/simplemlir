using System;
using System.IO;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace simplemlir
{
    public class TileGen
    {
        public static string ReadSource(string fname)
        {
            try {
                using (StreamReader sr = new StreamReader(fname)) {
                    return sr.ReadToEnd();
                }
            } catch (Exception ex) {
                Console.WriteLine($"Encounterd error {ex.Message}");
            }
            return ""; // TODO -- should re-throw the exception
        }

        public static string KernelName(string code)
        {
            var tree = CSharpSyntaxTree.ParseText(code);
            CompilationUnitSyntax root = tree.GetRoot() as CompilationUnitSyntax;
            NamespaceDeclarationSyntax ns = root.Members.First() as NamespaceDeclarationSyntax;
            ClassDeclarationSyntax cls = ns.Members.First() as ClassDeclarationSyntax;
            MethodDeclarationSyntax krnl = cls.Members.First() as MethodDeclarationSyntax; // TODO -- hard-coded to test code
            //krnl.ParameterList.Parameters.Count;
            return krnl.Identifier.ToString();
        }
    }
}
