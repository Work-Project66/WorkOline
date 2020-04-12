using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace work.api.Model
{
    public class ResultModelT<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public HttpStatusCode code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public String message { get; set; }
        /// <summary>
        /// 请求是否成功
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 数据实体
        /// </summary>
        public T body { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public long totalCount { get; set; }


        public void Failed(HttpStatusCode errCode,string errMessage)
        {
            this.code = errCode;
            this.message = errMessage;
            this.success = false;
            
        }

        public void Success(string successMsg, T successBody,long successCount=0)
        {
            this.code = HttpStatusCode.OK;
            this.message = successMsg;
            this.success = true;
            this.body = successBody;
            this.totalCount = successCount;
        }


    }
}
