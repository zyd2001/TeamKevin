%{
    public override void yyerror(string format, params object[] args)
    {
        Console.Error.Write("{0}:{1}:",yyline, yycol);
        Console.Error.WriteLine(format);
    }
%}

%namespace CompilerCore
%visibility internal
%option summary
%using QUT.Gppg;

identifier [a-zA-Z_][a-zA-Z_0-9]*
digits [0-9]+

%%
"int"           return (int)Tokens.INT;
"vector"        return (int)Tokens.VECTOR;
"float"         return (int)Tokens.FLOAT;
"asInt"         return (int)Tokens.ASINT;
"asFloat"       return (int)Tokens.ASFLOAT;
"void"          return (int)Tokens.VOID;
"if"            return (int)Tokens.IF;
"else"          return (int)Tokens.ELSE;
"for"           return (int)Tokens.FOR;
"while"         return (int)Tokens.WHILE;
"break"         return (int)Tokens.BREAK;
"continue"      return (int)Tokens.CONTINUE;
"return"        return (int)Tokens.RETURN;
"struct"        return (int)Tokens.STRUCT;
"const"         return (int)Tokens.CONST;
"trace"         return (int)Tokens.TRACE;
"reduce"        return (int)Tokens.REDUCE;
"sqrt"          return (int)Tokens.SQRT;
"&&"            return (int)Tokens.AND;
"||"            return (int)Tokens.OR;
"=="            return (int)Tokens.EQ;
"!="            return (int)Tokens.NE;
">"             return (int)Tokens.GT;
">="            return (int)Tokens.GE;
"<"             return (int)Tokens.LT;
"<="            return (int)Tokens.LE;
"++"            return (int)Tokens.INCREMENT;
"--"            return (int)Tokens.DECREMENT;
"{"             |
"}"             |
"["             |
"]"             |
"("             |
")"             |
","             |
";"             |
"!"             |
"+"             |
"-"             |
"*"             |
"/"             |
"%"             |
"^"             |
"~"             |
"="             return yytext[0];
[\ \t] ;
{identifier} {
    yylval.Identifier = yytext;
    return (int)Tokens.IDENTIFIER;
}
{digits}* {
    yylval.IntLiteral = int.Parse(yytext);
    return (int)Tokens.INT_LITERAL;
}
[\+\-]?({digits}*[\.])?{digits}+ {
    yylval.FloatLiteral = float.Parse(yytext);
    return (int)Tokens.FLOAT_LITERAL;
}
\[[\ ]*[\+\-]?({digits}*[\.])?{digits}+[\ ]*\,[\ ]*[\+\-]?({digits}*[\.])?{digits}+[\ ]*\,[\ ]*[\+\-]?({digits}*[\.])?{digits}+[\ ]*\,[\ ]*[\+\-]?({digits}*[\.])?{digits}+[\ ]*\] {
    yylval.VectorLiteral = yytext;
    return (int)Tokens.VECTOR_LITERAL;
}
"//".* ;
. return yytext[0];

%{
    yylloc = new LexLocation(tokLin,tokCol,tokELin,tokECol);
%}
%%
