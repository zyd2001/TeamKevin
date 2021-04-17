// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  LAPTOP-KDKHEG7R
// DateTime: 2021/4/17 15:10:01
// UserName: zyd20
// Input file <CompilerCore/RT.y - 2021/4/17 15:09:59>

// options: no-lines diagnose & report gplex

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using QUT.Gppg;

namespace CompilerCore
{
internal enum Tokens {
    error=127,EOF=128,AND=129,OR=130,EQ=131,NE=132,
    GT=133,GE=134,LT=135,LE=136,UMINUS=137,INT_LITERAL=138,
    FLOAT_LITERAL=139,VECTOR_LITERAL=140,IDENTIFIER=141,INT=142,FLOAT=143,VECTOR=144,
    VOID=145,IF=146,ELSE=147,ELSEIF=148,FOR=149,WHILE=150,
    BREAK=151,CONTINUE=152,RETURN=153,STRUCT=154,CONST=155,INCREMENT=156,
    DECREMENT=157};

internal struct ValueType
{ 
    internal Expression Expression;
    internal Statement Statement;
    internal List<Expression> ExpressionList;
    internal List<Statement> StatementList;
    internal string Identifier;
    internal DeclarationStatement.DeclarationItem DeclaraionItem;
    internal List<DeclarationStatement.DeclarationItem> DeclarationList;
    internal int IntLiteral;
    internal float FloatLiteral;
    internal string VectorLiteral;
    internal IfStatement IfStatement;
    internal Type Type;
    internal List<FunctionDefinitionStatement.Parameter> ParameterList;
}
// Abstract base class for GPLEX scanners
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
internal abstract class ScanBase : AbstractScanner<ValueType,LexLocation> {
  private LexLocation __yylloc = new LexLocation();
  public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

// Utility class for encapsulating token information
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
internal class ScanObj {
  public int token;
  public ValueType yylval;
  public LexLocation yylloc;
  public ScanObj( int t, ValueType val, LexLocation loc ) {
    this.token = t; this.yylval = val; this.yylloc = loc;
  }
}

[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
internal class Parser: ShiftReduceParser<ValueType, LexLocation>
{
  // Verbatim content from CompilerCore/RT.y - 2021/4/17 15:09:59
    internal List<Statement> AST;
  // End verbatim content from CompilerCore/RT.y - 2021/4/17 15:09:59

#pragma warning disable 649
  private static Dictionary<int, string> aliases;
#pragma warning restore 649
  private static Rule[] rules = new Rule[82];
  private static State[] states = new State[148];
  private static string[] nonTerms = new string[] {
      "program", "value_type", "statement", "loop_statement", "return_statement", 
      "assignment_statement", "function_definition_statement", "declaration_statement", 
      "for_special_statement", "block_statement", "toplevel_statement", "if_statement", 
      "declaration_item", "declaration_list", "statement_list", "optional_statement_list", 
      "expression_list", "expression", "binary_expression", "unary_expression", 
      "literal_expression", "index_expression", "possible_array_expression", 
      "optional_expression", "assignment_lval_expression", "identifier_expression", 
      "function_call_expression", "parameter_list", "$accept", };

  static Parser() {
    states[0] = new State(new int[]{142,121,143,122,144,123,145,124,155,125},new int[]{-1,1,-11,147,-8,4,-2,6,-7,146});
    states[1] = new State(new int[]{128,2,142,121,143,122,144,123,145,124,155,125},new int[]{-11,3,-8,4,-2,6,-7,146});
    states[2] = new State(-1);
    states[3] = new State(-3);
    states[4] = new State(new int[]{59,5});
    states[5] = new State(-4);
    states[6] = new State(new int[]{141,69},new int[]{-14,7,-13,120});
    states[7] = new State(new int[]{44,8,59,-42,41,-42});
    states[8] = new State(new int[]{141,10},new int[]{-13,9});
    states[9] = new State(-45);
    states[10] = new State(new int[]{61,11,91,66,44,-46,59,-46,41,-46});
    states[11] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,12,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[12] = new State(new int[]{43,13,45,15,42,17,47,19,37,21,131,23,132,25,133,27,134,29,135,31,136,33,129,35,130,37,44,-47,59,-47,41,-47});
    states[13] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,14,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[14] = new State(new int[]{43,-54,45,-54,42,17,47,19,37,21,131,-54,132,-54,133,-54,134,-54,135,-54,136,-54,129,-54,130,-54,44,-54,59,-54,41,-54,93,-54});
    states[15] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,16,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[16] = new State(new int[]{43,-55,45,-55,42,17,47,19,37,21,131,-55,132,-55,133,-55,134,-55,135,-55,136,-55,129,-55,130,-55,44,-55,59,-55,41,-55,93,-55});
    states[17] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,18,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[18] = new State(-56);
    states[19] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,20,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[20] = new State(-57);
    states[21] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,22,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[22] = new State(-58);
    states[23] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,24,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[24] = new State(new int[]{43,13,45,15,42,17,47,19,37,21,131,-59,132,-59,133,-59,134,-59,135,-59,136,-59,129,-59,130,-59,44,-59,59,-59,41,-59,93,-59});
    states[25] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,26,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[26] = new State(new int[]{43,13,45,15,42,17,47,19,37,21,131,-60,132,-60,133,-60,134,-60,135,-60,136,-60,129,-60,130,-60,44,-60,59,-60,41,-60,93,-60});
    states[27] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,28,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[28] = new State(new int[]{43,13,45,15,42,17,47,19,37,21,131,-61,132,-61,133,-61,134,-61,135,-61,136,-61,129,-61,130,-61,44,-61,59,-61,41,-61,93,-61});
    states[29] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,30,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[30] = new State(new int[]{43,13,45,15,42,17,47,19,37,21,131,-62,132,-62,133,-62,134,-62,135,-62,136,-62,129,-62,130,-62,44,-62,59,-62,41,-62,93,-62});
    states[31] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,32,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[32] = new State(new int[]{43,13,45,15,42,17,47,19,37,21,131,-63,132,-63,133,-63,134,-63,135,-63,136,-63,129,-63,130,-63,44,-63,59,-63,41,-63,93,-63});
    states[33] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,34,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[34] = new State(new int[]{43,13,45,15,42,17,47,19,37,21,131,-64,132,-64,133,-64,134,-64,135,-64,136,-64,129,-64,130,-64,44,-64,59,-64,41,-64,93,-64});
    states[35] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,36,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[36] = new State(new int[]{43,13,45,15,42,17,47,19,37,21,131,23,132,25,133,27,134,29,135,31,136,33,129,-65,130,-65,44,-65,59,-65,41,-65,93,-65});
    states[37] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,38,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[38] = new State(new int[]{43,13,45,15,42,17,47,19,37,21,131,23,132,25,133,27,134,29,135,31,136,33,129,-66,130,-66,44,-66,59,-66,41,-66,93,-66});
    states[39] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,40,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[40] = new State(new int[]{41,41,43,13,45,15,42,17,47,19,37,21,131,23,132,25,133,27,134,29,135,31,136,33,129,35,130,37});
    states[41] = new State(-38);
    states[42] = new State(-39);
    states[43] = new State(-40);
    states[44] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,45,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[45] = new State(-67);
    states[46] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,47,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[47] = new State(-68);
    states[48] = new State(new int[]{91,49,43,-41,45,-41,42,-41,47,-41,37,-41,131,-41,132,-41,133,-41,134,-41,135,-41,136,-41,129,-41,130,-41,44,-41,59,-41,41,-41,93,-41});
    states[49] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,50,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[50] = new State(new int[]{93,51,43,13,45,15,42,17,47,19,37,21,131,23,132,25,133,27,134,29,135,31,136,33,129,35,130,37});
    states[51] = new State(-74);
    states[52] = new State(-70);
    states[53] = new State(-75);
    states[54] = new State(-76);
    states[55] = new State(-77);
    states[56] = new State(-71);
    states[57] = new State(-72);
    states[58] = new State(new int[]{40,59,91,-69,43,-69,45,-69,42,-69,47,-69,37,-69,131,-69,132,-69,133,-69,134,-69,135,-69,136,-69,129,-69,130,-69,44,-69,59,-69,41,-69,93,-69,61,-69,156,-69,157,-69});
    states[59] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58,41,-79,44,-79},new int[]{-17,60,-18,65,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[60] = new State(new int[]{41,61,44,62});
    states[61] = new State(-78);
    states[62] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,63,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[63] = new State(new int[]{43,13,45,15,42,17,47,19,37,21,131,23,132,25,133,27,134,29,135,31,136,33,129,35,130,37,41,-81,44,-81});
    states[64] = new State(-73);
    states[65] = new State(new int[]{43,13,45,15,42,17,47,19,37,21,131,23,132,25,133,27,134,29,135,31,136,33,129,35,130,37,41,-80,44,-80});
    states[66] = new State(new int[]{138,67});
    states[67] = new State(new int[]{93,68});
    states[68] = new State(-48);
    states[69] = new State(new int[]{40,70,61,11,91,66,44,-46,59,-46});
    states[70] = new State(new int[]{142,121,143,122,144,123,145,124,41,-31,44,-31},new int[]{-28,71,-2,144});
    states[71] = new State(new int[]{41,72,44,141});
    states[72] = new State(new int[]{123,73});
    states[73] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58,146,85,149,91,150,100,153,106,142,121,143,122,144,123,145,124,155,125,152,128,151,130,123,133,125,-29},new int[]{-16,74,-15,76,-3,136,-18,78,-19,42,-20,43,-23,48,-21,52,-22,80,-27,57,-26,81,-12,82,-4,90,-5,105,-6,110,-25,112,-8,117,-2,119,-10,132});
    states[74] = new State(new int[]{125,75});
    states[75] = new State(-28);
    states[76] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58,146,85,149,91,150,100,153,106,142,121,143,122,144,123,145,124,155,125,152,128,151,130,123,133,125,-30},new int[]{-3,77,-18,78,-19,42,-20,43,-23,48,-21,52,-22,80,-27,57,-26,81,-12,82,-4,90,-5,105,-6,110,-25,112,-8,117,-2,119,-10,132});
    states[77] = new State(-7);
    states[78] = new State(new int[]{59,79,43,13,45,15,42,17,47,19,37,21,131,23,132,25,133,27,134,29,135,31,136,33,129,35,130,37});
    states[79] = new State(-8);
    states[80] = new State(new int[]{91,-71,59,-71,43,-71,45,-71,42,-71,47,-71,37,-71,131,-71,132,-71,133,-71,134,-71,135,-71,136,-71,129,-71,130,-71,61,-52,156,-52,157,-52});
    states[81] = new State(new int[]{91,-73,59,-73,43,-73,45,-73,42,-73,47,-73,37,-73,131,-73,132,-73,133,-73,134,-73,135,-73,136,-73,129,-73,130,-73,61,-53,156,-53,157,-53});
    states[82] = new State(new int[]{147,83,40,-9,45,-9,33,-9,138,-9,139,-9,140,-9,141,-9,146,-9,149,-9,150,-9,153,-9,142,-9,143,-9,144,-9,145,-9,155,-9,152,-9,151,-9,123,-9,125,-9});
    states[83] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58,146,85,149,91,150,100,153,106,142,121,143,122,144,123,145,124,155,125,152,128,151,130,123,133},new int[]{-3,84,-18,78,-19,42,-20,43,-23,48,-21,52,-22,80,-27,57,-26,81,-12,82,-4,90,-5,105,-6,110,-25,112,-8,117,-2,119,-10,132});
    states[84] = new State(-27);
    states[85] = new State(new int[]{40,86});
    states[86] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,87,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[87] = new State(new int[]{41,88,43,13,45,15,42,17,47,19,37,21,131,23,132,25,133,27,134,29,135,31,136,33,129,35,130,37});
    states[88] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58,146,85,149,91,150,100,153,106,142,121,143,122,144,123,145,124,155,125,152,128,151,130,123,133},new int[]{-3,89,-18,78,-19,42,-20,43,-23,48,-21,52,-22,80,-27,57,-26,81,-12,82,-4,90,-5,105,-6,110,-25,112,-8,117,-2,119,-10,132});
    states[89] = new State(-26);
    states[90] = new State(-10);
    states[91] = new State(new int[]{40,92});
    states[92] = new State(new int[]{142,121,143,122,144,123,145,124,155,125,138,53,139,54,140,55,141,58},new int[]{-9,93,-8,137,-2,119,-6,138,-25,112,-22,80,-23,139,-21,52,-27,57,-26,81});
    states[93] = new State(new int[]{59,94});
    states[94] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58,59,-22},new int[]{-24,95,-18,140,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[95] = new State(new int[]{59,96});
    states[96] = new State(new int[]{142,121,143,122,144,123,145,124,155,125,138,53,139,54,140,55,141,58},new int[]{-9,97,-8,137,-2,119,-6,138,-25,112,-22,80,-23,139,-21,52,-27,57,-26,81});
    states[97] = new State(new int[]{41,98});
    states[98] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58,146,85,149,91,150,100,153,106,142,121,143,122,144,123,145,124,155,125,152,128,151,130,123,133},new int[]{-3,99,-18,78,-19,42,-20,43,-23,48,-21,52,-22,80,-27,57,-26,81,-12,82,-4,90,-5,105,-6,110,-25,112,-8,117,-2,119,-10,132});
    states[99] = new State(-20);
    states[100] = new State(new int[]{40,101});
    states[101] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,102,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[102] = new State(new int[]{41,103,43,13,45,15,42,17,47,19,37,21,131,23,132,25,133,27,134,29,135,31,136,33,129,35,130,37});
    states[103] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58,146,85,149,91,150,100,153,106,142,121,143,122,144,123,145,124,155,125,152,128,151,130,123,133},new int[]{-3,104,-18,78,-19,42,-20,43,-23,48,-21,52,-22,80,-27,57,-26,81,-12,82,-4,90,-5,105,-6,110,-25,112,-8,117,-2,119,-10,132});
    states[104] = new State(-21);
    states[105] = new State(-11);
    states[106] = new State(new int[]{59,107,40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,108,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[107] = new State(-17);
    states[108] = new State(new int[]{59,109,43,13,45,15,42,17,47,19,37,21,131,23,132,25,133,27,134,29,135,31,136,33,129,35,130,37});
    states[109] = new State(-18);
    states[110] = new State(new int[]{59,111});
    states[111] = new State(-12);
    states[112] = new State(new int[]{61,113,156,115,157,116});
    states[113] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58},new int[]{-18,114,-19,42,-20,43,-23,48,-21,52,-22,56,-27,57,-26,64});
    states[114] = new State(new int[]{43,13,45,15,42,17,47,19,37,21,131,23,132,25,133,27,134,29,135,31,136,33,129,35,130,37,59,-49,41,-49});
    states[115] = new State(-50);
    states[116] = new State(-51);
    states[117] = new State(new int[]{59,118});
    states[118] = new State(-13);
    states[119] = new State(new int[]{141,10},new int[]{-14,7,-13,120});
    states[120] = new State(-44);
    states[121] = new State(-34);
    states[122] = new State(-35);
    states[123] = new State(-36);
    states[124] = new State(-37);
    states[125] = new State(new int[]{142,121,143,122,144,123,145,124},new int[]{-2,126});
    states[126] = new State(new int[]{141,10},new int[]{-14,127,-13,120});
    states[127] = new State(new int[]{44,8,59,-43,41,-43});
    states[128] = new State(new int[]{59,129});
    states[129] = new State(-14);
    states[130] = new State(new int[]{59,131});
    states[131] = new State(-15);
    states[132] = new State(-16);
    states[133] = new State(new int[]{40,39,45,44,33,46,138,53,139,54,140,55,141,58,146,85,149,91,150,100,153,106,142,121,143,122,144,123,145,124,155,125,152,128,151,130,123,133,125,-29},new int[]{-16,134,-15,76,-3,136,-18,78,-19,42,-20,43,-23,48,-21,52,-22,80,-27,57,-26,81,-12,82,-4,90,-5,105,-6,110,-25,112,-8,117,-2,119,-10,132});
    states[134] = new State(new int[]{125,135});
    states[135] = new State(-19);
    states[136] = new State(-6);
    states[137] = new State(-24);
    states[138] = new State(-25);
    states[139] = new State(new int[]{91,49});
    states[140] = new State(new int[]{43,13,45,15,42,17,47,19,37,21,131,23,132,25,133,27,134,29,135,31,136,33,129,35,130,37,59,-23});
    states[141] = new State(new int[]{142,121,143,122,144,123,145,124},new int[]{-2,142});
    states[142] = new State(new int[]{141,143});
    states[143] = new State(-33);
    states[144] = new State(new int[]{141,145});
    states[145] = new State(-32);
    states[146] = new State(-5);
    states[147] = new State(-2);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-29, new int[]{-1,128});
    rules[2] = new Rule(-1, new int[]{-11});
    rules[3] = new Rule(-1, new int[]{-1,-11});
    rules[4] = new Rule(-11, new int[]{-8,59});
    rules[5] = new Rule(-11, new int[]{-7});
    rules[6] = new Rule(-15, new int[]{-3});
    rules[7] = new Rule(-15, new int[]{-15,-3});
    rules[8] = new Rule(-3, new int[]{-18,59});
    rules[9] = new Rule(-3, new int[]{-12});
    rules[10] = new Rule(-3, new int[]{-4});
    rules[11] = new Rule(-3, new int[]{-5});
    rules[12] = new Rule(-3, new int[]{-6,59});
    rules[13] = new Rule(-3, new int[]{-8,59});
    rules[14] = new Rule(-3, new int[]{152,59});
    rules[15] = new Rule(-3, new int[]{151,59});
    rules[16] = new Rule(-3, new int[]{-10});
    rules[17] = new Rule(-5, new int[]{153,59});
    rules[18] = new Rule(-5, new int[]{153,-18,59});
    rules[19] = new Rule(-10, new int[]{123,-16,125});
    rules[20] = new Rule(-4, new int[]{149,40,-9,59,-24,59,-9,41,-3});
    rules[21] = new Rule(-4, new int[]{150,40,-18,41,-3});
    rules[22] = new Rule(-24, new int[]{});
    rules[23] = new Rule(-24, new int[]{-18});
    rules[24] = new Rule(-9, new int[]{-8});
    rules[25] = new Rule(-9, new int[]{-6});
    rules[26] = new Rule(-12, new int[]{146,40,-18,41,-3});
    rules[27] = new Rule(-12, new int[]{-12,147,-3});
    rules[28] = new Rule(-7, new int[]{-2,141,40,-28,41,123,-16,125});
    rules[29] = new Rule(-16, new int[]{});
    rules[30] = new Rule(-16, new int[]{-15});
    rules[31] = new Rule(-28, new int[]{});
    rules[32] = new Rule(-28, new int[]{-2,141});
    rules[33] = new Rule(-28, new int[]{-28,44,-2,141});
    rules[34] = new Rule(-2, new int[]{142});
    rules[35] = new Rule(-2, new int[]{143});
    rules[36] = new Rule(-2, new int[]{144});
    rules[37] = new Rule(-2, new int[]{145});
    rules[38] = new Rule(-18, new int[]{40,-18,41});
    rules[39] = new Rule(-18, new int[]{-19});
    rules[40] = new Rule(-18, new int[]{-20});
    rules[41] = new Rule(-18, new int[]{-23});
    rules[42] = new Rule(-8, new int[]{-2,-14});
    rules[43] = new Rule(-8, new int[]{155,-2,-14});
    rules[44] = new Rule(-14, new int[]{-13});
    rules[45] = new Rule(-14, new int[]{-14,44,-13});
    rules[46] = new Rule(-13, new int[]{141});
    rules[47] = new Rule(-13, new int[]{141,61,-18});
    rules[48] = new Rule(-13, new int[]{141,91,138,93});
    rules[49] = new Rule(-6, new int[]{-25,61,-18});
    rules[50] = new Rule(-6, new int[]{-25,156});
    rules[51] = new Rule(-6, new int[]{-25,157});
    rules[52] = new Rule(-25, new int[]{-22});
    rules[53] = new Rule(-25, new int[]{-26});
    rules[54] = new Rule(-19, new int[]{-18,43,-18});
    rules[55] = new Rule(-19, new int[]{-18,45,-18});
    rules[56] = new Rule(-19, new int[]{-18,42,-18});
    rules[57] = new Rule(-19, new int[]{-18,47,-18});
    rules[58] = new Rule(-19, new int[]{-18,37,-18});
    rules[59] = new Rule(-19, new int[]{-18,131,-18});
    rules[60] = new Rule(-19, new int[]{-18,132,-18});
    rules[61] = new Rule(-19, new int[]{-18,133,-18});
    rules[62] = new Rule(-19, new int[]{-18,134,-18});
    rules[63] = new Rule(-19, new int[]{-18,135,-18});
    rules[64] = new Rule(-19, new int[]{-18,136,-18});
    rules[65] = new Rule(-19, new int[]{-18,129,-18});
    rules[66] = new Rule(-19, new int[]{-18,130,-18});
    rules[67] = new Rule(-20, new int[]{45,-18});
    rules[68] = new Rule(-20, new int[]{33,-18});
    rules[69] = new Rule(-26, new int[]{141});
    rules[70] = new Rule(-23, new int[]{-21});
    rules[71] = new Rule(-23, new int[]{-22});
    rules[72] = new Rule(-23, new int[]{-27});
    rules[73] = new Rule(-23, new int[]{-26});
    rules[74] = new Rule(-22, new int[]{-23,91,-18,93});
    rules[75] = new Rule(-21, new int[]{138});
    rules[76] = new Rule(-21, new int[]{139});
    rules[77] = new Rule(-21, new int[]{140});
    rules[78] = new Rule(-27, new int[]{141,40,-17,41});
    rules[79] = new Rule(-17, new int[]{});
    rules[80] = new Rule(-17, new int[]{-18});
    rules[81] = new Rule(-17, new int[]{-17,44,-18});

    aliases = new Dictionary<int, string>();
    aliases.Add(129, "&&");
    aliases.Add(130, "||");
    aliases.Add(131, "==");
    aliases.Add(132, "!=");
    aliases.Add(133, ">");
    aliases.Add(134, ">=");
    aliases.Add(135, "<");
    aliases.Add(136, "<=");
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
#pragma warning disable 162, 1522
    switch (action)
    {
      case 2: // program -> toplevel_statement
{
        AST = new List<Statement>{ValueStack[ValueStack.Depth-1].Statement};
    }
        break;
      case 3: // program -> program, toplevel_statement
{
        AST.Add(ValueStack[ValueStack.Depth-1].Statement);
    }
        break;
      case 6: // statement_list -> statement
{
        CurrentSemanticValue.StatementList = new List<Statement>{ValueStack[ValueStack.Depth-1].Statement};
    }
        break;
      case 7: // statement_list -> statement_list, statement
{
        ValueStack[ValueStack.Depth-2].StatementList.Add(ValueStack[ValueStack.Depth-1].Statement);
        CurrentSemanticValue.StatementList = ValueStack[ValueStack.Depth-2].StatementList;
    }
        break;
      case 8: // statement -> expression, ';'
{
        CurrentSemanticValue.Statement = new ExpressionStatement(ValueStack[ValueStack.Depth-2].Expression);
    }
        break;
      case 14: // statement -> CONTINUE, ';'
{
        CurrentSemanticValue.Statement = new ControlStatement(ControlStatement.Type.CONTINUE);
    }
        break;
      case 15: // statement -> BREAK, ';'
{
        CurrentSemanticValue.Statement = new ControlStatement(ControlStatement.Type.BREAK);
    }
        break;
      case 17: // return_statement -> RETURN, ';'
{
        CurrentSemanticValue.Statement = new ReturnStatement();
    }
        break;
      case 18: // return_statement -> RETURN, expression, ';'
{
        CurrentSemanticValue.Statement = new ReturnStatement(ValueStack[ValueStack.Depth-2].Expression);
    }
        break;
      case 19: // block_statement -> '{', optional_statement_list, '}'
{
        CurrentSemanticValue.Statement = new BlockStatement(ValueStack[ValueStack.Depth-2].StatementList);
    }
        break;
      case 20: // loop_statement -> FOR, '(', for_special_statement, ';', optional_expression, 
               //                   ';', for_special_statement, ')', statement
{
        CurrentSemanticValue.Statement = new LoopStatement(ValueStack[ValueStack.Depth-7].Statement, ValueStack[ValueStack.Depth-5].Expression, ValueStack[ValueStack.Depth-3].Statement, ValueStack[ValueStack.Depth-1].Statement);
    }
        break;
      case 21: // loop_statement -> WHILE, '(', expression, ')', statement
{
        CurrentSemanticValue.Statement = new LoopStatement(ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Statement);
    }
        break;
      case 26: // if_statement -> IF, '(', expression, ')', statement
{
        CurrentSemanticValue.IfStatement = new IfStatement(ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Statement);
    }
        break;
      case 27: // if_statement -> if_statement, ELSE, statement
{
        CurrentSemanticValue.IfStatement = ValueStack[ValueStack.Depth-3].IfStatement.AddElse(ValueStack[ValueStack.Depth-1].Statement);
    }
        break;
      case 28: // function_definition_statement -> value_type, IDENTIFIER, '(', parameter_list, 
               //                                  ')', '{', optional_statement_list, '}'
{
        CurrentSemanticValue.Statement = new FunctionDefinitionStatement(ValueStack[ValueStack.Depth-8].Type, ValueStack[ValueStack.Depth-7].Identifier, ValueStack[ValueStack.Depth-5].ParameterList, ValueStack[ValueStack.Depth-2].StatementList);
    }
        break;
      case 29: // optional_statement_list -> /* empty */
{
        CurrentSemanticValue.StatementList = new List<Statement>();
    }
        break;
      case 31: // parameter_list -> /* empty */
{
        CurrentSemanticValue.ParameterList = new List<FunctionDefinitionStatement.Parameter>();
    }
        break;
      case 32: // parameter_list -> value_type, IDENTIFIER
{
        CurrentSemanticValue.ParameterList = new List<FunctionDefinitionStatement.Parameter>{new FunctionDefinitionStatement.Parameter(ValueStack[ValueStack.Depth-2].Type, ValueStack[ValueStack.Depth-1].Identifier)};
    }
        break;
      case 33: // parameter_list -> parameter_list, ',', value_type, IDENTIFIER
{
        ValueStack[ValueStack.Depth-4].ParameterList.Add(new FunctionDefinitionStatement.Parameter(ValueStack[ValueStack.Depth-2].Type, ValueStack[ValueStack.Depth-1].Identifier));
        CurrentSemanticValue.ParameterList = ValueStack[ValueStack.Depth-4].ParameterList;
    }
        break;
      case 34: // value_type -> INT
{CurrentSemanticValue.Type = Type.INT;}
        break;
      case 35: // value_type -> FLOAT
{CurrentSemanticValue.Type = Type.FLOAT;}
        break;
      case 36: // value_type -> VECTOR
{CurrentSemanticValue.Type = Type.VECTOR;}
        break;
      case 37: // value_type -> VOID
{CurrentSemanticValue.Type = Type.VOID;}
        break;
      case 38: // expression -> '(', expression, ')'
{
        CurrentSemanticValue.Expression = ValueStack[ValueStack.Depth-2].Expression;
    }
        break;
      case 42: // declaration_statement -> value_type, declaration_list
{
        CurrentSemanticValue.Statement = new DeclarationStatement(ValueStack[ValueStack.Depth-2].Type, ValueStack[ValueStack.Depth-1].DeclarationList);
    }
        break;
      case 43: // declaration_statement -> CONST, value_type, declaration_list
{
        CurrentSemanticValue.Statement = new DeclarationStatement(ValueStack[ValueStack.Depth-2].Type, ValueStack[ValueStack.Depth-1].DeclarationList, true);
    }
        break;
      case 44: // declaration_list -> declaration_item
{
        CurrentSemanticValue.DeclarationList = new List<DeclarationStatement.DeclarationItem>{ValueStack[ValueStack.Depth-1].DeclaraionItem};
    }
        break;
      case 45: // declaration_list -> declaration_list, ',', declaration_item
{
        ValueStack[ValueStack.Depth-3].DeclarationList.Add(ValueStack[ValueStack.Depth-1].DeclaraionItem);
        CurrentSemanticValue.DeclarationList = ValueStack[ValueStack.Depth-3].DeclarationList;
    }
        break;
      case 46: // declaration_item -> IDENTIFIER
{
        CurrentSemanticValue.DeclaraionItem = new DeclarationStatement.DeclarationItem(ValueStack[ValueStack.Depth-1].Identifier);
    }
        break;
      case 47: // declaration_item -> IDENTIFIER, '=', expression
{
        CurrentSemanticValue.DeclaraionItem = new DeclarationStatement.DeclarationItem(ValueStack[ValueStack.Depth-3].Identifier, ValueStack[ValueStack.Depth-1].Expression);
    }
        break;
      case 48: // declaration_item -> IDENTIFIER, '[', INT_LITERAL, ']'
{
        CurrentSemanticValue.DeclaraionItem = new DeclarationStatement.DeclarationItem(ValueStack[ValueStack.Depth-4].Identifier, null, ValueStack[ValueStack.Depth-2].IntLiteral);
    }
        break;
      case 49: // assignment_statement -> assignment_lval_expression, '=', expression
{
        CurrentSemanticValue.Statement = new AssignmentStatement(ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Expression);
    }
        break;
      case 50: // assignment_statement -> assignment_lval_expression, INCREMENT
{
        var one = new IntLiteralExpression(1);
        CurrentSemanticValue.Statement = new AssignmentStatement(ValueStack[ValueStack.Depth-2].Expression, new BinaryExpression(BinaryExpression.Type.ADD, ValueStack[ValueStack.Depth-2].Expression, one));
    }
        break;
      case 51: // assignment_statement -> assignment_lval_expression, DECREMENT
{
        var one = new IntLiteralExpression(1);
        CurrentSemanticValue.Statement = new AssignmentStatement(ValueStack[ValueStack.Depth-2].Expression, new BinaryExpression(BinaryExpression.Type.SUB, ValueStack[ValueStack.Depth-2].Expression, one));
    }
        break;
      case 54: // binary_expression -> expression, '+', expression
{
        CurrentSemanticValue.Expression = new BinaryExpression(BinaryExpression.Type.ADD, ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Expression);
    }
        break;
      case 55: // binary_expression -> expression, '-', expression
{
        CurrentSemanticValue.Expression = new BinaryExpression(BinaryExpression.Type.SUB, ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Expression);            
    }
        break;
      case 56: // binary_expression -> expression, '*', expression
{
        CurrentSemanticValue.Expression = new BinaryExpression(BinaryExpression.Type.MUL, ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Expression);            
    }
        break;
      case 57: // binary_expression -> expression, '/', expression
{
        CurrentSemanticValue.Expression = new BinaryExpression(BinaryExpression.Type.DIV, ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Expression);            
    }
        break;
      case 58: // binary_expression -> expression, '%', expression
{
        CurrentSemanticValue.Expression = new BinaryExpression(BinaryExpression.Type.MOD, ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Expression);            
    }
        break;
      case 59: // binary_expression -> expression, "==", expression
{
        CurrentSemanticValue.Expression = new BinaryExpression(BinaryExpression.Type.EQ, ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Expression);
    }
        break;
      case 60: // binary_expression -> expression, "!=", expression
{
        CurrentSemanticValue.Expression = new BinaryExpression(BinaryExpression.Type.NE, ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Expression);
    }
        break;
      case 61: // binary_expression -> expression, ">", expression
{
        CurrentSemanticValue.Expression = new BinaryExpression(BinaryExpression.Type.GT, ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Expression);
    }
        break;
      case 62: // binary_expression -> expression, ">=", expression
{
        CurrentSemanticValue.Expression = new BinaryExpression(BinaryExpression.Type.GE, ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Expression);
    }
        break;
      case 63: // binary_expression -> expression, "<", expression
{
        CurrentSemanticValue.Expression = new BinaryExpression(BinaryExpression.Type.LT, ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Expression);
    }
        break;
      case 64: // binary_expression -> expression, "<=", expression
{
        CurrentSemanticValue.Expression = new BinaryExpression(BinaryExpression.Type.LE, ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Expression);
    }
        break;
      case 65: // binary_expression -> expression, "&&", expression
{
        CurrentSemanticValue.Expression = new BinaryExpression(BinaryExpression.Type.AND, ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Expression);
    }
        break;
      case 66: // binary_expression -> expression, "||", expression
{
        CurrentSemanticValue.Expression = new BinaryExpression(BinaryExpression.Type.OR, ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Expression);
    }
        break;
      case 67: // unary_expression -> '-', expression
{
        CurrentSemanticValue.Expression = new UnaryExpression(UnaryExpression.Type.NEGATE, ValueStack[ValueStack.Depth-1].Expression);
    }
        break;
      case 68: // unary_expression -> '!', expression
{
        CurrentSemanticValue.Expression = new UnaryExpression(UnaryExpression.Type.NOT, ValueStack[ValueStack.Depth-1].Expression);
    }
        break;
      case 69: // identifier_expression -> IDENTIFIER
{
        CurrentSemanticValue.Expression = new IdentifierExpression(ValueStack[ValueStack.Depth-1].Identifier);
    }
        break;
      case 74: // index_expression -> possible_array_expression, '[', expression, ']'
{
        CurrentSemanticValue.Expression = new IndexExpression(ValueStack[ValueStack.Depth-4].Expression, ValueStack[ValueStack.Depth-2].Expression);
    }
        break;
      case 75: // literal_expression -> INT_LITERAL
{
        CurrentSemanticValue.Expression = new IntLiteralExpression(ValueStack[ValueStack.Depth-1].IntLiteral);
    }
        break;
      case 76: // literal_expression -> FLOAT_LITERAL
{
        CurrentSemanticValue.Expression = new FloatLiteralExpression(ValueStack[ValueStack.Depth-1].FloatLiteral);
    }
        break;
      case 77: // literal_expression -> VECTOR_LITERAL
{
        CurrentSemanticValue.Expression = new VectorLiteralExpression(ValueStack[ValueStack.Depth-1].VectorLiteral);
    }
        break;
      case 78: // function_call_expression -> IDENTIFIER, '(', expression_list, ')'
{
        CurrentSemanticValue.Expression = new FunctionCallExpression(ValueStack[ValueStack.Depth-4].Identifier, ValueStack[ValueStack.Depth-2].ExpressionList);
    }
        break;
      case 80: // expression_list -> expression
{
        CurrentSemanticValue.ExpressionList = new List<Expression>{ValueStack[ValueStack.Depth-1].Expression};
    }
        break;
      case 81: // expression_list -> expression_list, ',', expression
{
        ValueStack[ValueStack.Depth-3].ExpressionList.Add(ValueStack[ValueStack.Depth-1].Expression);
        CurrentSemanticValue.ExpressionList = ValueStack[ValueStack.Depth-3].ExpressionList;
    }
        break;
    }
#pragma warning restore 162, 1522
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliases != null && aliases.ContainsKey(terminal))
        return aliases[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }

public Parser(Scanner scnr) : base(scnr) { }
}
}
