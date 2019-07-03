$(document).ready(function () {
    $("#login-form").on("submit", function (e) {
        checkAllFormInputs($("#login-form *"), e);
    });
    $("#signup-form").on("submit", function (e) {
        checkAllFormInputs($("#signup-form *"), e);
    });

    //Adds styling for inputs that shouldn't be empty
    addInputRequiredStyling($("#login-form *").filter("input[type=text], input[type=password], input[type=email]"));
    addInputRequiredStyling($("#signup-form *").filter("input[type=text], input[type=password], input[type=email]"));
});