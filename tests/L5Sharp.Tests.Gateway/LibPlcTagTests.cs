using libplctag.NativeImport;

namespace L5Sharp.Tests.Gateway;

[TestFixture]
public class LibPlcTagTests
{
    [Test]
    public void ReadAsync_SimpleType_ShouldMatchControllerSize()
    {
        // Set up connection - Replace with your actual IP and a known Timer name
        const string ip = "10.10.38.32";
        const string tagName = "RTC.MONTH";
        const string path = $"protocol=ab_eip&gateway={ip}&path=1,1&plc=controllogix&name={tagName}";

        var handle = plctag.plc_tag_create(path, 5000);
        // Check if creation failed
        if (handle < 0) Assert.Fail($"Failed to create tag. Error: {plctag.plc_tag_decode_error(handle)}");

        // Read from PLC
        var read = plctag.plc_tag_read(handle, 5000);
        if (read != 0) Assert.Fail($"Read failed: {plctag.plc_tag_decode_error(read)}");

        var size = plctag.plc_tag_get_size(handle);
        var buffer = new byte[size];
        var result = plctag.plc_tag_get_raw_bytes(handle, 0, buffer, buffer.Length);
        Assert.That(result, Is.EqualTo(0));

        // 5. Output for analysis
        Console.WriteLine($"Tag: {tagName} | Size: {size} bytes");
        Console.WriteLine($"Bytes: {string.Join(",", buffer)}");

        plctag.plc_tag_destroy(handle);
    }
}