using System;
using proto_parser.FrontEnd.Lexical;

namespace proto_parser.FrontEnd
{
    internal class ParserException : Exception
    {
        public int ColumnNumber { get; }
        public int LineNumber { get; }
        public string File { get; }
        public string Text { get; }
        public string LineContents { get; }
        public bool IsError { get; }
        internal ErrorCode ErrorCode { get; }

        internal ParserException(Token token, string message, bool isError, ErrorCode errorCode)
            : base(message ?? "error")
        {
            ColumnNumber = token.ColumnNumber;
            LineNumber = token.LineNumber;
            File = token.File;
            LineContents = token.LineContent;
            Text = token.Value ?? "";
            IsError = isError;
            ErrorCode = errorCode;
        }
    }
}