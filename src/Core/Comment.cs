namespace L5Sharp.Core
{
    public class Comment
    {
        public Comment(string operand, string comment)
        {
            Operand = operand;
            Value = comment;
        }

        public string Operand { get; }
        public string Value { get; private set; }
        
        /// <summary>
        /// Overrides the description ...
        /// </summary>
        /// <param name="comment"></param>
        public void Override(string comment)
        {
            Value = comment;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public static implicit operator string(Comment comment)
        {
            return comment.Value;
        }
    }
}