using System;

namespace BookEnd.Clasess
{
    public class CaptchaResult
    {
        public string CaptchaCode { get; internal set; }
        public byte[] CaptchaByteData { get; internal set; }
        public DateTime Timestamp { get; internal set; }
    }
}