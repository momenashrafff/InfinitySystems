function executeDeviceChargeWithDelay(deviceId) {
    var isRequestInProgress = false;

    if (!isRequestInProgress) {
        $.ajax({
            type: "POST",
            url: "/Device/DeviceCharge",
            data: { Id: deviceId },
            success: function (result) {
                $(".chargeIdElement").text("");
                $("#chargeIdElement_" + deviceId).text("Device Charge: " + result);
            },
            error: function (error) {
                console.error(error);
            },
            // complete: function () {
            //     setTimeout(function () {
            //         isRequestInProgress = false;
            //     }, 50); 
            // }
        });
    } else {
        // console.log('DeviceCharge action already in progress. Please wait.');
    }
}

$(document).ready(function () {
    var isRequestInProgress = false;

    $('.myCard').on('mouseenter', function () {
        console.log('mouseenter');
        if (!isRequestInProgress) {
            var deviceId = $(this).find('[name="Id"]').val();
            executeDeviceChargeWithDelay(deviceId);
        } else {
            // console.log('DeviceCharge action already in progress. Please wait.');
        }
    });
});
