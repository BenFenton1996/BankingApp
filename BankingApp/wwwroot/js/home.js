$(document).ready(function () {
    $("#transfer-form").on("submit", function (e) {
        checkAllFormInputs($("#transfer-form *"), e);
    });

    //Adds styling for inputs that shouldn't be empty
    addInputRequiredStyling($("#transfer-form *").filter(":input"));

    $("#AmountToTransfer").on("keyup", function (e) {
        if (e.target.value !== "") {
            $("#SenderID").prop("disabled", false);
            let recipientElement = $("#RecipientID");
            if (recipientElement.val() !== "") {
                recipientElement.prop("disabled", false);
            }
        }
        else {
            $("#SenderID").prop("disabled", true);
            $("#RecipientID").prop("disabled", true);
        }
    });
    $("#SenderID").on("change", function (e) {
        if (e.target.value !== "") {
            $("#RecipientID").prop("disabled", false);
        }
        else {
            $("#RecipientID").prop("disabled", true);
        }
    });
});