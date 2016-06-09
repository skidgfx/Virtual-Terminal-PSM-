
namespace AppData3088261ATSI
{

/**
 * HttpSample
 */
public class WebSocket
{
    
    private const int maxReadSize = 1024;
    private static int totalReadSize;
    private static MemoryStream readStream;
    private static byte[] readBuffer;
    private static ConnectState connectState = ConnectState.None;

    private static string statusCode;
    private static long contentLength;

    public enum ConnectState
    {
        None,
        Ready,
        Connect,
        Success,
        Failed
    }

    static bool loop = true;

    public WebSocket{}

    public static bool Init()
    {

        connectState = ConnectState.None;

        return true;
    }

    
    public static bool Update()
    {
        switch (connectState) {
        case ConnectState.None:
            
            break;
        case ConnectState.Ready:
            connectHttpTest("http://tsiserver.us/data/settings.dat");
            break;
        case ConnectState.Success:
            connectButton.ButtonColor = 0xffffffff;

            if (readStream != null) {
                httpText = createTextSprite(readStream.ToArray(), statusCode, contentLength);

                readStream.Dispose();
                readStream = null;
                readBuffer = null;
            }

            connectState = ConnectState.None;
            break;
        case ConnectState.Failed:
            httpText = createTextSprite(null, statusCode, contentLength);
            connectState = ConnectState.None;
            break;
        }

        return true;
    }
		
    /// Http connection test
    private static bool connectHttpTest(string url)
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

    private static void requestCallBack(IAsyncResult ar)
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

    private static void readCallBack(IAsyncResult ar)
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
	
	public static string webData;
    /// Create a sprite from a string buffer
    private static void readWeb(byte[] buffer, string statusCode, long contentLength)
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

} // Sample
