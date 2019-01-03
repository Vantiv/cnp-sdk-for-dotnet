using System;

namespace Cnp.Sdk
{
    public class RequestTarget
    {

        private String targetUrl = null;
        private int urlIndex = 0;
        private long requestTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        public RequestTarget(String url, int index)
        {
            targetUrl = url;
            urlIndex = index;
        }
        public String getUrl()
        {
            return targetUrl;
        }
        public long getRequestTime()
        {
            return requestTime;
        }
        public int getUrlIndex()
        {
            return urlIndex;
        }
    }
}
