﻿namespace ServiceModel.Models
{
    public class ExceptionResponse
    {
        public string Success { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionType { get; set; }
        public string StackTrace { get; set; }
        public ExceptionResponse InnerException { get; set; }
    }
}
