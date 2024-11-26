using System;

namespace Aoc2024Net.Utilities
{
    [AttributeUsage(AttributeTargets.Field)]
    internal sealed class TokenAttribute : Attribute
    {
        public TokenAttribute(string token) => Token = token;

        public string Token { get; }
    }
}
