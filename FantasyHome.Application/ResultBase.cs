namespace FantasyHome.Application
{

    public class ResultBase<T>
    {
        /// <summary>状态码</summary>
        public int? StatusCode { get; set; }

        /// <summary>数据</summary>
        public T Data { get; set; }

        /// <summary>执行成功</summary>
        public bool Succeeded { get; set; }

        /// <summary>错误信息</summary>
        public object Errors { get; set; }

        /// <summary>附加数据</summary>
        public object Extras { get; set; }

        /// <summary>时间戳</summary>
        public long Timestamp { get; set; }


    }
}