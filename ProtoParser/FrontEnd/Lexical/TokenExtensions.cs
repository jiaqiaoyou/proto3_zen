using System.Collections.Generic;
using System.IO;
using System.Text;

namespace proto_parser.FrontEnd.Lexical
{
    internal static class TokenExtensions
    {
        public static bool Is(this Peekable<Token> tokens, TokenType type, string value = null)
            => tokens.Peek(out var val) && val.Is(type, value);

        private static Token Read(this Peekable<Token> tokens)
        {
            if (!tokens.Peek(out var val))
            {
                throw new ParserException(tokens.Previous, "unexpected end of file", true, ErrorCode.UnexpectedEOF);
            }
            return val;
        }
        
        public static void Consume(this Peekable<Token> tokens, TokenType type, string value)
        {
            var token = tokens.Read();
            token.Assert(type, value);
            tokens.Consume();
        }

        public static bool ConsumeIf(this Peekable<Token> tokens, TokenType type, string value)
        {
            if (!tokens.Peek(out var val)) return false;
            if (!val.Is(type, value)) return false;

            tokens.Consume();
            return true;
        }

        public static IEnumerable<Token> Tokenize(this TextReader reader, string file)
        {
            var buffer = new StringBuilder();
            var row = 0;
            var offset = 0;
            var line = "";
            var lastLine = "";

            while ((line = reader.ReadLine()) != null)
            {
                lastLine = line;
                ++row;

                foreach (var c in line)
                {
                    
                }
            }
        }
    }
}