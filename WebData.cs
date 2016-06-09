
using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Net;
using System.Text;
using System.Diagnostics;
using Sce.PlayStation.Core.Environment;


namespace AppData3088261ATSI
{
	public class WebData
	{
		
	    public const int maxReadSize = 1024;
	    public int totalReadSize;
	    public MemoryStream readStream;
	    public byte[] readBuffer;
	    public ConnectState connectState = ConnectState.None;
	
	    public string statusCode;
	    public long contentLength;
	
		private string url;
		
	    public enum ConnectState
	    {
	        None,
	        Ready,
	        Connect,
	        Success,
	        Failed
	    }
	
	    
	    public WebData()
		{
			Init();
			
		}
		
	    public bool Init()
	    {
	
	        connectState = ConnectState.None;
	
	        return true;
	    }
	
	   	public void connect(string link)
		{
			connectState = ConnectState.Ready;	
			url = link;
			webData = "";
		}
	    public bool Update()
	    {
	        switch (connectState) {
	        case ConnectState.None:
	            
	            break;
	        case ConnectState.Ready:
	            connectHttpTest(url);
	            break;
	        case ConnectState.Success:
				
	
	            if (readStream != null) {
	                readWeb(readStream.ToArray(), statusCode, contentLength);
	
	                readStream.Dispose();
	                readStream = null;
	                readBuffer = null;
	            }
	
	            connectState = ConnectState.None;
	            break;
	        case ConnectState.Failed:
	            webData = "ERROR";
	            connectState = ConnectState.None;
	            break;
	        }
	
	        return true;
	    }
			
	    /// Http connection test
	    public bool connectHttpTest(string url)
	    {
	        statusCode = "Unknown";
	        contentLength = 0;
	
	        try {
	            var webRequest = HttpWebRequest.Create(url);
	            // If you use web proxy, uncomment this and set appropriate address.
	            //webRequest.Proxy = new System.Net.WebProxy("http://your_proxy.com:10080");
	            webRequest.BeginGetResponse(new AsyncCallback(requestCallBack), webRequest);
	            connectState = ConnectState.Connect;
	        } catch (Exception e) {
	            Console.WriteLine(e);
	            connectState = ConnectState.Failed;
	            return false;
	        }
	
	        return true;
	    }
	
	    private void requestCallBack(IAsyncResult ar)
	    {
	        try {
	            var webRequest = (HttpWebRequest)ar.AsyncState;
	            var webResponse = (HttpWebResponse)webRequest.EndGetResponse(ar);
	
	            statusCode = webResponse.StatusCode.ToString();
	            contentLength = webResponse.ContentLength;
	            readBuffer = new byte[1024];
	            readStream = new MemoryStream();
	            totalReadSize = 0;
	            var stream = webResponse.GetResponseStream();
	            stream.BeginRead(readBuffer, 0, readBuffer.Length, new AsyncCallback(readCallBack), stream);
	        } catch (Exception e) {
	            Console.WriteLine(e);
	            connectState = ConnectState.Failed;
	        }
	    }
	
	    private void readCallBack(IAsyncResult ar)
	    {
	        try {
	            var stream = (Stream)ar.AsyncState;
	            int readSize = stream.EndRead(ar);
	
	            if (readSize > 0) {
	                totalReadSize += readSize;
	                readStream.Write(readBuffer, 0, readSize);
	            }
	
	            if (readSize <= 0 || totalReadSize >= maxReadSize) {
	                stream.Close();
	                connectState = ConnectState.Success;
	            } else {
	                stream.BeginRead(readBuffer, 0, readBuffer.Length, new AsyncCallback(readCallBack), stream);
	            }
	        } catch (Exception e) {
	            Console.WriteLine(e);
	            connectState = ConnectState.Failed;
	        }
	    }
		
		public string webData;
	    /// Create a sprite from a string buffer
	    private void readWeb(byte[] buffer, string statusCode, long contentLength)
	    {
	
	        if (buffer != null) {
	            var stream = new MemoryStream(buffer);
	            var reader = new StreamReader(stream);
				webData = reader.ReadToEnd().ToString();
	            reader.Close();
	            stream.Close();
	        }
	    }
	}
}

