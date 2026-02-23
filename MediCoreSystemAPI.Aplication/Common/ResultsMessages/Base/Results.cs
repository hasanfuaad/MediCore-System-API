using System.Text.Json.Serialization;

namespace Application.Common
{
    public abstract class Results
    {
        [JsonPropertyOrder(0)]
        public bool Success { get; protected set; }

        [JsonPropertyOrder(1)]
        public string? Message { get; set; }

        [JsonPropertyOrder(2)]
        public int Code { get; set; }

        [JsonPropertyOrder(3)]
        public int ResultCode { get; set; }

        [JsonPropertyOrder(4)]
        public ServiceError? Error { get; set; }
    }

    public abstract class Results<T> : Results
    {
        private T _data = default!;

        protected Results(T data)
        {
            Data = data;
        }

        [JsonPropertyOrder(5)]
        public T Data
        {
            get => _data != null ? _data : default!;
            set => _data = value;
        }
    }
}


