var qrCode;

window.qrGenerator = {

       initializeQrCode: function (container) {
        var containerElement = document.getElementById(container);

        if (containerElement !== null && qrCode === undefined) {
            qrCode = new QRious({element: containerElement, size: 300});
        }
    },

    generateQrCode: function (data) {
        // stringify json the data
       const jsonData = JSON.stringify(data);
       qrCode.set({value: jsonData});
    },

    clearQrCode: function () {
        qrCode.set({value: ''});
    }

}

//var qrScanner;
//window.qrScanHelper = {
//    startScan: function (dotNetObject) {
//        qrScanner = new QrScanner(
//            document.getElementById("qrScanner"),
//            result => {
//                console.log('decoded qr code:', result)
//                dotNetObject.invokeMethodAsync('OnQrCodeScanned', result.data);
//            },
//            { 
//                highlightScanRegion: true,
//                highlightCodeOutline: true,

//            },
//        );
//        qrScanner.start();


//    },

//     stopScan: function () {
//        qrScanner.stop();
//    }
//};

var qrScanner;
var scanTimeout; // Variable to hold the timeout reference

window.qrScanHelper = {
    startScan: function (dotNetObject) {
        qrScanner = new QrScanner(
            document.getElementById("qrScanner"),
            result => {
                // Clear any existing timeout to avoid multiple calls
                if (scanTimeout) {
                    clearTimeout(scanTimeout);
                }

                // Set a new timeout to call the Blazor method after 3 seconds
                scanTimeout = setTimeout(() => {
                    console.log('decoded qr code:', result);
                    dotNetObject.invokeMethodAsync('OnQrCodeScanned', result.data);
                }, 1250); // 3000 milliseconds = 3 seconds
            },
            {
                highlightScanRegion: true,
                highlightCodeOutline: true,
            },
        );
        qrScanner.start();
    },

    stopScan: function () {
        qrScanner.stop();
        // Clear the timeout when stopping the scan
        if (scanTimeout) {
            clearTimeout(scanTimeout);
        }
    }
};