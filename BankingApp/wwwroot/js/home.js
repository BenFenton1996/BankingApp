$(document).ready(function () {
    //Add is-invalid class to inputs and prevent form submission if required inputs in quick transfer are empty
    $("#quick-transfer-form").on("submit", function (e) {
        e.preventDefault();
        senderId = $("#sender-id");
        recipientId = $("#recipient-id");
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

                    //Reset quick transfer form
                    $("#amount-to-transfer").val("");

                    SenderIdElement = $("#sender-id");
                    SenderIdElement.val("");
                    SenderIdElement.prop("disabled", true);

                    recipientIdElement = $("#recipient-id");
                    recipientIdElement.val("");
                    recipientIdElement.prop("disabled", true);

                    RefreshBankAccounts();
                }
                else {
                    //If the transaction failed, remove the hidden attribute from the failure message and add it to the success message
                    $("#transfer-success-message").prop("hidden", true);
                    $("#transfer-failure-message").prop("hidden", false);
                }
            }
        });
    });
    addInputRequiredStyling($("#sender-id"));
    addInputRequiredStyling($("#recipient-id"));

    //Disable inputs in quick transfer if the previous required input(s) are empty
    $("#amount-to-transfer").on("keyup", function (e) {
        if (e.target.value !== "") {
            $("#sender-id").prop("disabled", false);
            let recipientElement = $("#recipient-id");
            if (recipientElement.val() !== "") {
                recipientElement.prop("disabled", false);
            }
        }
        else {
            $("#sender-id").prop("disabled", true);
            $("#recipient-id").prop("disabled", true);
        }
    });
    $("#sender-id").on("change", function (e) {
        if (e.target.value !== "") {
            $("#recipient-id").prop("disabled", false);
        }
        else {
            $("#recipient-id").prop("disabled", true);
        }
    });

    $("#refresh-bank-accounts-btn").on("click", function (e) {
        RefreshBankAccounts();
    });
});

//Refresh the list of bank accounts to show the user's updated balance
function RefreshBankAccounts() {
    $.ajax({
        type: "GET",
        url: "/Home/Home/BankAccountsPartial",
        success: function (response) {
            $("#bank-accounts").html(response);
        }
    });
}

//Toggles the bottom section on bank accounts between visible and hidden
function ToggleAccountBottom(accountId) {
    let recentTransactions = $("#account-" + accountId + "-bottom");
    if ($(recentTransactions).is(":visible")) {
        recentTransactions.attr("hidden", true)
    }
    else {
        recentTransactions.attr("hidden", false)
    }
}