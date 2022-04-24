
namespace Redback;

public enum CapTableEntryType {

    Issuance,
    Trade
}

public partial class CapTableEntry {

    public CapTableEntryType CapTableEntryType { get; init; }

    public CapTableIssuing? Issuing { get; init; }

    public CapTableTrade? Trade { get; init; }

    ///

    public CapTableEntry(
        CapTableEntryType capTableEntryType,
        CapTableIssuing? issuing,
        CapTableTrade? trade
        ) {

        this.CapTableEntryType = capTableEntryType;
        this.Issuing = issuing;
        this.Trade = trade;
    }
}

public partial class CapTableIssuing {

    public String Receiver { get; init; }

    public double Value { get; init; }

    ///

    public CapTableIssuing(
        String receiver,
        double value) {

        this.Receiver = receiver;
        this.Value = value;
    }
}

public partial class CapTableTrade {

    public String Origin { get; init; }

    public String Receiver { get; init; }

    public double Value { get; init; }

    ///

    public CapTableTrade(
        String origin,
        String receiver,
        double value) {

        this.Origin = origin;
        this.Receiver = receiver;
        this.Value = value;
    }
}

///

public partial class Program {

    public static void Main(
        String[] args) {

        // var salt = new byte[5] { 0x00, 0x01, 0x02, 0x03, 0x04 };

        var ogBytes = new byte[64] {
            // 0xc4, 0xb7, 0x09, 0x12, 0xd6, 0x2f, 0x0f, 0x35, 0x93, 0x6f, 0xe7, 0xf9, 0x95, 0x6d, 0x9a, 0xca, 0x50, 0xe8, 0x65, 0xcf, 0xf1, 0x93, 0xbd, 0x7e, 0xdb, 0x3e, 0xbf, 0x46, 0x18, 0x71, 0xfc, 0x51, 0xbe, 0x4b, 0x36, 0xd4, 0x38, 0x68, 0x87, 0x37, 0xac, 0x42, 0x9f, 0x39, 0x99, 0x94, 0x03, 0x2d, 0x1c, 0xe5, 0xe2, 0xe4, 0x4b, 0x44, 0xbc, 0x16, 0x03, 0x89, 0x06, 0xd6, 0xc6, 0xb2, 0x41, 0xab
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };

        ///

        var ogString = ToHexString(ogBytes);

        WriteLine($"og: {ogString}");
        
        WriteLine($"og length (raw): {ogBytes.Length}; og length (string): {ogString.Length}\n");

        ///

        using var hmac = new HMACSHA512(ogBytes);

        var firstString = "8118b249ba97416c712d0bd41957aea69a0ed5165c690181713dcc0aabdf2e3d92354f5797243cb3cfbd71040ee44955a4a91600fccd7684db3a63dd11d57596";
        
        var firstBytes = HexStringToBytes(firstString);

        WriteLine($"first: {firstString}");
        
        WriteLine($"first length (raw): {firstBytes.Length}; first length (string): {firstString.Length}\n");

        ///

        var hashBytes = hmac.ComputeHash(firstBytes);

        var hashString = ToHexString(hashBytes);

        WriteLine($"hash: {hashString}");
        
        WriteLine($"hash length (raw): {hashBytes.Length}; hash length (string): {hashString.Length}\n");

        // WriteLine($"hash: {hashString}");

        // WriteLine($"hash length: {hash.Length}");

        // WriteLine($"hello foo");
    }

    public static byte[] HexStringToBytes(
        String hashString) {

        if (hashString.Length % 2 != 0) {

            throw new Exception();
        }

        ///

        var half = hashString.Length / 2;

        var bytes = new byte[half];

        for (var i = 0; i < half; ++i) {

            var o = i * 2;

            var a = ToByte(hashString[o]);

            var b = ToByte(hashString[o + 1]);

            var c = a << 4 | b;

            bytes[i] = (byte) c;

            // Write($"{c.ToString("x2")}");
        }

        return bytes;

        // Write("\n");


    }

    public static String ToHexString(
        byte[] bytes
    ) {

        return String.Join("", bytes.Select(x => x.ToString("x2")));
    }

    public static byte ToByte(
        Char c) {

        switch (Char.ToLower(c)) {

            case '0':
                return 0x00;
            
            case '1':
                return 0x01;

            case '2':
                return 0x02;

            case '3':
                return 0x03;

            case '4':
                return 0x04;

            case '5':
                return 0x05;

            case '6':
                return 0x06;

            case '7':
                return 0x07;

            case '8':
                return 0x08;

            case '9':
                return 0x09;

            case 'a':
                return 0x0a;

            case 'b':
                return 0x0b;

            case 'c':
                return 0x0c;

            case 'd':
                return 0x0d;

            case 'e':
                return 0x0e;

            case 'f':
                return 0x0f;

            default:

                throw new Exception();
        }
    }
}