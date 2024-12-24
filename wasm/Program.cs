using System.Runtime.InteropServices.JavaScript;
using Newtonsoft.Json;
using SkiaSharp;
using ZXing;

public partial class JSBridge
{

    [JSImport("globalThis.console.log")]
    internal static partial void Log([JSMarshalAs<JSType.String>] string message);



    public static SKBitmap Base64ToBitmap(string base64String)
    {
        // 将 Base64 字符串解码为字节数组
        byte[] imageBytes = Convert.FromBase64String(base64String);
        SKMemoryStream stream = new SKMemoryStream(imageBytes);
        return SKBitmap.Decode(stream);
    }


    [JSExport]
    internal static string DecodeImage(string imgbase64)
    {

        // 读取图像文件
        var barcodeBitmap = Base64ToBitmap(imgbase64);

        List<string> resList = new List<string>();
        var barcodeReader = new ZXing.SkiaSharp.BarcodeReader
        {
            AutoRotate = true,
            Options = new ZXing.Common.DecodingOptions
            {
                PossibleFormats = new[] { BarcodeFormat.CODE_128 },
                TryHarder = true,
                TryInverted = true,
                PureBarcode = false
            }
        };//参数项根据具体项目需求进行调整以提高识别率，在当前需求中条形码就只有CODE128格式的，所以就只添加了CODE128

        // 解码图像中的条形码
        var barcodeResults = barcodeReader.DecodeMultiple(barcodeBitmap);
        // 如果成功识别到条形码
        if (barcodeResults != null && barcodeResults.Length > 0)
        {

            Log($"Found {barcodeResults.Length} barcodes:");
            for (int i = 0; i < barcodeResults.Length; i++)
            {


                Log($"  Text: {barcodeResults[i].Text}");
                resList.Add(barcodeResults[i].Text);
                if (barcodeResults[i].ResultPoints.Length > 0)
                {
                    Log($"  Position: ({barcodeResults[i].ResultPoints[0].X}, {barcodeResults[i].ResultPoints[0].Y})");
                }
            }
        }
        else
        {
            Console.WriteLine("No CODE128 barcodes found in the image.");
        }
        var resJson = JsonConvert.SerializeObject(resList);


        //!important 记得要进行垃圾回收，不然在浏览器端会一直占用内存
        GC.Collect();
        return resJson;
    }


}

public class Program
{

    [STAThread]
    static void Main(params string[] paramaters)
    {
        Console.WriteLine("WASM MAIN");
    }
}