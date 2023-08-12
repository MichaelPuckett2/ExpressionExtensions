using System.Runtime.Serialization;

namespace G3.ExpressionExtensions
{
    [Serializable]
    public class LambdaExpressionMemberNotFoundException : Exception
    {
        public LambdaExpressionMemberNotFoundException() { }
        public LambdaExpressionMemberNotFoundException(string? message) : base(message) { }
        public LambdaExpressionMemberNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }
        protected LambdaExpressionMemberNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}