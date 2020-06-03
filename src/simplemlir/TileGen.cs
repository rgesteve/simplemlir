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
            ReturnStatementSyntax ret = krnl.Body.Statements.Last() as ReturnStatementSyntax;
            //ret.Expression;
            var nxtToLast = krnl.Body.Statements.Count - 2;
            ExpressionStatementSyntax tgtExp = krnl.Body.Statements[nxtToLast] as ExpressionStatementSyntax;
            // %0 = tile.contract add, mul, %cst, %arg1, %arg0 {sink = #map0, srcs = [#map1, #map2]} : !f32, tensor<3x4x!eltwise.f32>, tensor<4x3x!eltwise.f32> -> tensor<3x3x!eltwise.f32>
            return krnl.Identifier.ToString();
        }
    }
}
