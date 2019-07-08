$(document).ready(function () {
    //Add is-invalid class to inputs and prevent form submission if required inputs in quick transfer are empty
    $("#quick-transfer-form").on("submit", function (e) {
        e.preventDefault();
        senderId = $("#SenderID");
        recipientId = $("#RecipientID");
        if (senderId.val() === "") {
            senderId.addClass("is-invalid");
        }
        if (recipientId.val() === "") {
            recipientId.addClass("is-invalid");
            return false;
        }
        checkAllFormInputs($("#quick-transfer-form *"), e);

        let quickTransferForm = $("#quick-transfer-form");
        $.ajax({
            type: "POST",
            url: quickTransferForm.attr("action"),
            dataType: "json",
            data: quickTransferForm.serialize(),
            success: function (response) {
                if (response) {
                    //If the transaction was successful, remove the hidden attribute from the success message and add it to the failure message
                    $("#transfer-success-message").prop("hidden", false);
                    $("#transfer-failure-message").prop("hidden", true);
                }
                else {
                    //If the transaction failed, remove the hidden attribute from the failure message and add it to the success message
                    $("#transfer-success-message").prop("hidden", true);
                    $("#transfer-failure-message").prop("hidden", false);
                }
            }
        })
    });
    addInputRequiredStyling($("#SenderID"));
    addInputRequiredStyling($("#RecipientID"));

    //Disable inputs in quick transfer if the previous required input(s) are empty
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
    //Validation for Quick Transfer END -------------------
});