using System;

namespace proto_parser.FrontEnd.Lexical
{
    public enum CharType
    {
        Letter,
        DecimalDigit,
        OctalDigit,
        Symbol
    }
    
    public enum TokenType
    {
       Start,
       End,
       
       Identifier,
       Integer,
       Float,
       String,
       Symbol,
       WhiteSpace,
       Newline
       
       // ref: https://github.com/protocolbuffers/protobuf/blob/master/src/google/protobuf/io/tokenizer.h
    }

    public class Token
    {
        public int Offset { get; }
        public int LineNumber { get; }
        public string File { get; }
        public int ColumnNumber { get; }
        public string Value { get; }
        public string LineContent { get; }
        public TokenType Type { get; }
        
        // public methods
        public static bool operator== (Token x, Token y)
        {
            return x.Offset == y.Offset && x.File == y.File;
        }
        public static bool operator!= (Token x, Token y)
        {
            return x.Offset != y.Offset || x.File != y.File;
        }
        public override int GetHashCode() => Offset;
        public override bool Equals(object obj) => obj is Token token && token.Offset == this.Offset;

        internal Token(string value, int lineNumber, int columnNumber, TokenType type, string lineContent, int offset,
            string file)
        {
            Value = value;
            LineNumber = lineNumber;
            ColumnNumber = columnNumber;
            Type = type;
            LineContent = lineContent;
            Offset = offset;
            File = file;
        }

        internal Exception Throw(ErrorCode code, string error = null, bool isError = true) =>
            throw new ParserException(this, string.IsNullOrEmpty(error) ? $"syntax error: '{Value}'" : error, isError,
                code);
        
        internal void Assert(TokenType type, string value = null)
        {
            if (value != null)
            {
                if (type != Type || value != Value)
                {
                    Throw(ErrorCode.ExpectedToken, $"expected {type} '{value}'");
                }
            }
            else
            {
                if (type != Type)
                {
                    Throw(ErrorCode.ExpectedToken, $"expect {type}");
                }
            }
        }

        internal bool Is(TokenType type, string value = null)
        {
            if (Type != type) return false;
            return !(value != null && Value != value);
        }
    }
}