﻿using System.Security.Cryptography;
using System.Text;

namespace StringCoder;

public static class Base64Coder
{
    public static string ToBase64(this string value)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
    }
    
    public static string FromBase64(this string value)
    {
        return Encoding.UTF8.GetString(Convert.FromBase64String(value));
    }
}